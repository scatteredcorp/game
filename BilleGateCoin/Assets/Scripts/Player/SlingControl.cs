using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingControl : MonoBehaviour
{
    public GameObject sling;
    public GameObject slingGroup;
    public SpringJoint springJoint;

    private bool wasClicked;
    
    public enum RotationAxis
    {
        MouseX = 1,
        MouseY = 2
    }
    
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            sling.transform.Translate(0, -Input.GetAxis("Mouse Y") * 0.1f, -Input.GetAxis("Mouse Y") * 0.2f);
        }
        else
        {
            sling.transform.position = slingGroup.transform.position;
        }
    }
}
