using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private bool grounded;
    private Rigidbody rb;
    private int i = 0;
    void Awake() {
        rb = gameObject.GetComponent<Rigidbody>();
        Time.timeScale = 1;
        rb.AddForce(new Vector3(100, 0, 300));
    }

    void FixedUpdate() {
        if(rb.IsSleeping() && i < 5) {
            rb.AddForce(new Vector3(100, 0, 300));
            i++;
        }
    }

    void OnCollisionEnter(Collision collision) {
        if(!grounded) {
            if(collision.collider.name == "Map") {
                rb.useGravity = false;
                rb.constraints = RigidbodyConstraints.FreezePositionY;
                grounded = true;
            }
        }

        if(collision.collider.name == "Opponent") {
            Debug.Log("SUCCESS");
        }
    }
}
