using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BGC.Base58;
using Network;
using System.Net;

public class CreateGameButton : MonoBehaviour
{
    public GameObject marbleText;
    public GameObject content;

    private Network.Network.ReturnCode addMarbleReturnCode;
    bool waitingForAnswer = false;
    public void AddMarble() {

        int xOffset = 50;
        int yOffset = -50;
        int add = 50;
        GameObject createGameState = GameObject.FindGameObjectWithTag("CreateGameState");
        
        createGameState.GetComponent<CreateGameState>().marbles.Add("HELLO0");
        GameObject gb = Instantiate(marbleText, content.transform.position + new Vector3(xOffset, yOffset, 0), new Quaternion(0, 0, 0, 0)) as GameObject;
        gb.transform.parent = content.transform;
        gb.GetComponent<Text>().text = "HELLO0";

        addMarbleReturnCode = Network.Network.ReturnCode.Pending;

        byte[] msg = Utils.CreateMessage(Message.COMMAND.UnityGetInventory, Base58Encode.Decode("xg3S73psPZRsMFycvrKCTiNRv4Mi8XKdps"), Message.MAGIC.LocalNetwork);

        Network.Network.SendData(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 27497), msg, ref addMarbleReturnCode);
        waitingForAnswer = true;

        Debug.Log("Requested inventory from core...");
    }

    public void YO() {
        // Time.timeScale = 2;
    }

    private void Update()
    {
        if (waitingForAnswer && GlobalController.Listener.IncomingQueue.Count > 0)
        {
            NetworkMessage networkMessage = GlobalController.Listener.IncomingQueue.Dequeue();

            if (networkMessage.command != Message.COMMAND.UnitySendInventory)
            {
                GlobalController.Listener.IncomingQueue.Enqueue(networkMessage);
            }
            else
            {
                waitingForAnswer = false;
                // Nuf.ParseInventory(networkMessage.Payload);
                Debug.Log("Received inventory response.");
            }
        }
    }
}
