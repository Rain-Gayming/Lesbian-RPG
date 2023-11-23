using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Sirenix.OdinInspector;
using TMPro;
using System.IO;

public class MainMenu : MonoBehaviour
{
    public string[] paths;
    public FileInfo[] files;
    [BoxGroup("Create Character")]
    public TMP_InputField nameInput;

    [BoxGroup("Load Character")]
    public GameObject characterButtonPrefab;    
    [BoxGroup("Load Character")]
    public Transform characterButtonPoint;
    
    public void Start()
    {
        DirectoryInfo info = new DirectoryInfo(Application.persistentDataPath);
        files = info.GetFiles("*_PlayerSaveData.json");
        for (int i = 0; i < files.Length; i++)
        {
            GameObject newButton = Instantiate(characterButtonPrefab);
            newButton.transform.SetParent(characterButtonPoint);

            string characterName = files[i].ToString().Split('_')[0];            
            paths = characterName.Split('\\');
            string result = paths[7];

            newButton.GetComponent<LoadCharacterButton>().characterName = result;
            newButton.transform.localScale = Vector3.one;

            print(result);
        }
    }

    public void CreateCharacter()
    {
        if(nameInput.text != string.Empty){
            MainMenuManager.instance.playerName = nameInput.text;
            SceneManager.LoadScene("World");
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
