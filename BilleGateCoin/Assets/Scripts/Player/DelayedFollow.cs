using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DelayedFollow : MonoBehaviour
{
    public GameObject player;

    IEnumerator DelayedCamera()
    {
        Vector3 playerPos = player.transform.localPosition;
        yield return new WaitForSeconds(0.1f);
        transform.localPosition = playerPos;
    }

    private void FixedUpdate()
    {
        StartCoroutine(DelayedCamera());
    }
}
