using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Sirenix.OdinInspector;
using UnityEngine.UI;
using System.IO;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;
    public PlayerSaveData playerSaveData;
    public string playerSavePath;   
    [BoxGroup("Refernces")]
    public SpellBook spellBook;
    [BoxGroup("Refernces")]
    public GameObject player;
    [BoxGroup("Refernces")]
    public InventorySaving inventorySave;
    [BoxGroup("Refernces")]
    public WorldManager worldManager;

    [BoxGroup("Saving")]
    public float autoSaveTimer;
    [BoxGroup("Saving")]
    public float autoSaveTime = 300;
    
    public void Awake() 
    {        
        instance = this;
    }
    
    public void Start()
    {
        autoSaveTimer = autoSaveTime;
        playerSavePath = Application.persistentDataPath + "/"+ MainMenuManager.instance.playerName + 
            "_PlayerSaveData.json";
        Load();
        
    }

    [Button]
    public void Save()
    {
        spellBook.SaveSpells();
        inventorySave.SaveInventory();
        worldManager.SaveWorld();
        SavePlayer();
    }

    public void Load()
    {
        spellBook.LoadSpells();
        inventorySave.LoadInventory();
        worldManager.LoadWorld();
        LoadPlayer();
    }

    void Update()
    {
        autoSaveTimer -= Time.deltaTime;

        if(autoSaveTime <= 0){
            SavePlayer();
        }    
    }
    public void SavePlayer()
    {
        autoSaveTimer = autoSaveTime;
        playerSaveData.position = player.transform.position;

        string jsonString = JsonUtility.ToJson(playerSaveData, true);
        File.WriteAllText(playerSavePath, jsonString);

    }

    [Button]
    public void LoadPlayer()
    {
        string fileContents = File.ReadAllText(playerSavePath);

        PlayerSaveData newSaveData = JsonUtility.FromJson<PlayerSaveData>(fileContents);
    }
}

[System.Serializable]
public class PlayerSaveData
{
    public Vector3 position;
}