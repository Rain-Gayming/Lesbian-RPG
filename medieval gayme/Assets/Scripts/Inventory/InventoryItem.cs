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
