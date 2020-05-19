using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseMarble : MonoBehaviour
{
    public GameObject type;
    public GameObject color;
    public GameObject marblepreview;
    public InputField amount;
    public GameObject marblepreviewslot;
    public GameObject marblepreviewslottext;

    public void SelectThisMarble()
    {
        var _type = type.GetComponent<Text>().text;
        var _color = color.GetComponent<Text>().text.Substring(0, color.GetComponent<Text>().text.IndexOf("x"));
        var _amount = amount.text;
        int _availableamount = Convert.ToInt32(color.GetComponent<Text>().text.Substring(_color.Length + 2));
        var exists = false;
        int existsinamount = 0;
        for (int i = 0; i < marblepreview.transform.childCount; i++)
        {
            var selection = marblepreview.transform.GetChild(i).GetComponentInChildren<Text>().text;
            var selectedtype = selection.Substring(0, selection.IndexOf(" "));
            if (selectedtype == _type)
            {
                if (selectedtype != "Earth" && selectedtype != "Soccer")
                {
                    var selectedcolor = selection.Substring(selection.IndexOf("-") + 2, selection.IndexOf("x") - 1 - selection.IndexOf("-") - 2);
                    if (selectedcolor == _color.Substring(0, _color.Length - 1))
                    {
                        exists = true;
                    }
                }
                else
                {
                    exists = true;
                }
            }

            if (exists)
            {
                existsinamount = Convert.ToInt32(selection.Substring(selection.IndexOf("x") + 2));
                if (Convert.ToInt32(_amount) + existsinamount <= _availableamount && Convert.ToInt32(_amount) + existsinamount >= 0)
                {
                    var temp = Convert.ToInt32(_amount) + existsinamount;
                    if (selectedtype != "Earth" && selectedtype != "Soccer")
                    {
                        marblepreview.transform.GetChild(i).GetComponentInChildren<Text>().text = selectedtype + " - " + _color + "x " + temp;
                    }
                    else
                    {
                        marblepreview.transform.GetChild(i).GetComponentInChildren<Text>().text = selectedtype + " x " + temp;
                    }
                }
                break;
            }
        }

        if (!exists)
        {
            if (Convert.ToInt32(_amount) <= _availableamount && Convert.ToInt32(_amount) >= 0)
            {
                if (_color != "")
                {
                    marblepreviewslottext.GetComponent<Text>().text = _type + " - " + _color + "x " + _amount;
                }
                else
                {
                    marblepreviewslottext.GetComponent<Text>().text = _type + " x " + _amount;
                }
                Instantiate(marblepreviewslot, marblepreview.transform);
            }
        }
        amount.text = "";
    }
}
