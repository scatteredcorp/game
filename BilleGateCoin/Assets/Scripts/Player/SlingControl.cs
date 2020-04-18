using System;
using System.Collections;
using System.Collections.Generic;
using Boo.Lang;
using TMPro;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.UI;

public class SlingControl : MonoBehaviour
{
    public GameObject sling;
    public GameObject notTheSling;
    public GameObject slingGroup;
    public GameObject powerPreview;
    public GameObject powerPreviewArrow;
    public Camera camera;
    public TextMeshPro powerPreviewNumber;
    private bool wasPressed;
    private float power;
    private bool shootIsRunning;

    private void Start()
    {
        powerPreview.SetActive(false);
        powerPreviewArrow.SetActive(false);
        wasPressed = false;
        shootIsRunning = false;
        power = 0;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            powerPreview.SetActive(true);
            powerPreviewArrow.SetActive(true);
            powerPreviewNumber.enabled = true;
            if ((notTheSling.transform.localPosition.z < 0))
            {
                notTheSling.transform.localPosition = Vector3.zero;
                powerPreview.transform.localPosition = Vector3.zero;
                powerPreviewArrow.transform.localPosition = Vector3.zero;
                powerPreview.transform.localScale = new Vector3(0.05f, 1f, 0.01f);
                power = 0;
            }
            else
            {
                if ((notTheSling.transform.localPosition.z > 30))
                {
                    notTheSling.transform.Translate(0, -1 / 20f * 1.8f, -0.1f * 1.8f);
                    powerPreview.transform.Translate(0, 0, -1/200f);
                    powerPreviewArrow.transform.Translate(0, 0, 1/100f);
                    powerPreview.transform.localScale += new Vector3(0, 0, -1/1000f);
                    power -= 0.1f;
                }
                else
                {
                    notTheSling.transform.Translate(0, -Input.GetAxis("Mouse Y") / 2f * 1.8f,
                        -Input.GetAxis("Mouse Y") * 1.8f);
                    powerPreview.transform.Translate(0, 0, -Input.GetAxis("Mouse Y")/20f);
                    powerPreviewArrow.transform.Translate(0, 0, Input.GetAxis("Mouse Y")/10f);
                    powerPreview.transform.localScale += new Vector3(0, 0, -Input.GetAxis("Mouse Y")/100f);
                    power += -Input.GetAxis("Mouse Y");
                    if (power >= 0)
                    {
                        if (power * 6 <= 100)
                        {
                            powerPreviewNumber.text = Convert.ToString(Convert.ToInt32(power * 6)) + "%";
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
            if (Input.GetMouseButtonUp(0) && Physics.Raycast(transform.position, -Vector3.up, 0.6f))
            {
                StartCoroutine(Shoot());
                StartCoroutine(LaunchEffect());
            }
            else
            {
                if (!shootIsRunning)
                {
                    var returnPoint = slingGroup.transform.position;
                    sling.transform.position = returnPoint;
                    notTheSling.transform.position = returnPoint;
                    powerPreview.transform.position = returnPoint;
                    powerPreviewArrow.transform.position = returnPoint;
                    powerPreview.transform.localScale = new Vector3(0.05f, 1f, 0.01f);
                    power = 0;
                }
            }
            powerPreview.SetActive(false);
            powerPreviewArrow.SetActive(false);
            powerPreviewNumber.enabled = false;
        }
    }
    
    IEnumerator Shoot()
    {
        shootIsRunning = true;
        float timer = 0.1f;
        while (timer > 0)
        {
            sling.transform.position = notTheSling.transform.position;
            timer -= Time.deltaTime;
            yield return null;
        }
        var returnPoint = slingGroup.transform.position;
        sling.transform.position = returnPoint;
        notTheSling.transform.position = returnPoint;
        powerPreview.transform.position = returnPoint;
        powerPreviewArrow.transform.position = returnPoint;
        powerPreview.transform.localScale = new Vector3(0.05f, 1f, 0.01f);
        power = 0;
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
