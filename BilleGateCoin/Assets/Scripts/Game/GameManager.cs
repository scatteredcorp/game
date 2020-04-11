using UnityEngine;
public class GameManager : MonoBehaviour {  

    public uint Obstacles = 20;
    public GameObject Obstacle;
    private string hash = "8cf28eb9ac221d8cd15298b9ae63eca910b536a5234c133c7e364b29a4e39d21";

    private GameObject ObstaclesContainer;
    public void Start() {
        ObstaclesContainer = GameObject.FindGameObjectWithTag("ObstaclesContainer");

        Random.InitState(int.Parse(hash.Substring(0, hash.Length/8), System.Globalization.NumberStyles.HexNumber));
        
        for(int i = 0; i < Obstacles; i++) {
            float x = Random.Range(-54, -26);
            float z = Random.Range(-36, -8);

            Instantiate(Obstacle, new Vector3(x, 5, z), Quaternion.identity);
        }
    }
}
