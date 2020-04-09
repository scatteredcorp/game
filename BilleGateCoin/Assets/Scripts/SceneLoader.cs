using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string Scene;

    public void Load() {
        SceneManager.LoadScene(Scene);
    }
}
