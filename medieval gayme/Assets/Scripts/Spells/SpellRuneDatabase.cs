using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Magic/Runes/Database")]
public class SpellRuneDatabase : ScriptableObject
{
    public List<SpellRuneRecipe> recipiesInDatabase;
}
