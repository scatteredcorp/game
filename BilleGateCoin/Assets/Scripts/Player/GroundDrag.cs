using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class GroundDrag : MonoBehaviour
{
    public float SpeedLimit;
    private void Update()
    {
        if (Math.Abs(GetComponent<Rigidbody>().velocity.x) +
            Math.Abs(GetComponent<Rigidbody>().velocity.z) < SpeedLimit)
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
}
