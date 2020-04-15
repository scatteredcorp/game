using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraGroupControl : MonoBehaviour
{
    public GameObject player;

    void LateUpdate()
    {
        transform.position = player.transform.position;
    }
}
