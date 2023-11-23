using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager instance;

    public string playerName;

    void Awake() {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void Update()
    {
        if(PlayerManager.instance){
            if(PlayerManager.instance.playerName != playerName){
                PlayerManager.instance.playerName = playerName;
            }
        }
    }
}
