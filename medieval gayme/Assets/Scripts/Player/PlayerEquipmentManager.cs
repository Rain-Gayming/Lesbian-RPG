using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Unity.VisualScripting;

public class PlayerEquipmentManager : MonoBehaviour
{
    public static PlayerEquipmentManager instance;
    [BoxGroup("Stats")]
    public int defence;
    [BoxGroup("Stats")]
    public float attackModifier = 1;

    [BoxGroup("Equipment Slots")]
    public InventorySlot headSlot;
    [BoxGroup("Equipment Slots")]
    public InventorySlot chestSlot;
    [BoxGroup("Equipment Slots")]
    public InventorySlot legsSlot;
    [BoxGroup("Equipment Slots")]
    public InventorySlot feetSlot;
    [BoxGroup("Equipment Slots/Rings")]
    public InventorySlot ringSlot1;
    [BoxGroup("Equipment Slots/Rings")]
    public InventorySlot ringSlot2;

    void Awake() {
        instance = this;
    }

    [Button]
    public void UpdateStats()
    {
        defence = 0;
#region Defence
        defence += headSlot.currentItem.item ? headSlot.currentItem.item.armour : 0;
        defence += chestSlot.currentItem.item ? chestSlot.currentItem.item.armour : 0;
        defence += legsSlot.currentItem.item ? legsSlot.currentItem.item.armour : 0;
        defence += feetSlot.currentItem.item ? feetSlot.currentItem.item.armour : 0;
        

        defence += ringSlot1.currentItem.item ? ringSlot1.currentItem.item.armour : 0;
        defence += ringSlot2.currentItem.item ? ringSlot2.currentItem.item.armour : 0;
#endregion
    }

    public void QuickEquip(InventorySlot slotFrom, InventoryItem item)
    {
        InventoryItem storedItem = item;
        switch (item.item.equipmentType)
        {
            case EEquipmentType.head:
                slotFrom.currentItem = headSlot.currentItem;
                headSlot.currentItem = storedItem;
                headSlot.UpdateItem();
            break;
            case EEquipmentType.chest:
                slotFrom.currentItem = chestSlot.currentItem;
                chestSlot.currentItem = storedItem;
                chestSlot.UpdateItem();
            break;
            case EEquipmentType.legs:
                slotFrom.currentItem = legsSlot.currentItem;
                legsSlot.currentItem = storedItem;
                legsSlot.UpdateItem();
            break;
            case EEquipmentType.feet:
                slotFrom.currentItem = feetSlot.currentItem;
                feetSlot.currentItem = storedItem;
                feetSlot.UpdateItem();
            break;
        }
    }
}
