using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDrag : MonoBehaviour
{
    private void FixedUpdate()
    {
        if (Math.Abs(GetComponent<Rigidbody>().velocity.x) +
            Math.Abs(GetComponent<Rigidbody>().velocity.z) < 0.1)
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
}
