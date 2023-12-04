using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine.UI;

public class ItemToolTip : ToolTip
{    
    [BoxGroup("Item")]
    public ItemObject item;

    [BoxGroup("UI")]
    public TMP_Text itemTypeText;
    [BoxGroup("UI")]
    public TMP_Text statText;
    [BoxGroup("UI")]
    public TMP_Text itemStatsText;

    public override void SetText(string content, string header = "")
    {
        item = ItemDrag.instance.hoveredSlot.currentItem.item;
        header = item.itemName;

        itemTypeText.text = item.itemType.ToString();

        base.SetText(content, header);

        if(item.itemType == EItemType.equipment || item.itemType == EItemType.weapon){
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
        if(item.itemType == EItemType.equipment){
            itemStatsText.text = "+ " + item.statChanges.defence    .ToString() + " armour";
            itemTypeText.text = "Equipment: " + item.equipmentType.ToString();
        }
        if(item.itemType == EItemType.weapon){
            itemStatsText.text = "+ " + item.damage.ToString() + " damage";
            itemTypeText.text = "Weapon: " + item.weaponType.ToString();
        }
    }
}
