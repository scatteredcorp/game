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

        byte[] addr = Base58Encode.Decode("xshhxPSH42N4d9VZ4VNaxgPGVWKws24taq");
        for(int i = 0; i < addr.Length; i++) {
            Debug.Log(addr[i]);
        }

        byte[] msg = Utils.CreateMessage(Message.COMMAND.UnityGetInventory, addr, Message.MAGIC.LocalNetwork);

        Network.Network.SendData(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 27497), msg, ref addMarbleReturnCode);
        waitingForAnswer = true;

        Debug.Log("Requested inventory from core...");
    }

    public void YO() {
        // Time.timeScale = 2;
    }

    private void Update()
    {
        if(GlobalController.Listener == null) return;

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
                uint dummy = 0;
                Contracts.Placement inventory = Contracts.ContractHelper.DeserializePlacement(networkMessage.Payload, ref dummy);
                inventory.PrettyPrint();
                Debug.Log(inventory.Marbles[0].Type.ToString());
                Debug.Log("Received inventory response.");
            }
        }
    }
}
