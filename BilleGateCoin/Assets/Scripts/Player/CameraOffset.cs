using UnityEngine;

public class CameraOffset : MonoBehaviour
{
    public Vector3 Offset;
    public GameObject marble;
    
    public void Update() {
        if(Input.GetMouseButton(1)) {
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
