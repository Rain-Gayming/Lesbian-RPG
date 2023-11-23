using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool paused;

    public void Awake()
    {
        instance = this;
    }
    public void Update()
    {
        if(paused){
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }else{
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        if(InputManager.instance.pause){
            InputManager.instance.pause = false;
            paused = !paused;
        }
    }

    public void Pause()
    {
        paused = true;
    }
    public void Unpause()
    {
        paused = false;
    }
}
