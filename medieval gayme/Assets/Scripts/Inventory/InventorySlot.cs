using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Sirenix.OdinInspector;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor.Rendering;

public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [BoxGroup("References")]
    public TMP_Text amountText;
    [BoxGroup("References")]
    public Image itemRarity;
    [BoxGroup("References")]
    public Image itemIcon;

    [BoxGroup("Chest")]
    public bool chestSlot;
    [BoxGroup("Chest")]
    public ChestInteractable chestFrom;

    [BoxGroup("Hovering")]
    public bool hovered;
    [BoxGroup("Hovering")]
    public GameObject hoveredObject;
    
    [BoxGroup("Items")]
    public bool hasRestriction;
    [BoxGroup("Items")][ShowIf("hasRestriction")]
    public EItemType itemRestriction;
    [BoxGroup("Items")][ShowIf("hasRestriction")][ShowIf("itemRestriction", EItemType.equipment)]
    public EEquipmentType equipmentRestriction;
    [BoxGroup("Items")]
    public InventoryItem currentItem;
    [BoxGroup("Items")]
    public InventoryItem preItem;

    public void Start()
    {
        UpdateItem();
    }
    private void Update() {
        if(hovered){        
            if(Input.GetMouseButtonDown(0)){
                if(chestFrom){
                    ItemDrag.instance.SetChestFrom(chestFrom);
                }
                ItemDrag.instance.MouseClicked(chestSlot); 
            }
            if(InputManager.instance.dropItem && currentItem.item != null){
                InputManager.instance.dropItem = false;
                
                Inventory.instance.DropItem(currentItem);
                currentItem = new InventoryItem(null, 0);
                UpdateItem();
            }

            if(Input.GetMouseButtonDown(1) && currentItem.item != null){
                UseItem();
            }
        }
    }

    public void UseItem()
    {
        switch (currentItem.item.itemType)
        {
            case EItemType.spellEffect:
                SpellBook.instance.UnlockSpellEffect(currentItem.item.spellEffectToUnlock);
                currentItem = new InventoryItem(null, 0);
                UpdateItem();
            break;
            case EItemType.equipment:
                PlayerEquipmentManager.instance.QuickEquip(this, currentItem);
                UpdateItem();
            break;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hovered = true;
        hoveredObject.SetActive(true);
        ItemSlotTooltipTrigger.instance.item = currentItem.item;
        ItemDrag.instance.hoveredSlot = this;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hovered = false;
        hoveredObject.SetActive(false);
    }

    [Button]
    public void UpdateItem()
    {
        QuestManager.instance.CheckQuestsForItems(currentItem.item, currentItem.amount);
        if(currentItem.amount <= 1){
            amountText.gameObject.SetActive(false);
        }else{
            amountText.gameObject.SetActive(true);
            amountText.text = currentItem.amount.ToString();
        }

        if(currentItem.item != null){
            itemRarity.gameObject.SetActive(true);
            itemIcon.gameObject.SetActive(true);

            itemRarity.color = currentItem.item.itemRarity.rarityColour;
            itemIcon.sprite = currentItem.item.itemIcon;

            if(currentItem.amount == 0 || !currentItem.item.canStack && currentItem.amount > 1){
                currentItem.amount = 1;
            }
        }else{
            itemRarity.gameObject.SetActive(false);
            itemIcon.gameObject.SetActive(false);
        }
        
        //Setting directly breaks it
        InventoryItem ii = new InventoryItem(currentItem.item, currentItem.amount);
        preItem = ii;
    }
}
