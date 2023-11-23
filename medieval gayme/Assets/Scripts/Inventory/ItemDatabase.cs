using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Database")]
public class ItemDatabase : ScriptableObject
{
    public List<ItemObject> items;
}
