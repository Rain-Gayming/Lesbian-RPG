using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ChestSave
{
    public int chestID;
    public string chestName;
    public List<SaveInventoryItem> chestItems;
    public bool chestHasBeenOpened;

    public ChestSave(int _id, string _name, List<SaveInventoryItem> _items, bool _opened)
    {
        chestID = _id;
        chestName = _name;
        chestItems = _items;
        chestHasBeenOpened = _opened;
    }
}
