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
    public GameObject marblepreview;
    public GameObject colorcount;
    public GameObject image;
    public GameObject display;
    public GameObject title;
    public GameObject variants;
    public GameObject lastcolor;
    public Sprite Earth;
    public Sprite Elastic;
    public Sprite Error;
    public Sprite Layers;
    public Sprite Ribbon;
    public Sprite SoccerBall;
    public Sprite Stripes;
    public Sprite Whirlwind;
    
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
                for (int i = display.transform.childCount; i > 0; i--)
                {
                    Destroy(display.transform.GetChild(i-1).gameObject);
                }
                foreach (var VARIABLE in inventory.Marbles)
                {
                    var x = VARIABLE.Type.ToString();
                    var z = VARIABLE.Amount.ToString();
                    var y = VARIABLE.Color.ToString();
                    string _lastcolor = lastcolor.GetComponent<Text>().text;
                    if (_lastcolor != x)
                    {
                        switch (x)
                        {
                            case "Earth":
                                image.GetComponent<Image>().sprite = Earth;
                                break;
                            case "Elastic":
                                image.GetComponent<Image>().sprite = Elastic;
                                break;
                            case "Layers":
                                image.GetComponent<Image>().sprite = Layers;
                                break;
                            case "Ribbon":
                                image.GetComponent<Image>().sprite = Ribbon;
                                break;
                            case "Soccer":
                                image.GetComponent<Image>().sprite = SoccerBall;
                                break;
                            case "Stripes":
                                image.GetComponent<Image>().sprite = Stripes;
                                break;
                            case "Whirlwind":
                                image.GetComponent<Image>().sprite = Whirlwind;
                                break;
                            default:
                                image.GetComponent<Image>().sprite = Error;
                                break;
                        }
                        title.GetComponent<Text>().text = x;
                        if (y != "None")
                        {
                            colorcount.GetComponent<Text>().text = y + " x " + z;
                        }
                        else
                        {
                            colorcount.GetComponent<Text>().text = "x " + z;
                        }
                        for (int i = variants.transform.childCount; i > 0; i--)
                        {
                            DestroyImmediate(variants.transform.GetChild(i-1).gameObject);
                        }
                        Instantiate(colorcount, variants.transform);
                        lastcolor.GetComponent<Text>().text = x;
                        Instantiate(marblepreview, display.transform);
                    }
                    else
                    {
                        DestroyImmediate(display.transform.GetChild(display.transform.childCount - 1).gameObject);
                        colorcount.GetComponent<Text>().text = y + " x " + z;
                        Instantiate(colorcount, variants.transform);
                        Instantiate(marblepreview, display.transform);
                    }
                }
            }
        }
    }
}
