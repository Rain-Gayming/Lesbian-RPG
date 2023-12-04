using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryItem
{
    public ItemObject item;
    public int amount;

    public InventoryItem(ItemObject _item, int _amount)
    {
        item = _item;
        amount = _amount;
    }
}

[System.Serializable]
public class SaveInventoryItem
{
    public string item;
    public int amount;
    public int slotPosition;

    public SaveInventoryItem(string _item, int _amount, int _slotPosition)
    {
        item = _item;
        amount = _amount;
        slotPosition = _slotPosition;
    }
}