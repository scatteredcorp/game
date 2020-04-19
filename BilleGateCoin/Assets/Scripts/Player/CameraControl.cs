using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraControl : MonoBehaviour
{
    public float CameraSensivity;
    void FixedUpdate()
    {
        if (Input.GetMouseButton(1))
        {
            transform.Rotate(0, Input.GetAxis("Mouse X") * CameraSensivity, 0);
        }
    }
}
