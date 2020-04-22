using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateGameButton : MonoBehaviour
{
    public GameObject marbleText;
    public GameObject content;
    public void AddMarble() {

        int xOffset = 50;
        int yOffset = -50;
        int add = 50;
        GameObject createGameState = GameObject.FindGameObjectWithTag("CreateGameState");
        
        createGameState.GetComponent<CreateGameState>().marbles.Add("HELLO0");
        GameObject gb = Instantiate(marbleText, content.transform.position + new Vector3(xOffset, yOffset, 0), new Quaternion(0, 0, 0, 0)) as GameObject;
        gb.transform.parent = content.transform;
        gb.GetComponent<Text>().text = "HELLO0";
    }

    public void YO() {
        // Time.timeScale = 2;
    }
}
