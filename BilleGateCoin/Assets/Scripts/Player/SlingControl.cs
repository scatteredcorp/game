using System;
using System.Collections;
using System.Collections.Generic;
using Boo.Lang;
using UnityEngine;

public class SlingControl : MonoBehaviour
{
    public GameObject sling;
    public GameObject notTheSling;
    public GameObject slingGroup;
    public GameObject powerPreview;
    public GameObject powerPreviewArrow;
    private int wasPressedSince;

    private void Start()
    {
        powerPreview.SetActive(false);
        powerPreviewArrow.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            wasPressedSince = 3;
            powerPreview.SetActive(true);
            powerPreviewArrow.SetActive(true);
            if ((notTheSling.transform.localPosition.z >= 0))
            {
                notTheSling.transform.Translate(0, -Input.GetAxis("Mouse Y") * 2f, -Input.GetAxis("Mouse Y") * 2f);
                powerPreview.transform.Translate(0, 0, -Input.GetAxis("Mouse Y")/20f);
                powerPreviewArrow.transform.Translate(0, 0, Input.GetAxis("Mouse Y")/10f);
                powerPreview.transform.localScale += new Vector3(0, 0, -Input.GetAxis("Mouse Y")/100f);
            }
            else
            {
                notTheSling.transform.localPosition = Vector3.zero;
                powerPreview.transform.localPosition = Vector3.zero;
                powerPreviewArrow.transform.localPosition = Vector3.zero;
                powerPreview.transform.localScale = new Vector3(0.05f, 1f, 0.01f);
            }
        }
        else
        {
            if (wasPressedSince > 0 && Physics.Raycast(transform.position, -Vector3.up, 0.6f))
            {
                Shoot();
                powerPreview.SetActive(false);
                powerPreviewArrow.SetActive(false);
            }
            else
            {
                var returnPoint = slingGroup.transform.position;
                sling.transform.position = returnPoint;
                notTheSling.transform.position = returnPoint;
                powerPreview.transform.position = returnPoint;
                powerPreviewArrow.transform.position = returnPoint + Vector3.zero;
                powerPreview.transform.localScale = new Vector3(0.05f, 1f, 0.01f);
            }
        }
    }

    void Shoot()
    {
        sling.transform.position = notTheSling.transform.position;
        wasPressedSince -= 1;
    }
}
