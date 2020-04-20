using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {  

    public uint ObstaclesNumber;
    public List<GameObject> Obstacles;
    private string hash = "1cf28eb9ac221d8cd15298b9ae63eca910b536a5234c133c7e364b29a4e39d21";

    private GameObject ObstaclesContainer;
    public void Start() {
        ObstaclesContainer = GameObject.FindGameObjectWithTag("ObstaclesContainer");

        Random.InitState(int.Parse(hash.Substring(0, hash.Length/8), System.Globalization.NumberStyles.HexNumber));
        
        for(int i = 0; i < ObstaclesNumber; i++) {
            Debug.Log(Obstacles);
            for(int j = 0; j < Obstacles.Count; j++) {
                float x = Random.Range(-175, -40);
                float z = Random.Range(-123, 19);

                Instantiate(Obstacles[j], new Vector3(x, 15, z), new Quaternion(90, 0, 0, -90));
            }
        }
    }
}
