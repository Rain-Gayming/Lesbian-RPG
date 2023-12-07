using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStatManager : MonoBehaviour
{
    public static PlayerStatManager instance;
    [BoxGroup("References")]
    public PlayerHealthManager playerHealthManager;
    [BoxGroup("References")]
    public PlayerSpellManager playerSpellManager;

    [BoxGroup("Stats")]
    public CStats baseStats;
    [BoxGroup("Stats")]
    public CStats currentStats;

    void Awake() {
        instance = this;
    }
    
    public void CombineStats(CStats sta)
    {
        currentStats.maxHealth += sta.maxHealth;
        currentStats.maxMana += sta.maxMana;
        currentStats.maxStamina += sta.maxStamina;
        currentStats.defence += sta.defence;
        
        currentStats.strength += sta.strength;
        currentStats.dexterity += sta.dexterity;
        currentStats.intelligence += sta.intelligence;
        currentStats.constitution += sta.constitution;
        
        currentStats.minorStats.strengthStats.longWeapons += sta.minorStats.strengthStats.longWeapons;
        currentStats.minorStats.strengthStats.shortWeapons += sta.minorStats.strengthStats.shortWeapons;
        currentStats.minorStats.strengthStats.heavyArmour += sta.minorStats.strengthStats.heavyArmour;
        currentStats.minorStats.strengthStats.mediumArmour += sta.minorStats.strengthStats.mediumArmour;
        
        currentStats.minorStats.dexterityStats.speed += sta.minorStats.dexterityStats.speed;
        currentStats.minorStats.dexterityStats.athletics += sta.minorStats.dexterityStats.athletics;
        currentStats.minorStats.dexterityStats.agility += sta.minorStats.dexterityStats.agility;
        currentStats.minorStats.dexterityStats.lightArmour += sta.minorStats.dexterityStats.lightArmour;
        
        currentStats.minorStats.intelligenceStats.enchanting += sta.minorStats.intelligenceStats.enchanting;
        currentStats.minorStats.intelligenceStats.alchemy += sta.minorStats.intelligenceStats.alchemy;
        currentStats.minorStats.intelligenceStats.spellPower += sta.minorStats.intelligenceStats.spellPower;
        
        currentStats.minorStats.constitutionStats.illnessResistence += sta.minorStats.constitutionStats.illnessResistence;
        currentStats.minorStats.constitutionStats.swimming += sta.minorStats.constitutionStats.swimming;


        playerHealthManager.maxHealth = currentStats.maxHealth;
        playerSpellManager.maxMana = currentStats.maxMana;
    }
}
