using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentController : MonoBehaviour
{

    void Start() {
        transform.position = GameState.OpponentPosition;
    }
}
