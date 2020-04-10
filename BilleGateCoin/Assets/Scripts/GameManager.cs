using UnityEngine;
using System.Collections;
using DPhysics;

/// <summary>
/// Here's a neat example of what you can do with DPhysics that other physics engine can't.
/// Because DPhysics is deterministic, every replay and every calculation will be consistent.
/// This means you can create a perfect Newton's Cradle to demonstrate transfer of momentum!
/// </summary>
/// 
public class GameManager : MonoBehaviour
{
	public GameObject Ball;
	public GameObject Wall;
    public Vector2 SpawnPosition;

    public GameObject CameraController;

    void Start() {
        //Initializing the simulation
		Time.fixedDeltaTime = .01f; //Setting our fixed update rate
		DPhysicsManager.SimulationDelta = FInt.Create (.01d); //Syncing DPhysics's simulation rate with Unity's fixed update
		DPhysicsManager.Restitution = FInt.OneF; //Restitution of 1 results in complete conservation of momentum
		DPhysicsManager.CollisionDamp = FInt.Create(1); //No reason for collision offsets since objects aren't clumped
		DPhysicsManager.Drag = FInt.Create(0.995f); //Velocity is completely conserved
    
		Body ball = Instantiate (Ball).GetComponent<Body> (); 
        ball.Initialize (new Vector2d( FInt.Create(SpawnPosition.x), FInt.Create(SpawnPosition.y)));
        

        GameObject.FindGameObjectWithTag("CameraController").GetComponent<CameraOffset>().Init(ball);
    }

	void FixedUpdate ()
	{
		//Remember to simulate DPhysics with this call
		DPhysicsManager.Simulate ();
	}

	void Update ()
	{
		//This call will communicate with Unity's rendering and transform components
		DPhysicsManager.Visualize ();
	}

}
