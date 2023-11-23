using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveSpell
{
    public string spellName;
    public string iconEffect;
    public List<string> effects;
    public float manaCost;

    public SaveSpell(List<string> _effects, string _spellName, string _icon, float _manaCost)
    {
        effects = _effects;
        spellName = _spellName;
        iconEffect = _icon;
        manaCost = _manaCost;
    }
}

[System.Serializable]
public class SaveEffect
{
    public string effectName;

    public SaveEffect(string _effectName)
    {
        effectName = _effectName;
    }
}
