using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    [BoxGroup("References")]
    public Menu inventoryMenu;

    [BoxGroup("UI")]
    public Image background;
    [BoxGroup("UI")]
    public Sprite inventoryBackground;
    [BoxGroup("UI")]
    public Sprite chestBackground;

    [BoxGroup("Chest")]
    public bool inChest;
    [BoxGroup("Chest")]
    public GameObject chestSlotsUI;
    [BoxGroup("Slots")]
    public List<InventorySlot> chestSlots;
    [BoxGroup("Chest")]
    public GameObject armourSlots;

    [BoxGroup("Slots")]
    public List<InventorySlot> inventorySlots;

    [BoxGroup("Ground Item")]
    public GameObject groundItem;
    [BoxGroup("Ground Item")]
    public Transform dropPoint;
     
    
    public void Start()
    {
        instance = this;
    }

    public void FixedUpdate()
    {
        if(InputManager.instance.inventory){
            InputManager.instance.inventory = false;

            inChest = false;
            MenuManager.instance.ChangeMenuWithPause(inventoryMenu);
        }

        if(inChest){
            background.sprite = chestBackground;
            chestSlotsUI.SetActive(true);
            armourSlots.SetActive(false);
        }else{
            background.sprite = inventoryBackground;
            chestSlotsUI.SetActive(false);
            armourSlots.SetActive(true);
        }
    }

    public void OpenChest(List<InventoryItem> chestItems, ChestInteractable chest)
    {
        for (int i = 0; i < chestSlots.Count; i++)
        {
            chestSlots[i].currentItem = new InventoryItem(null, 0);
            chestSlots[i].chestFrom = chest;
        }
        for (int i = 0; i < chestItems.Count; i++)
        {
            chestSlots[i].currentItem = chestItems[i];
            chestSlots[i].UpdateItem();
        }
    }

    [Button]
    public void TestAdd(ItemObject item, int amount)
    {
        AddItem(new InventoryItem(item, amount));   
    }

    public bool CanAddItem(InventoryItem item)
    {
        int _i = 0;
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            _i = i;
            if(inventorySlots[i].currentItem.item != null || inventorySlots[i].currentItem.item == item.item){
                return true;
            }
        }
        if(_i > inventorySlots.Count)
            return false;
        else
            return true;
    }
    
    public void AddItem(InventoryItem itemToAdd)
    {
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            if(inventorySlots[i].currentItem.item == null){
                AddNewItem(itemToAdd, inventorySlots[i]);
                return;
            }else{
                if(inventorySlots[i].currentItem.item == itemToAdd.item){
                    if(itemToAdd.item.canStack){
                        AddToStack(itemToAdd, inventorySlots[i]);
                        return;
                    }else{
                        continue;
                    }
                }
            }
        }
    }

    public void AddToStack(InventoryItem item, InventorySlot slot)
    {
        slot.currentItem.amount += item.amount;
        slot.UpdateItem();
        item = null;
    }

    public void AddNewItem(InventoryItem item, InventorySlot slot)
    {
        slot.currentItem = item;
        slot.UpdateItem();
        item = null;
    }

    public void DropItem(InventoryItem item)
    {
        GameObject newGround = Instantiate(groundItem);
        newGround.transform.position = dropPoint.position;

        newGround.GetComponent<GroundItem>().item = item;
    }
}
