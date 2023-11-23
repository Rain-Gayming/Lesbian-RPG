using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Magic/Database")]
public class SpellDatabase : ScriptableObject
{
    public List<SpellEffect> effectsInDatabase;
}
