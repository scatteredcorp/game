using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Network;

public class GlobalController : MonoBehaviour
{
    private static Listener listener;

    internal static Listener Listener { get => listener; set => listener = value; }

    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    void Start()
    {
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if(Listener != null) return;
        Listener = new Listener(27496, 2);
        Listener.StartListening();
    }

    private void OnApplicationQuit()
    {
        listener.StopListening();
    }
}
