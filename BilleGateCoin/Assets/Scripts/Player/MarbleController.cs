using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DPhysics;

public class MarbleController : MonoBehaviour {

    public Rigidbody rb;

    public const float ForceIntensity = 1f;
    private const float MaxForce = 100.00f;
    private GameObject cameraContainer; 

    private Vector3 mousePositionOne;
    private Vector3 mousePositionTwo; 
    public void Start() {
        cameraContainer = GameObject.FindGameObjectWithTag("CameraController");
    }

    public void OnMouseDown() {
        Debug.Log("test");
        mousePositionOne = Input.mousePosition;
    }

    public void OnMouseUp() {
        mousePositionTwo = Input.mousePosition;
        Vector3 force = ComputeForce();
        rb.AddForce(force);
    }

    private Vector3 ComputeForce() {

        float intensity = Mathf.Abs(mousePositionOne.y - mousePositionTwo.y);
        Vector3 force = new Vector3(
            Mathf.Sin(cameraContainer.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) * ForceIntensity * intensity,
            0,
            Mathf.Cos(cameraContainer.transform.rotation.eulerAngles.y * Mathf.Deg2Rad) * ForceIntensity * intensity
        );

        if(force.x < -MaxForce) force.x = -MaxForce;
        if(force.x > MaxForce) force.x = MaxForce;

        if(force.y < -MaxForce) force.y = -MaxForce;
        if(force.y > MaxForce) force.y = MaxForce;

        return force;
    }

    void OnCollisionEnter(Collision collision) {
        Debug.Log(collision.collider.name);
    }
}
