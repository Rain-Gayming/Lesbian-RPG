using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Spell
{
    public List<SpellEffect> effects;
    public string spellName;
    public SpellEffect iconEffect;
    public float manaCost;

    public Spell(List<SpellEffect> _effects, string _spellName, SpellEffect _icon, float _manaCost)
    {
        effects = _effects;
        spellName = _spellName;
        iconEffect = _icon;
        manaCost = _manaCost;
    }
}
