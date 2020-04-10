using UnityEngine;

public class CameraOffset : MonoBehaviour
{
    public Vector3 Offset;
    public GameObject marble;
    
    public void Update() {
        if(Input.GetMouseButton(1)) {
            Debug.Log("mouse");
            Debug.Log(Input.mousePosition);
            transform.eulerAngles = new Vector3(
                transform.eulerAngles.x,
                Input.mousePosition.x % 360,
                transform.eulerAngles.z
            );
        }
    }

    public void FixedUpdate() {
        gameObject.transform.position = marble.transform.position + Offset;
    }
}
