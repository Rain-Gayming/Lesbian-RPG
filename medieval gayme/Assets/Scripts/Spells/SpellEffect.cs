using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "Magic/Effect")]
public class SpellEffect : ScriptableObject
{
    [BoxGroup("Spell")]
    public string effectName;
    [BoxGroup("Spell")]
    public ESpellType spellType;
    [BoxGroup("Spell")]
    public Sprite effectSprite;
    [BoxGroup("Spell")]
    public float manaCost;
}
