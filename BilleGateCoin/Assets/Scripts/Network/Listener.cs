using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

namespace Network
{
    class Listener
    {
        public Queue<NetworkMessage> IncomingQueue;

        public Mutex QueueMutex;

        private Thread listenerThread;

        private bool keepListening;
        public bool IsRunning => keepListening;

        private int port;
        public int Port => port;

        private int maxClientsQueue;
        public int MaxClientsQueue => maxClientsQueue;

        private int updateInterval;

        public Listener(int port, int maxClientsQueue, int serverUpdateInterval = 50)
        {
            IncomingQueue = new Queue<NetworkMessage>();

            listenerThread = new Thread(new ThreadStart(listen));

            keepListening = false;

            this.port = port;
            this.maxClientsQueue = maxClientsQueue;
            updateInterval = serverUpdateInterval;
            
            QueueMutex = new Mutex();
        }

        public void StartListening()
        {
            keepListening = true;

            listenerThread.Start();

            Debug.Log("Listener thread started.");
        }

        public void StopListening()
        {
            keepListening = false;

            Debug.Log("Sent request stop request to the listener.");
        }

        private void listen()
        {
            TcpListener tcpServer = null;

            try
            {
                // Set the local address
                IPAddress localAddress = IPAddress.Parse("0.0.0.0");

                // Init the server
                tcpServer = new TcpListener(localAddress, port);
                
                // Start listening to client requests
                tcpServer.Start(maxClientsQueue);

                Debug.Log("Successfully started the TCP server on port " + port);

                // Buffer for incoming data
                byte[] buffer;

                while (keepListening)
                {
                    Debug.Log("Waiting for an incoming connection...");

                    while (keepListening && !tcpServer.Pending())
                    {
                        Thread.Sleep(updateInterval);
                    }

                    if (!keepListening)
                        break;

                    TcpClient tcpClient = tcpServer.AcceptTcpClient();

                    Socket socket = tcpClient.Client;

                    Debug.Log("Connected to a TCP client !");
                    Debug.Log("Connected to " + socket.RemoteEndPoint.ToString());
                    
                    //Network.AddNode(socket.RemoteEndPoint as IPEndPoint);

                    buffer = new byte[1024];

                    // Read first packet to know the expected size

                    List<byte[]> payload = new List<byte[]>();
                    int recv = socket.Receive(buffer);

                    UInt32 expectedSize = BitConverter.ToUInt32(buffer, sizeof(Message.MAGIC) + sizeof(Message.COMMAND)) + Message.MessageStructureSize;

                    payload.Add(buffer);

                    //Debug.Log("Received message: " + Encoding.Unicode.GetString(buffer, (int) Message.MessageStructureSize, recv), Debug.LoggingLevels.HighLogging);

                    while (recv < expectedSize)
                    {
                        recv += socket.Receive(buffer);

                        //Debug.Log("Received bytes: " + Encoding.ASCII.GetString(buffer, 0, recv), Debug.LoggingLevels.HighLogging);

                        payload.Add(buffer);
                    }

                    QueueMutex.WaitOne();
                    IncomingQueue.Enqueue(new NetworkMessage(payload, socket.RemoteEndPoint as IPEndPoint));
                    QueueMutex.ReleaseMutex();
                    
                    tcpClient.Close();

                    Debug.Log("Done receiving data; connection closed.");
                }

                Debug.Log("Listener: received stop request; stopping...");
            }
            catch (SocketException e) {
                Debug.Log("SocketException while listening: " + e);
            }
            catch (IOException e) {
                Debug.Log("IOException while listening: " + e);
            }
            finally {
                // Register the listener as stopped, so it doesn't get stuck in case of errors
                keepListening = false;

                tcpServer.Stop();

                Debug.Log("Stopped the TCP server");
            }
        }
    }
}
