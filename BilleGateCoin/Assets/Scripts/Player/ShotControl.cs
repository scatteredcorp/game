using System;
using System.Collections;
using System.Collections.Generic;
using Boo.Lang;
using TMPro;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.UI;

public class ShotControl : MonoBehaviour
{
    public GameObject player;
    public GameObject powerPreview;
    public GameObject powerPreviewArrow;
    public Camera camera;
    public TextMeshPro powerPreviewNumber;
    public float ArrowLength;
    public float Power;
    private bool wasPressed;
    private float pow;
    private bool shootIsRunning;

    private void Start()
    {
        powerPreview.SetActive(false);
        powerPreviewArrow.SetActive(false);
        wasPressed = false;
        shootIsRunning = false;
        pow = 0;
    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            wasPressed = true;
            powerPreview.SetActive(true);
            powerPreviewArrow.SetActive(true);
            powerPreviewNumber.enabled = true;
            pow += -Input.GetAxis("Mouse Y") * 9;
            if ((pow < 0))
            {
                pow = 0;
            }
            else
            {
                if ((pow > 100))
                {
                    pow = 100;
                }
            }
        }
        else
        {
            powerPreview.SetActive(false);
            powerPreviewArrow.SetActive(false);
            powerPreviewNumber.enabled = false;
            if (wasPressed)
            {
                wasPressed = false;
                if (Physics.Raycast(transform.position, -Vector3.up, 0.6f))
                {
                    Shoot();
                    StartCoroutine(LaunchEffect());
                }
            }
            else
            {
                if (!shootIsRunning)
                {
                    pow = 0;
                }
            }
        }

        powerPreview.transform.localPosition = new Vector3(0f, pow / 400f, pow / 200f) * ArrowLength;
        powerPreviewArrow.transform.localPosition = new Vector3(0, pow / 200f, pow / 100f) * ArrowLength;
        powerPreview.transform.localScale = new Vector3(0, 0, pow / 900f) * ArrowLength + new Vector3(0.05f, 1f, 0.01f);
        powerPreviewNumber.text = Convert.ToString(Convert.ToInt32(pow)) + "%";
    }

    void Shoot()
    {
        shootIsRunning = true;
        Vector3 force = (transform.forward * 4 + transform.up * 2) * pow * Power;
        Vector3 torque = new Vector3(force.z, 0, -force.x) * 0.02f;

        player.GetComponent<Rigidbody>().AddForce(force);
        player.GetComponent<Rigidbody>().AddTorque(torque);
        shootIsRunning = false;
    }

    IEnumerator LaunchEffect()
    {
        float originalFOV = camera.fieldOfView;

        camera.fieldOfView *= 0.9f;
        while (camera.fieldOfView < originalFOV - 0.01f)
        {
            camera.fieldOfView *= 1.004f;
            yield return null;
        }
        camera.fieldOfView = originalFOV;
    }    
}