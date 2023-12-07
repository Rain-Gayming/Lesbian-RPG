using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class ChestInteractable : Interactable
{
    [BoxGroup("Chest")]
    public LootPool lootPool;
    [BoxGroup("Chest")]
    public string chestName;
    [BoxGroup("Chest")]
    public bool hasBeenOpened;
    [BoxGroup("Chest")]
    public List<InventoryItem> chestItems;

    public override void Interact()
    {
        if(!hasBeenOpened){
            chestItems = lootPool.GenerateLoot();
            hasBeenOpened = true;
        }


        Inventory.instance.inChest = true;
        Inventory.instance.inAlter = false;
        Inventory.instance.OpenChest(chestItems, this);
        MenuManager.instance.ChangeMenuWithPause(Inventory.instance.inventoryMenu);

        for (int i = 0; i < Inventory.instance.chestSlots.Count; i++)
        {
            Inventory.instance.chestSlots[i].UpdateItem();
        }
    }
}
