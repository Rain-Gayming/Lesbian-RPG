using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine.UI;

public class SpellToolTip : ToolTip
{
    [BoxGroup("Item")]
    public SpellSlot spellSlot;

    [BoxGroup("UI")]
    public TMP_Text spellTypeText;
    [BoxGroup("UI")]
    public TMP_Text effectsTitleText;
    [BoxGroup("UI")]
    public TMP_Text effectsText;
    [BoxGroup("UI")]
    public TMP_Text manaText;

    public override void SetText(string content, string header = "")
    {
        if(spellSlot.effect){
            header = spellSlot.effect.effectName;
            effectsText.gameObject.SetActive(false);
            effectsTitleText.gameObject.SetActive(false);
            manaText.text = "Mana: " + spellSlot.effect.manaCost;

        }else if(spellSlot.slotSpell.spellName != string.Empty){
            SetSpellText();

            effectsText.gameObject.SetActive(true);
            effectsTitleText.gameObject.SetActive(true);
            header = spellSlot.slotSpell.spellName;
            headerText.text = header;
            
            spellTypeText.text = spellSlot.slotSpell.effects[0].effectName;
        }

        base.SetText(content, header);
    }
    
    public void SetSpellText()
    {
        effectsTitleText.gameObject.SetActive(true);
        effectsText.gameObject.SetActive(true);

        // \n for new line
        string effects = "";
        for (int i = 0; i < spellSlot.slotSpell.effects.Count; i++)
        {
            effects = effects + "\n " + spellSlot.slotSpell.effects[i].effectName;
        }
        effectsText.text = effects;
        manaText.text = "Mana: " + spellSlot.slotSpell.manaCost;
        print(effects);
    }
}
