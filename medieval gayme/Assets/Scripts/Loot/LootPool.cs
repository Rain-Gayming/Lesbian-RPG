using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "Loot/Loot Pool")]
public class LootPool : ScriptableObject
{
    [BoxGroup("Loot")]
    public List<LootPoolItem> itemsInLootPool;
    [BoxGroup("Loot")]
    public int minDrops, maxDrops;

    [Button]
    public List<InventoryItem> GenerateLoot()
    {
        List<InventoryItem> returnLoot = new List<InventoryItem>();

        int drops = Random.Range(minDrops, maxDrops);
        int dropped = 0;
        if(dropped < drops){
            for (int c = 0; c < itemsInLootPool.Count; c++)
            {
                float val = Random.Range(0, 100);

                if(itemsInLootPool[c].dropChange >= val){
                    dropped++;
                    returnLoot.Add(new InventoryItem(itemsInLootPool[c].item, Random.Range(itemsInLootPool[c].minAmount, itemsInLootPool[c].maxAmount)));
                }            
            }  
        } 

        return returnLoot;
    }
}
