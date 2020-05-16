using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Network;

public class GlobalController : MonoBehaviour
{
    private static Listener listener;

    internal static Listener Listener { get => listener; set => listener = value; }

    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        Listener = new Listener(27496, 2);
        Listener.StartListening();
    }

    private void OnDestroy()
    {
        listener.StopListening();
    }
}
