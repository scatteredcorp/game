using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using BGC.Base58;
using Network;
using System.Net;

public class InventoryManager : MonoBehaviour
{
    private Network.Network.ReturnCode returnCode;
    bool waitingForAnswer = false;
    public void Start() {

        returnCode = Network.Network.ReturnCode.Pending;

        byte[] msg = Utils.CreateMessage(Message.COMMAND.UnityGetOwnInventory, new byte[0], Message.MAGIC.LocalNetwork);

        Network.Network.SendData(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 27497), msg, ref returnCode);
        waitingForAnswer = true;

        Debug.Log("Requested wallet from core...");
    }
    private void Update()
    {
        if(GlobalController.Listener == null) return;

        if (waitingForAnswer && GlobalController.Listener.IncomingQueue.Count > 0)
        {
            NetworkMessage networkMessage = GlobalController.Listener.IncomingQueue.Dequeue();

            if (networkMessage.command != Message.COMMAND.UnitySendOwnInventory)
            {
                GlobalController.Listener.IncomingQueue.Enqueue(networkMessage);
            }
            else
            {
                waitingForAnswer = false;
                uint dummy = 0;
                byte[] address = networkMessage.Payload;
                
                Contracts.Placement inventory = Contracts.ContractHelper.DeserializePlacement(networkMessage.Payload, ref dummy);
                inventory.PrettyPrint();
            }
        }
    }
}
