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
    public GameObject imaginaryTarget;
    public GameObject powerPreview;
    public GameObject powerPreviewArrow;
    public Camera camera;
    public TextMeshPro powerPreviewNumber;
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
            if ((pow < 0))
            {
                powerPreview.transform.localPosition = Vector3.zero;
                powerPreviewArrow.transform.localPosition = Vector3.zero;
                powerPreview.transform.localScale = new Vector3(0.05f, 1f, 0.01f);
                pow = 0;
            }
            else
            {
                if ((pow * 6 > 100))
                {
                    powerPreview.transform.Translate(0, 0, -1 / 200f);
                    powerPreviewArrow.transform.Translate(0, 0, 1 / 100f);
                    powerPreview.transform.localScale += new Vector3(0, 0, -1 / 1000f);
                    pow -= 0.1f;
                }
                else
                {
                    powerPreview.transform.Translate(0, 0, -Input.GetAxis("Mouse Y") / 20f);
                    powerPreviewArrow.transform.Translate(0, 0, Input.GetAxis("Mouse Y") / 10f);
                    powerPreview.transform.localScale += new Vector3(0, 0, -Input.GetAxis("Mouse Y") / 100f);
                    pow += -Input.GetAxis("Mouse Y");
                    if (pow >= 0)
                    {
                        if (pow * 6 <= 100)
                        {
                            powerPreviewNumber.text = Convert.ToString(Convert.ToInt32(pow * 6)) + "%";
                        }
                        else
                        {
                            powerPreviewNumber.text = "100%";
                        }
                    }
                    else
                    {
                        powerPreviewNumber.text = "0%";
                    }
                }
            }
        }
        else
        {
            if (wasPressed && Physics.Raycast(transform.position, -Vector3.up, 0.6f))
            {
                wasPressed = false;
                StartCoroutine(Shoot());
                StartCoroutine(LaunchEffect());
            }
            else
            {
                if (!shootIsRunning)
                {
                    powerPreview.SetActive(false);
                    powerPreviewArrow.SetActive(false);
                    powerPreviewNumber.enabled = false;
                    powerPreview.transform.localPosition = Vector3.zero;
                    powerPreviewArrow.transform.localPosition = Vector3.zero;
                    powerPreview.transform.localScale = new Vector3(0.05f, 1f, 0.01f);
                    pow = 0;
                }
            }
        }
    }
    
    IEnumerator Shoot()
    {
        shootIsRunning = true;
        for (float timer = 0f; timer < 0.1f; timer+=Time.deltaTime)
        {
            player.GetComponent<Rigidbody>().AddForce((transform.forward * 4 + transform.up * 2) * pow * Power);
            player.GetComponent<Rigidbody>().AddTorque(transform.right * pow * Power * 0.8f);
            yield return null;
        }
        shootIsRunning = false;
    }

    IEnumerator LaunchEffect()
    {
        float originalFOV = camera.fieldOfView;

        camera.fieldOfView *= 0.9f;
        while (camera.fieldOfView < originalFOV + 0.01f)
        {
            camera.fieldOfView *= 1.004f;
            yield return null;
        }

        camera.fieldOfView = originalFOV;
    }    
}