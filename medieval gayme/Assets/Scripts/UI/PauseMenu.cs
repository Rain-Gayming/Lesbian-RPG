using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    public void Resume()
    {
        GameManager.instance.Unpause();
    }

    public void SaveGame()
    {
        SaveManager.instance.Save();
    }

    public void LoadGame()
    {
        SaveManager.instance.LoadPlayer();
    }
    
    public void SettingsMenu()
    {
        MenuManager.instance.ChangeMenu("Settings");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
