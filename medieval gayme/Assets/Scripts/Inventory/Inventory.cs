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
    
    [BoxGroup("Alter")]
    public bool inAlter;
    [BoxGroup("Alter")]
    public List<AlterSlot> alterSlots;
    [BoxGroup("Alter")]
    public InventorySlot outputSlot;
    [BoxGroup("Alter")]
    public GameObject alterSlotsObject;
    [BoxGroup("Alter")]
    public Sprite alterBackground;
    [BoxGroup("Alter")]
    public AlterInteractable lastOpenedAlter;
    [BoxGroup("Alter")]
    public bool isAlterClosed;
    [BoxGroup("Alter")]
    public SpellRuneDatabase spellRuneRecipeDatabase;

    [BoxGroup("Slots")]
    public List<InventoryItem> items;
    [BoxGroup("Slots")]
    public List<InventorySlot> inventorySlots;

    [BoxGroup("Ground Item")]
    public GameObject groundItem;
    [BoxGroup("Ground Item")]
    public Transform dropPoint;
     
    
    void Awake() {
        SetSlotPositions();
    }

    public void Start()
    {
        instance = this;
    }

    public void FixedUpdate()
    {
        if(InputManager.instance.inventory){
            InputManager.instance.inventory = false;

            inChest = false;
            inAlter = false;
            MenuManager.instance.ChangeMenuWithPause(inventoryMenu);
        }

        if(inChest){
            ChangeBackground(chestBackground);
            chestSlotsUI.SetActive(true);
            armourSlots.SetActive(false);
        }else{
            ChangeBackground(inventoryBackground);
            chestSlotsUI.SetActive(false);
            armourSlots.SetActive(true);
        }

        if(inAlter){
            ChangeBackground(alterBackground);
            alterSlotsObject.SetActive(true);
            chestSlotsUI.SetActive(false);
            armourSlots.SetActive(false);
        }else{
            if(!isAlterClosed && lastOpenedAlter != null){
                
                ChangeBackground(inventoryBackground);
                alterSlotsObject.SetActive(false);
                chestSlotsUI.SetActive(false);
                armourSlots.SetActive(true);

                for (int i = 0; i < alterSlots.Count; i++)
                {
                    for (int a = 0; a < lastOpenedAlter.alterItems.Count; a++)
                    {
                        if(alterSlots[i].slotPlace == lastOpenedAlter.alterItems[a].slotPlace){
                            lastOpenedAlter.alterItems[a].item = alterSlots[i].slot.currentItem;
                        }                
                    }
                }
                
                inAlter = false;
                isAlterClosed = true;
                lastOpenedAlter = null;
            }
        }
    }

    public void ChangeBackground(Sprite backgroundSprite)
    {
        background.sprite = backgroundSprite;
    }
    
#region Containers
    public void OpenAlter(List<AlterItem> alterItems, AlterInteractable alterInteractable)
    {
        for (int i = 0; i < alterSlots.Count; i++)
        {
            alterSlots[i].slot.alterFrom = alterInteractable;
            for (int a = 0; a < alterItems.Count; a++)
            {
                if(alterSlots[i].slotPlace == alterItems[a].slotPlace){
                    alterSlots[i].slot.currentItem = alterItems[a].item;
                }                
            }
        }

        ItemDrag.instance.SetAlterFrom(alterInteractable);
        lastOpenedAlter = alterInteractable;
        isAlterClosed = false;
    }

    public void CheckAlterRecipe()
    {
        for (int i = 0; i < spellRuneRecipeDatabase.recipiesInDatabase.Count; i++)
        {
            bool top = false;
            bool bottom = false;
            bool left = false;
            bool right = false;
            for (int a = 0; a < alterSlots.Count; a++)
            {
                if(!top){
                    if(alterSlots[a].slotPlace == AlterSlotPlace.top){
                        if(alterSlots[a].slot.currentItem.item == spellRuneRecipeDatabase.recipiesInDatabase[i].topItem){
                            top = true;
                        }
                    }
                }

                if(!bottom){
                    if(alterSlots[a].slotPlace == AlterSlotPlace.bottom){
                        if(alterSlots[a].slot.currentItem.item == spellRuneRecipeDatabase.recipiesInDatabase[i].bottomItem){
                            bottom = true;
                        }
                    }
                }

                if(!left){
                    if(alterSlots[a].slotPlace == AlterSlotPlace.left){
                        if(alterSlots[a].slot.currentItem.item == spellRuneRecipeDatabase.recipiesInDatabase[i].leftItem){
                            left = true;
                        }
                    }
                }

                if(!right){
                    if(alterSlots[a].slotPlace == AlterSlotPlace.right){
                        if(alterSlots[a].slot.currentItem.item == spellRuneRecipeDatabase.recipiesInDatabase[i].rightItem){
                            right = true;
                        }
                    }
                }
            }

            if(top && bottom && left && right){
                outputSlot.currentItem.amount = 1;
                outputSlot.currentItem.item = spellRuneRecipeDatabase.recipiesInDatabase[i].outputItem;
                
                top = false;
                bottom = false;
                left = false;
                right = false;
            }else{
                for (int a = 0; a < alterSlots.Count; a++)
                {
                    if(alterSlots[a].slotPlace == AlterSlotPlace.output){
                        alterSlots[a].slot.currentItem = null;
                        alterSlots[a].slot.UpdateItem();
                    }
                }
            }
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
#endregion
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
        for (int i = 0; i < items.Count; i++)
        {
            if(items[i].item == item.item){
                items[i].amount += item.amount;
            }
        }
        item = null;
    }

    public void AddNewItem(InventoryItem item, InventorySlot slot)
    {
        slot.currentItem = item;
        slot.UpdateItem();
        items.Add(item);
        item = null;
    }

    public void DropItem(InventoryItem item)
    {
        GameObject newGround = Instantiate(groundItem);
        newGround.transform.position = dropPoint.position;

        newGround.GetComponent<GroundItem>().item = item;
    }

    [Button]
    public void SetSlotPositions()
    {
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            inventorySlots[i].slotPos = i;
        }
    }
}
