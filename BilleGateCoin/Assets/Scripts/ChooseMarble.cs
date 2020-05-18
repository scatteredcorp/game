using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseMarble : MonoBehaviour
{
    public GameObject type;
    public GameObject color;
    public GameObject marblepreview;
    public GameObject amount;
    public GameObject marblepreviewslot;
    public GameObject marblepreviewslottext;

    public void SelectThisMarble()
    {
        var _type = type.GetComponent<Text>().text;
        var _color = color.GetComponent<Text>().text.Substring(0, color.GetComponent<Text>().text.IndexOf("x"));
        var _amount = amount.GetComponent<Text>().text;
        if (_color != "")
        {
            marblepreviewslottext.GetComponent<Text>().text = _type + " - " + _color.Substring(0, _color.Length-1) + " x " + _amount;
        }
        else
        {
            marblepreviewslottext.GetComponent<Text>().text = _type + " x " + _amount;
        }

        Instantiate(marblepreviewslot, marblepreview.transform);
        amount.GetComponent<Text>().text = "";
    }
}
