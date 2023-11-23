using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Sirenix.OdinInspector;

public class GroundItem : Interactable
{
    [BoxGroup("UI")]
    public TMP_Text itemNameDisplay;
    
    [BoxGroup("Item")]
    public InventoryItem item;

    public void Start()
    {
        itemNameDisplay.text = "[" + item.item.itemName + " x " + item.amount.ToString() + "]";

        itemNameDisplay.color = item.item.itemRarity.rarityColour;
    }

    public override void Interact()
    {
        base.Interact();
        
        if(Inventory.instance == null){
            print("No Inventory");
        }
        Inventory.instance.AddItem(item);
        Destroy(gameObject);
    }
}
