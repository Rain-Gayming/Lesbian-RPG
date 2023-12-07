using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;

public class PlayerEquipmentManager : MonoBehaviour
{
    public static PlayerEquipmentManager instance;
    [BoxGroup("References")]
    public PlayerStatManager statManager;

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
    
    [BoxGroup("Equipment Slots/Other")]
    public InventorySlot backSlot;

    void Awake() {
        instance = this;
    }

    [Button]
    public void UpdateStats()
    {
        int o = 0;
        if(headSlot.currentItem.item)
            statManager.CombineStats(headSlot.currentItem.item.statChanges);
        else
            o++;
        if(chestSlot.currentItem.item)
            statManager.CombineStats(chestSlot.currentItem.item.statChanges);
        else
            o++;
        if(legsSlot.currentItem.item)
            statManager.CombineStats(legsSlot.currentItem.item.statChanges);
        else
            o++;
        if(feetSlot.currentItem.item)
            statManager.CombineStats(feetSlot.currentItem.item.statChanges);
        else
            o++;
        
        if(ringSlot1.currentItem.item)
            statManager.CombineStats(ringSlot1.currentItem.item.statChanges);
        else
            o++;
        if(ringSlot2.currentItem.item)
            statManager.CombineStats(ringSlot2.currentItem.item.statChanges);
        else
            o++;

        if(o >= 7){
            statManager.currentStats = statManager.baseStats;
        }
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

        UpdateStats();
    }
}
