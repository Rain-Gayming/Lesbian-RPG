using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using JetBrains.Annotations;
public class ItemDrag : MonoBehaviour
{
    public static ItemDrag instance;

    [BoxGroup("References")]
    public Image itemIcon;
    [BoxGroup("References")]
    public ChestInteractable chestFrom;
    [BoxGroup("References")]
    public AlterInteractable alterFrom;
    
    [BoxGroup("Slots")]
    public InventorySlot fromSlot;
    [BoxGroup("Slots")]
    public InventorySlot hoveredSlot;
    [BoxGroup("Slots")]
    public InventoryItem storedFromItem;

    public void Awake()
    {
        instance = this;
    }

    public void Update()
    {
        if(fromSlot){
            transform.position = Input.mousePosition;
            if(fromSlot.currentItem.item != null){
                itemIcon.gameObject.SetActive(true);
                itemIcon.sprite = fromSlot.currentItem.item.itemIcon;
            }else{
                fromSlot = null;
            }
        }else{
            itemIcon.gameObject.SetActive(false);
        }
    }

    public void SetChestFrom(ChestInteractable _chestFrom)
    {
        chestFrom = _chestFrom;
    }
    public void SetAlterFrom(AlterInteractable _alterFrom)
    {
        alterFrom = _alterFrom;
    }

    public void MouseClicked(bool chestSlot)
    {
        if(fromSlot == null){
            fromSlot = hoveredSlot;
        }else{
            DoStuff();
        }
    }

    public void DoStuff()
    {
        if(fromSlot.outputSlot){
            
            for (int i = 0; i < Inventory.instance.alterSlots.Count; i++)
            {
                if(!Inventory.instance.alterSlots[i].slot.outputSlot){ 
                    Inventory.instance.alterSlots[i].slot.currentItem.amount -= 1;
                    print("removing item from " + Inventory.instance.alterSlots[i].slot);
                    Inventory.instance.alterSlots[i].slot.UpdateItem();
                }                    
            }
        }

        if(!hoveredSlot.cantTakeItem){
            if(hoveredSlot != fromSlot && fromSlot.currentItem.item){
                
                if(fromSlot.currentItem.item.canStack){
                    if(hoveredSlot.currentItem.item == fromSlot.currentItem.item){
                        hoveredSlot.currentItem.amount += fromSlot.currentItem.amount;
                        fromSlot.currentItem = new InventoryItem(null, 0);
                        
                        fromSlot.UpdateItem();
                        hoveredSlot.UpdateItem();

                        fromSlot = null;
                        hoveredSlot = null;
                        storedFromItem = null;

                        return;
                    }
                }

                if(!hoveredSlot.hasRestriction){
                    SwapSlots();
                }else{
                    if(hoveredSlot.itemRestriction != EItemType.equipment){
                        if(fromSlot.currentItem.item.itemType == hoveredSlot.itemRestriction){
                            SwapSlots();
                        }
                    }else{
                        if(fromSlot.currentItem.item.itemType == hoveredSlot.itemRestriction){                            
                            if(fromSlot.currentItem.item.equipmentType == hoveredSlot.equipmentRestriction){
                                SwapSlots();
                            }
                        }
                    }
                }
            }else{
                fromSlot = null;
                storedFromItem = new InventoryItem(null, 0);
            }
        }
    }

    public void SwapSlots()
    {
        InventoryItem iii = fromSlot.currentItem;
        storedFromItem = fromSlot.currentItem;

        fromSlot.UpdateItem();
        hoveredSlot.UpdateItem();
        
        if(chestFrom){
            for (int i = 0; i < chestFrom.chestItems.Count; i++)
            {
                if(chestFrom.chestItems[i].item == storedFromItem.item){
                    chestFrom.chestItems.RemoveAt(i);
                    Inventory.instance.items.Add(storedFromItem);
                }
            }
            if(hoveredSlot.chestSlot){
                chestFrom.chestItems.Add(storedFromItem);
            }
        }

        if(fromSlot.alterSlot){

            for (int i = 0; i < alterFrom.alterItems.Count; i++)
            {
                if(alterFrom.alterItems[i].slotPlace == fromSlot.alterPlace){
                    InventoryItem ii = new InventoryItem(hoveredSlot.currentItem.item, hoveredSlot.currentItem.amount);
                    alterFrom.alterItems[i].item = ii;
                    
                    if(!hoveredSlot.alterFrom)
                        Inventory.instance.items.Add(storedFromItem);
                }
            }
        }

        if(hoveredSlot.alterSlot)
        {
            print(hoveredSlot.alterPlace);
            for (int i = 0; i < alterFrom.alterItems.Count; i++)
            {
                if(alterFrom.alterItems[i].slotPlace == hoveredSlot.alterPlace){
                    
                    InventoryItem ii = new InventoryItem(iii.item, iii.amount);
                    alterFrom.alterItems[i].item = ii;
                }
            } 
        }

        fromSlot.currentItem = hoveredSlot.currentItem;
        hoveredSlot.currentItem = storedFromItem;

        PlayerEquipmentManager.instance.UpdateStats();

        chestFrom = null;
        fromSlot = null;
        hoveredSlot = null;
        storedFromItem = null;
    }
}
