using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class ItemToolTip : ToolTip
{    
    [BoxGroup("Item")]
    public InventoryItem item;

    [BoxGroup("UI")]
    public TMP_Text itemTypeText;
    [BoxGroup("UI")]
    public TMP_Text statText;
    [BoxGroup("UI")]
    public TMP_Text itemStatsText;

    string eq;

    public override void SetText(string content, string header = "")
    {
        item = ItemDrag.instance.hoveredSlot.currentItem;
        header = item.item.itemName + $"({item.amount})";

        headerText.color = item.item.itemRarity.rarityColour;

        itemTypeText.text = item.item.itemType.ToString();

        base.SetText(content, header);

        if(item.item.itemType == EItemType.equipment || item.item.itemType == EItemType.weapon){
            SetStatText();
        }else{
            statText.gameObject.SetActive(false);
            itemStatsText.gameObject.SetActive(false);
        }
    }

    public void SetStatText()
    {
        statText.gameObject.SetActive(true);
        itemStatsText.gameObject.SetActive(true);

        // \n for new line
        if(item.item.itemType == EItemType.equipment){
            eq = "";
            if(item.item.statChanges.maxHealth != 0){         
                AddSeperatorToEquipment(EquipmentTipType.attribute);
                eq = eq + "\n Max Health: " + item.item.statChanges.maxHealth + " (" + (PlayerStatManager.instance.currentStats.maxHealth + item.item.statChanges.maxHealth) + ")"; 
            }
            if(item.item.statChanges.maxMana != 0){ 
                AddSeperatorToEquipment(EquipmentTipType.attribute);
                eq = eq + "\n Max Mana: " + item.item.statChanges.maxMana + " (" + (PlayerStatManager.instance.currentStats.maxMana + item.item.statChanges.maxMana) + ")"; 
            }
            if(item.item.statChanges.maxStamina != 0){ 
                AddSeperatorToEquipment(EquipmentTipType.attribute);
                eq = eq + "\n Max Stamina: " + item.item.statChanges.maxStamina + " (" + (PlayerStatManager.instance.currentStats.maxStamina + item.item.statChanges.maxStamina) + ")"; 
            }
            if(item.item.statChanges.defence != 0){
                AddSeperatorToEquipment(EquipmentTipType.attribute);
                eq = eq + "\n Defence: " + item.item.statChanges.defence + " (" + (PlayerStatManager.instance.currentStats.defence + item.item.statChanges.defence) + ")"; 
            }

            if(item.item.statChanges.strength != 0){
                AddSeperatorToEquipment(EquipmentTipType.mainStat);
                eq = eq + "\n Strength: " + item.item.statChanges.strength + " (" + (PlayerStatManager.instance.currentStats.strength + item.item.statChanges.strength) + ")"; 
            }            
            if(item.item.statChanges.dexterity != 0){
                AddSeperatorToEquipment(EquipmentTipType.mainStat);
                eq = eq + "\n Dexterity: " + item.item.statChanges.dexterity + " (" + (PlayerStatManager.instance.currentStats.dexterity + item.item.statChanges.dexterity) + ")"; 
            }
            if(item.item.statChanges.intelligence != 0){         
                AddSeperatorToEquipment(EquipmentTipType.mainStat);
                eq = eq + "\n Intelligence: " + item.item.statChanges.intelligence + " (" + (PlayerStatManager.instance.currentStats.intelligence + item.item.statChanges.intelligence) + ")"; 
            }
            if(item.item.statChanges.constitution != 0){   
                AddSeperatorToEquipment(EquipmentTipType.mainStat);
                eq = eq + "\n Constitution: " + item.item.statChanges.constitution + " (" + (PlayerStatManager.instance.currentStats.constitution + item.item.statChanges.constitution) + ")"; 
            }
            
            if(item.item.statChanges.minorStats.strengthStats.longWeapons != 0){
                AddSeperatorToEquipment(EquipmentTipType.strStat);
                eq = eq + "\n Long Weapons: " + item.item.statChanges.minorStats.strengthStats.longWeapons + " (" + (PlayerStatManager.instance.currentStats.minorStats.strengthStats.longWeapons + item.item.statChanges.minorStats.strengthStats.longWeapons) + ")"; 
            }            
            if(item.item.statChanges.minorStats.strengthStats.shortWeapons != 0){
                AddSeperatorToEquipment(EquipmentTipType.strStat);
                eq = eq + "\n Short Weapons: " + item.item.statChanges.minorStats.strengthStats.shortWeapons + " (" + (PlayerStatManager.instance.currentStats.minorStats.strengthStats.shortWeapons + item.item.statChanges.minorStats.strengthStats.shortWeapons) + ")"; 
            }            
            if(item.item.statChanges.minorStats.strengthStats.heavyArmour != 0){
                AddSeperatorToEquipment(EquipmentTipType.strStat);
                eq = eq + "\n Heavy Armour: " + item.item.statChanges.minorStats.strengthStats.heavyArmour + " (" + (PlayerStatManager.instance.currentStats.minorStats.strengthStats.heavyArmour + item.item.statChanges.minorStats.strengthStats.heavyArmour) + ")"; 
            }            
            if(item.item.statChanges.minorStats.strengthStats.mediumArmour != 0){
                AddSeperatorToEquipment(EquipmentTipType.strStat);
                eq = eq + "\n Medium Armour: " + item.item.statChanges.minorStats.strengthStats.mediumArmour + " (" + (PlayerStatManager.instance.currentStats.minorStats.strengthStats.mediumArmour + item.item.statChanges.minorStats.strengthStats.mediumArmour) + ")"; 
            }
            
            
            if(item.item.statChanges.minorStats.dexterityStats.speed != 0){
                AddSeperatorToEquipment(EquipmentTipType.dexStat);
                eq = eq + "\n Speed: " + item.item.statChanges.minorStats.dexterityStats.speed + " (" + (PlayerStatManager.instance.currentStats.minorStats.dexterityStats.speed + item.item.statChanges.minorStats.dexterityStats.speed) + ")"; 
            }
            if(item.item.statChanges.minorStats.dexterityStats.athletics != 0){
                AddSeperatorToEquipment(EquipmentTipType.dexStat);
                eq = eq + "\n Athletics: " + item.item.statChanges.minorStats.dexterityStats.athletics + " (" + (PlayerStatManager.instance.currentStats.minorStats.dexterityStats.athletics + item.item.statChanges.minorStats.dexterityStats.athletics) + ")"; 
            }
            if(item.item.statChanges.minorStats.dexterityStats.agility != 0){
                AddSeperatorToEquipment(EquipmentTipType.dexStat);
                eq = eq + "\n Agility: " + item.item.statChanges.minorStats.dexterityStats.agility + " (" + (PlayerStatManager.instance.currentStats.minorStats.dexterityStats.agility + item.item.statChanges.minorStats.dexterityStats.agility) + ")"; 
            }
            if(item.item.statChanges.minorStats.dexterityStats.lightArmour != 0){
                AddSeperatorToEquipment(EquipmentTipType.dexStat);
                eq = eq + "\n Light Armour: " + item.item.statChanges.minorStats.dexterityStats.lightArmour + " (" + (PlayerStatManager.instance.currentStats.minorStats.dexterityStats.lightArmour + item.item.statChanges.minorStats.dexterityStats.lightArmour) + ")"; 
            }
            
            if(item.item.statChanges.minorStats.intelligenceStats.enchanting != 0){
                AddSeperatorToEquipment(EquipmentTipType.intStat);
                eq = eq + "\n Enchanting: " + item.item.statChanges.minorStats.intelligenceStats.enchanting + " (" + (PlayerStatManager.instance.currentStats.minorStats.intelligenceStats.enchanting + item.item.statChanges.minorStats.intelligenceStats.enchanting) + ")"; 
            }
            if(item.item.statChanges.minorStats.intelligenceStats.alchemy != 0){
                AddSeperatorToEquipment(EquipmentTipType.intStat);
                eq = eq + "\n Alchemy: " + item.item.statChanges.minorStats.intelligenceStats.alchemy + " (" + (PlayerStatManager.instance.currentStats.minorStats.intelligenceStats.alchemy + item.item.statChanges.minorStats.intelligenceStats.alchemy) + ")"; 
            }
            if(item.item.statChanges.minorStats.intelligenceStats.spellPower != 0){
                AddSeperatorToEquipment(EquipmentTipType.intStat);
                eq = eq + "\n Spell Power: " + item.item.statChanges.minorStats.intelligenceStats.spellPower + " (" + (PlayerStatManager.instance.currentStats.minorStats.intelligenceStats.spellPower + item.item.statChanges.minorStats.intelligenceStats.spellPower) + ")"; 
            }

            if(item.item.statChanges.minorStats.constitutionStats.illnessResistence != 0){
                AddSeperatorToEquipment(EquipmentTipType.conStat);
                eq = eq + "\n Illness Resistence: " + item.item.statChanges.minorStats.constitutionStats.illnessResistence + " (" + (PlayerStatManager.instance.currentStats.minorStats.constitutionStats.illnessResistence + item.item.statChanges.minorStats.constitutionStats.illnessResistence) + ")"; 
            }
            if(item.item.statChanges.minorStats.constitutionStats.swimming != 0){
                AddSeperatorToEquipment(EquipmentTipType.conStat);
                eq = eq + "\n Swimming: " + item.item.statChanges.minorStats.constitutionStats.swimming + " (" + (PlayerStatManager.instance.currentStats.minorStats.constitutionStats.swimming + item.item.statChanges.minorStats.constitutionStats.swimming) + ")"; 
            }



            itemStatsText.text = eq;

            itemTypeText.text = "Equipment: " + item.item.equipmentType.ToString();
        }
        if(item.item.itemType == EItemType.weapon){
            itemStatsText.text = "+ " + item.item.damage.ToString() + " damage";
            itemTypeText.text = "Weapon: " + item.item.weaponType.ToString();
        }
    }

    public void AddSeperatorToEquipment(EquipmentTipType tipType)
    {
        switch (tipType)
        {
            case EquipmentTipType.attribute:
                if(!eq.Contains("\n \n Attributes:") && eq != ""){
                    eq = eq + "\n \n Attributes:";
                }else if(!eq.Contains("\n \n Attributes:") && eq.StartsWith("\n")){
                    eq = eq + "Attributes:";
                }else if(eq == ""){
                    eq = eq + "\n Attributes:";
                }
            break;
            case EquipmentTipType.mainStat:
                if(!eq.Contains("\n \n Main Stats:") && eq != ""){
                    eq = eq + "\n \n Main Stats:";
                }else if(!eq.Contains("\n \n Main Stats:") && eq.StartsWith("\n")){
                    eq = eq + "Main Stats:";
                }else if(eq == ""){
                    eq = eq + "\n Main Stats:";
                }
            break;
            case EquipmentTipType.strStat:
                if(!eq.Contains("\n \n Strength Minor Stats:") && eq != ""){
                    eq = eq + "\n \n Strength Minor Stats:";
                }else if(!eq.Contains("\n \n Strength Minor Stats:") && eq.StartsWith("\n")){
                    eq = eq + "Strength Minor Stats:";
                }else if(eq == ""){
                    eq = eq + "\n Strength Minor Stats:";
                }

            break;
            case EquipmentTipType.dexStat:
                if(!eq.Contains("\n \n Dexterity Minor Stats:") && eq != ""){
                    eq = eq + "\n \n Dexterity Minor Stats:";
                }else if(!eq.Contains("\n \n Dexterity Minor Stats:") && eq.StartsWith("\n")){
                    eq = eq + "Dexterity Minor Stats:";
                }else if(eq == ""){
                    eq = eq + "\n Dexterity Minor Stats:";
                }

            break;
            case EquipmentTipType.intStat:
                if(!eq.Contains("\n \n Intelligence Minor Stats:") && eq != ""){
                    eq = eq + "\n \n Intelligence Minor Stats:";
                }else if(!eq.Contains("\n \n Intelligence Minor Stats:") && eq.StartsWith("\n")){
                    eq = eq + "Intelligence Minor Stats:";
                }else if(eq == ""){
                    eq = eq + "\n Intelligence Minor Stats:";
                }

            break;
            case EquipmentTipType.conStat:
                if(!eq.Contains("\n \n Constitution Minor Stats:") && eq != ""){
                    eq = eq + "\n \n Constitution Minor Stats:";
                }else if(!eq.Contains("\n \n Constitution Minor Stats:") && eq.StartsWith("\n")){
                    eq = eq + "Constitution Minor Stats:";
                }else if(eq == ""){
                    eq = eq + "\n Constitution Minor Stats:";
                }

            break;
        }
    }
}



public enum EquipmentTipType
{
    attribute,
    mainStat,
    strStat,
    dexStat,
    intStat,
    conStat
}