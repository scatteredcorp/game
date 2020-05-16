using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Mime;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class MarblePreviews : MonoBehaviour
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

    // Start is called before the first frame update
    void Start()
    {
        /*var testlist = new (string type, string color, string number)[9];
        testlist[0] = ("Elastic", "26", "Orange");
        testlist[1] = ("Ribbon", "748", "Red");
        testlist[2] = ("Ribbon", "25", "Blue");
        testlist[3] = ("Layers", "47", "Green");
        testlist[4] = ("Whirlwind", "364", "Purple");
        testlist[5] = ("Stripes", "25", "Red");
        testlist[6] = ("Stripes", "267", "Dark");
        testlist[7] = ("Soccer", "25", "None");
        testlist[8] = ("Earth", "21", "None");
        LoadInventory(testlist);*/
    }


    void LoadInventory((string type, string color, string number)[] test)
    {
        foreach (var (x, z, y) in test)
        {
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
