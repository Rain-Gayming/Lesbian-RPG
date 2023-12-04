using System;
using Sirenix.OdinInspector;

[Serializable]
public class CStats
{
    [BoxGroup("Main Stats")]
    public float maxHealth;
    [BoxGroup("Main Stats")]
    public float maxMana;
    [BoxGroup("Main Stats")]
    public float maxStamina;
    [BoxGroup("Main Stats")]
    public int defence;
    
    [BoxGroup("Major Stats")]
    public int strength;
    [BoxGroup("Major Stats")]
    public int dexterity;
    [BoxGroup("Major Stats")]
    public int intelligence;
    [BoxGroup("Major Stats")]
    public int constitution;
    
    [BoxGroup("Minor Stats")]
    public MinorStats minorStats;
}

[System.Serializable]
public class MinorStats
{
    public StrengthStats strengthStats;
    public DexterityStats dexterityStats;
    public IntelligenceStats intelligenceStats;
    public ConstitutionStats constitutionStats;
}

public class StrengthStats
{
    public int longWeapons;
    public int shortWeapons;
    public int heavyArmour;
    public int mediumArmour;    
}
public class DexterityStats
{
    public int speed;
    public int athletics;
    public int agility;
    public int lightArmour;    
}
public class IntelligenceStats
{
    public int enchanting;
    public int alchemy;
    public int spellPower;    
}
public class ConstitutionStats
{
    public int illnessResistence;
    public int swimming;    
}