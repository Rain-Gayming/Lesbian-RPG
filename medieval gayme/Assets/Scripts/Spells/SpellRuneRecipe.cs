using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "Magic/Runes/Recipe")]
public class SpellRuneRecipe : ScriptableObject
{
    [BoxGroup("Items")]
    public ItemObject topItem;
    [BoxGroup("Items")]
    public ItemObject bottomItem;
    [BoxGroup("Items")]
    public ItemObject leftItem;
    [BoxGroup("Items")]
    public ItemObject rightItem;
    
    [BoxGroup("Output")]
    public ItemObject outputItem;
}
