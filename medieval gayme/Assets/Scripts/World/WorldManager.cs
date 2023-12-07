using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using System.IO;
using System;
using JetBrains.Annotations;

public class WorldManager : MonoBehaviour
{
    [BoxGroup("References")]
    public ItemDatabase itemDatabase;
    [BoxGroup("References")]
    public WorldSave worldSave;

    [BoxGroup("interactables")]
    public List<ChestInteractable> chests;
    
    [BoxGroup("Saving")]
    public ChestSave chestSave;
    [BoxGroup("Saving")]
    public string worldSavePath;
    public void Awake()
    {
    }    

    void Start()
    {
        worldSavePath = Application.persistentDataPath + "/" + MainMenuManager.instance.playerName + "_World.json";      
        if(File.Exists(worldSavePath)){
            LoadWorld();
        }   
    }

    [Button]    
    public void FindChests()
    {
        ChestInteractable[] chestArray = FindObjectsOfType<ChestInteractable>();

        for (int i = 0; i < chestArray.Length; i++)
        {
            if(!chests.Contains(chestArray[i]))
                chests.Add(chestArray[i]);
        }
    }
    public void SaveWorld()
    {
        worldSave.chestsSaved.Clear();
        for (int i = 0; i < chests.Count; i++)
        {
            List<SaveInventoryItem> chestSaveItems = new List<SaveInventoryItem>();
            for (int s = 0; s < chests[i].chestItems.Count; s++)
            {
                SaveInventoryItem newSaveItem = new SaveInventoryItem(chests[i].chestItems[s].item.itemName, chests[i].chestItems[s].amount, s);
                chestSaveItems.Add(newSaveItem);
            }
            
            worldSave.chestsSaved.Add(new ChestSave(i, chests[i].chestName, chestSaveItems ,chests[i].hasBeenOpened));
        }

        string jsonString = JsonUtility.ToJson(worldSave, true);
        File.WriteAllText(worldSavePath, jsonString);
    }

    public void LoadWorld()
    {           
        if(!File.Exists(worldSavePath)){
            worldSavePath = Application.persistentDataPath + "/" + MainMenuManager.instance.playerName + "_World.json";  
            return;
        }    

        string fileContents = File.ReadAllText(worldSavePath);

        worldSave = JsonUtility.FromJson<WorldSave>(fileContents);

        for (int i = 0; i < worldSave.chestsSaved.Count; i++)
        {
            for (int d = 0; d < itemDatabase.items.Count; d++)
            {       
                for (int ci = 0; ci < worldSave.chestsSaved[i].chestItems.Count; ci++)
                {
                    if(worldSave.chestsSaved[i].chestItems[ci].item == itemDatabase.items[d].itemName){
                        InventoryItem ii = new InventoryItem(itemDatabase.items[d], worldSave.chestsSaved[i].chestItems[ci].amount);
                        chests[i].chestItems.Add(ii);
                        print(chests[i].name + " has " + ii.item.itemName + " of amount " + ii.amount);
                    }   
                }
            }
            chests[i].hasBeenOpened = worldSave.chestsSaved[i].chestHasBeenOpened;
        }
    }
}

[System.Serializable]
public class WorldSave
{
    public List<ChestSave> chestsSaved;
}
