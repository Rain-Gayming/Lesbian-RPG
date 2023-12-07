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
    public PlayerEquipmentManager equipmentManager;
    [BoxGroup("References")]
    public ItemDatabase itemDatabase;
    
    [BoxGroup("Saving")]
    public InventorySave inventorySave;
    [BoxGroup("Saving")]
    public string inventorySavePath;
    void Start()
    {
        inventorySavePath = Application.persistentDataPath + "/" + MainMenuManager.instance.playerName + "_Inventory.json";  
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

        
        for (int d = 0; d < itemDatabase.items.Count; d++)
        { 
            if(itemDatabase.items[d].itemName == inventorySave.headItem.item){
                InventoryItem ii = new InventoryItem(itemDatabase.items[d], inventorySave.headItem.amount);
                equipmentManager.headSlot.currentItem = ii;
            }
            if(itemDatabase.items[d].itemName == inventorySave.chestItem.item){
                InventoryItem ii = new InventoryItem(itemDatabase.items[d], inventorySave.chestItem.amount);
                equipmentManager.chestSlot.currentItem = ii;
            }
            if(itemDatabase.items[d].itemName == inventorySave.legsItem.item){
                InventoryItem ii = new InventoryItem(itemDatabase.items[d], inventorySave.legsItem.amount);
                equipmentManager.legsSlot.currentItem = ii;
            }
            if(itemDatabase.items[d].itemName == inventorySave.feetItem.item){
                InventoryItem ii = new InventoryItem(itemDatabase.items[d], inventorySave.feetItem.amount);
                equipmentManager.feetSlot.currentItem = ii;
            }
            
            if(itemDatabase.items[d].itemName == inventorySave.ring1Item.item){
                InventoryItem ii = new InventoryItem(itemDatabase.items[d], inventorySave.ring1Item.amount);
                equipmentManager.ringSlot1.currentItem = ii;
            }
            if(itemDatabase.items[d].itemName == inventorySave.ring2Item.item){
                InventoryItem ii = new InventoryItem(itemDatabase.items[d], inventorySave.ring2Item.amount);
                equipmentManager.ringSlot2.currentItem = ii;
            }
            
            if(itemDatabase.items[d].itemName == inventorySave.backItem.item){
                InventoryItem ii = new InventoryItem(itemDatabase.items[d], inventorySave.backItem.amount);
                equipmentManager.backSlot.currentItem = ii;
            }
        }
        
        equipmentManager.UpdateStats();
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

        if(equipmentManager.headSlot.currentItem.item)
            inventorySave.headItem = new SaveInventoryItem(equipmentManager.headSlot.currentItem.item.itemName, 1, -1);
        if(equipmentManager.chestSlot.currentItem.item)
            inventorySave.chestItem = new SaveInventoryItem(equipmentManager.chestSlot.currentItem.item.itemName, 1, -1);
        if(equipmentManager.legsSlot.currentItem.item)
            inventorySave.legsItem = new SaveInventoryItem(equipmentManager.legsSlot.currentItem.item.itemName, 1, -1);
        if(equipmentManager.feetSlot.currentItem.item)
            inventorySave.feetItem = new SaveInventoryItem(equipmentManager.feetSlot.currentItem.item.itemName, 1, -1);

        if(equipmentManager.ringSlot1.currentItem.item)
            inventorySave.ring1Item = new SaveInventoryItem(equipmentManager.ringSlot1.currentItem.item.itemName, 1, -1);
        if(equipmentManager.ringSlot2.currentItem.item)
            inventorySave.ring2Item = new SaveInventoryItem(equipmentManager.ringSlot2.currentItem.item.itemName, 1, -1);
        
        if(equipmentManager.backSlot.currentItem.item)
            inventorySave.backItem = new SaveInventoryItem(equipmentManager.backSlot.currentItem.item.itemName, 1, -1);

        string jsonString = JsonUtility.ToJson(inventorySave, true);
        File.WriteAllText(inventorySavePath, jsonString);

    }

}

[System.Serializable]
public class InventorySave
{
    public List<SaveInventoryItem> saveItems;

    public SaveInventoryItem headItem;
    public SaveInventoryItem chestItem;
    public SaveInventoryItem legsItem;
    public SaveInventoryItem feetItem;
    
    public SaveInventoryItem ring1Item;
    public SaveInventoryItem ring2Item;

    public SaveInventoryItem backItem;
}
