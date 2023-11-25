using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [BoxGroup("Loading")]
    public Menu loadingScreen;
    [BoxGroup("Loading")]
    public bool isLoading;
    [BoxGroup("Loading")]
    public Slider loadingBar;
    AsyncOperation op;
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
        if(isLoading){
            loadingBar.value = op.progress;
            if(op.isDone){
                loadingScreen.Disable();
            }
        }
    }
    
    public void LoadCharacter()
    {
        loadingScreen.Enable();
        op = SceneManager.LoadSceneAsync("World");
        isLoading = true;
    }
}
