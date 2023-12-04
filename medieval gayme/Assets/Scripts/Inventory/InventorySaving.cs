using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using System.IO;
using System;

public class InventorySaving : MonoBehaviour
{
    [BoxGroup("References")]
    public Inventory playerInventory;
    [BoxGroup("References")]
    public ItemDatabase itemDatabase;
    
    [BoxGroup("Saving")]
    public InventorySave inventorySave;
    [BoxGroup("Saving")]
    public string inventorySavePath;
    void Start()
    {
        inventorySavePath = Application.persistentDataPath + "/" + MainMenuManager.instance.playerName + "_Inventory.json";  

        if(File.Exists(inventorySavePath)){
            LoadInventory();
        }     
    }

    [Button]
    public void LoadInventory()
    {        
        string fileContents = File.ReadAllText(inventorySavePath);

        inventorySave = JsonUtility.FromJson<InventorySave>(fileContents);

        for (int i = 0; i < inventorySave.saveItems.Count; i++)
        {
            for (int d = 0; d < itemDatabase.items.Count; d++)
            {       
                if(inventorySave.saveItems[i].item == itemDatabase.items[d].itemName){
                    for (int s = 0; s < playerInventory.inventorySlots.Count; s++)
                    {
                        if(playerInventory.inventorySlots[s].slotPos == inventorySave.saveItems[i].slotPosition){
                            InventoryItem ii = new InventoryItem(itemDatabase.items[d], inventorySave.saveItems[i].amount);
                            playerInventory.inventorySlots[s].currentItem = ii;
                            playerInventory.items.Add(ii);
                        }
                    }   
                } 
            }
        }

    }


    [Button]
    public void SaveInventory()
    {
        inventorySave.saveItems.Clear();
        for (int i = 0; i < playerInventory.items.Count; i++)
        {
            for (int s = 0; s < playerInventory.inventorySlots.Count; s++)
            {
                if(playerInventory.inventorySlots[s].currentItem == playerInventory.items[i]){
                    SaveInventoryItem newSaveItem = new SaveInventoryItem(playerInventory.items[i].item.itemName, 
                        playerInventory.items[i].amount, s);
                    inventorySave.saveItems.Add(newSaveItem);
                }
            }
        }

        string jsonString = JsonUtility.ToJson(inventorySave, true);
        File.WriteAllText(inventorySavePath, jsonString);

    }

}

[System.Serializable]
public class InventorySave
{
    public List<SaveInventoryItem> saveItems;
}
