using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private bool grounded;
    private Rigidbody rb;

    void Awake() {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision) {
        if(!grounded) {
            if(collision.collider.name == "Map") {
                rb.useGravity = false;
                rb.constraints = RigidbodyConstraints.FreezePositionY;
                grounded = true;
            }
        } 

        Debug.Log(collision.collider.name);
    }
}
