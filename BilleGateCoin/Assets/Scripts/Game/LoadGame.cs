using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour
{
    private string Scene = "Game";

    public Vector3 OpponentPosition;
    public string GameHash;
    public void Load() {
        GameState.OpponentPosition = OpponentPosition;
        GameState.GameHash = GameHash;
        
        SceneManager.LoadScene(Scene);
    }
}
