using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;

[CreateAssetMenu(menuName = "Items/ItemObject")]
public class ItemObject : ScriptableObject
{
    [BoxGroup("Basic Info")]
    public string itemName;
    [BoxGroup("Basic Info")]
    public Rarity itemRarity;
    [BoxGroup("Basic Info")]
    public Sprite itemIcon;
    [BoxGroup("Basic Info")]
    public bool canStack;
    [BoxGroup("Basic Info")]
    public EItemType itemType; 

#region Equipment Info
    [BoxGroup("Equipment Info")][ShowIf("itemType", EItemType.equipment)]
    public EEquipmentType equipmentType;
    [BoxGroup("Equipment Info")][ShowIf("itemType", EItemType.equipment)]
    public int armour;
    [BoxGroup("Equipment Info")][ShowIf("itemType", EItemType.equipment)][ShowIf("equipmentType", EEquipmentType.bags)]
    public int slots;
#endregion

#region Spell Info
    [BoxGroup("Spell Info")][ShowIf("itemType", EItemType.spellEffect)]
    public SpellEffect spellEffectToUnlock;
#endregion

#region Weapon Info
    [BoxGroup("Weapon Info")][ShowIf("itemType", EItemType.weapon)]
    public EWeaponType weaponType;
    [BoxGroup("Weapon Info")][ShowIf("itemType", EItemType.weapon)]
    public float damage;
    [BoxGroup("Weapon Info")][ShowIf("itemType", EItemType.weapon)]
    public float baseRange;
#endregion

    [Button]
    public void SetStuff()
    {
        string n = name;
        string parsed = n.Split('_')[1];

        itemName = parsed;
    }
}
