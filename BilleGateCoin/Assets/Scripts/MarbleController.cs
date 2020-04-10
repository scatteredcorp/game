using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DPhysics;

public class MarbleController : MonoBehaviour {

    private Body body;

    private const float ForceIntensity = 0.01f;
    private const float MaxForce = 6.00f;

    private Vector2 mousePositionOne;
    private Vector2 mousePositionTwo; 
    public void Start() {
        body = gameObject.GetComponent<Body>();
    }

    public void OnMouseDown() {
        mousePositionOne = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
    }

    public void OnMouseUp() {
        mousePositionTwo = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        Vector2d force = ComputeForce();        
        Debug.Log(force);
        body.ApplyForce(ref force);
    }

    private Vector2d ComputeForce() {
        Vector2d force = new Vector2d(
            FInt.Create(
                ForceIntensity * (mousePositionOne.x - mousePositionTwo.x)
            ), 
            FInt.Create(
                ForceIntensity * (mousePositionOne.y - mousePositionTwo.y)
            )
        );

        if(force.x.ToFloat() < -MaxForce) force.x = FInt.Create(-MaxForce);
        if(force.x.ToFloat() > MaxForce) force.x = FInt.Create(MaxForce);

        if(force.y.ToFloat() < -MaxForce) force.y = FInt.Create(-MaxForce);
        if(force.y.ToFloat() > MaxForce) force.y = FInt.Create(MaxForce);

        return force;
    }
}
