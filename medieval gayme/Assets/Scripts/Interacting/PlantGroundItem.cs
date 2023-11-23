using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Sirenix.OdinInspector;


public class PlantGroundItem : Interactable
{
    [BoxGroup("UI")]
    public TMP_Text itemNameDisplay;
    
    [BoxGroup("Item")]
    public InventoryItem item;

    
    [BoxGroup("Plant")]
    public float minRegenTime;
    [BoxGroup("Plant")]
    public float maxRegenTime;
    [BoxGroup("Plant")]
    public float currentRegenTime;
    [BoxGroup("Plant")]
    public int minAmount;
    [BoxGroup("Plant")]
    public int maxAmount;
    [BoxGroup("Plant")]
    public GameObject unpickedObject;
    [BoxGroup("Plant")]
    public GameObject pickedObject;

    public void Start()
    {
        item.amount = Random.Range(minAmount, maxAmount);
        itemNameDisplay.text = "[" + item.item.itemName + " x " + item.amount.ToString() + "]";

        itemNameDisplay.color = item.item.itemRarity.rarityColour;
    }

    public void Update() {
        if(currentRegenTime <= 0){

            if(item.amount < minAmount){
                item.amount = Random.Range(minAmount, maxAmount);
                itemNameDisplay.text = "[" + item.item.itemName + " x " + item.amount.ToString() + "]";
            }

            currentRegenTime = 0;
            showInteractableUI = true;
            unpickedObject.SetActive(true);
            pickedObject.SetActive(false);
        }else{
            currentRegenTime -= Time.deltaTime;
            showInteractableUI = false;
            unpickedObject.SetActive(false);
            pickedObject.SetActive(true);
        }
        
    }

    public override void Interact()
    {
        base.Interact();
        
        if(currentRegenTime >= 0){
            if(Inventory.instance == null){
                print("No Inventory");
            }
            Inventory.instance.AddItem(item);
            currentRegenTime = Random.Range(minRegenTime, maxRegenTime);
        }
    }
}
