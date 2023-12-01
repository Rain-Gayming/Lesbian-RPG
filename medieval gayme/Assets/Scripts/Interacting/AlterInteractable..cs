using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class AlterInteractable : Interactable
{
    public List<AlterItem> alterItems;
    public override void Interact()
    {
        Inventory.instance.inChest = false;
        Inventory.instance.inAlter = true;
        Inventory.instance.OpenAlter(alterItems, this);
        MenuManager.instance.ChangeMenuWithPause(Inventory.instance.inventoryMenu);

        for (int i = 0; i < Inventory.instance.chestSlots.Count; i++)
        {
            Inventory.instance.chestSlots[i].UpdateItem();
        }
    }
}


[System.Serializable]
public class AlterSlot
{

    public AlterSlotPlace slotPlace;
    public InventorySlot slot;
}

[System.Serializable]
public class AlterItem
{
    public AlterSlotPlace slotPlace;
    public InventoryItem item;
}

public enum AlterSlotPlace
{
    top,
    bottom,
    left,
    right,
    output
}