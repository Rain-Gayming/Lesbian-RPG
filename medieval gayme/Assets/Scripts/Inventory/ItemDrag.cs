using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
public class ItemDrag : MonoBehaviour
{
    public static ItemDrag instance;

    [BoxGroup("References")]
    public Image itemIcon;
    [BoxGroup("References")]
    public ChestInteractable chestFrom;
    
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

    public void MouseClicked(bool chestSlot)
    {
        if(!fromSlot){
            fromSlot = hoveredSlot;
        }else{
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
                    SwapSlots(chestSlot);
                }else{
                    if(hoveredSlot.itemRestriction != EItemType.equipment){
                        if(fromSlot.currentItem.item.itemType == hoveredSlot.itemRestriction){
                            SwapSlots(chestSlot);
                        }
                    }else{
                        if(fromSlot.currentItem.item.itemType == hoveredSlot.itemRestriction){                            
                            if(fromSlot.currentItem.item.equipmentType == hoveredSlot.equipmentRestriction){
                                SwapSlots(chestSlot);
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

    public void SwapSlots(bool chestSlot)
    {
        storedFromItem = fromSlot.currentItem;

        fromSlot.currentItem = hoveredSlot.currentItem;
        hoveredSlot.currentItem = storedFromItem;

        fromSlot.UpdateItem();
        hoveredSlot.UpdateItem();
        
        if(chestFrom){
            for (int i = 0; i < chestFrom.chestItems.Count; i++)
            {
                if(chestFrom.chestItems[i].item == storedFromItem.item){
                    chestFrom.chestItems.RemoveAt(i);
                }
            }
            if(hoveredSlot.chestSlot){
                chestFrom.chestItems.Add(storedFromItem);
            }
        }
        chestFrom = null;
        fromSlot = null;
        storedFromItem = null;
    }
}
