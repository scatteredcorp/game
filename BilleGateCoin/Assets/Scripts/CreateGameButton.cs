using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateGameButton : MonoBehaviour
{   
    
    public void AddMarble() {
        GameObject createGameState = GameObject.FindGameObjectWithTag("CreateGameState");
        
        createGameState.GetComponent<CreateGameState>().marbles.Add("HELLO");
    }
}
