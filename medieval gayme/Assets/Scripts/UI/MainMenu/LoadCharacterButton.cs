using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadCharacterButton : MonoBehaviour
{
    [BoxGroup("UI")]
    public TMP_Text characterNameText;

    [BoxGroup("Charcter")]
    public string characterName;

    public void Update() 
    {
        if(characterNameText.text != characterName){
            characterNameText.text = characterName;
        }
    }

    public void LoadCharacter()
    {
        MainMenuManager.instance.playerName = characterName;
        MainMenuManager.instance.LoadCharacter();
    }
}
