using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class LootPoolItem
{
    public ItemObject item;
    public int maxAmount;
    public int minAmount;
    [Range(0.001f, 100)] public float dropChange = 0.001f;
}
