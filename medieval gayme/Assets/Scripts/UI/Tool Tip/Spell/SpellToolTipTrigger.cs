using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Unity.VisualScripting;

public class SpellToolTipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [BoxGroup("Item")]
    public SpellSlot spellSlot;


    [BoxGroup("Tooltip")]
    public string content;
    [BoxGroup("Tooltip")]
    public string header;

    public void OnPointerEnter(PointerEventData eventData)
    {
        ToolTipSystem.instance.spellToolTip.spellSlot = GetComponent<SpellSlot>();
        ToolTipSystem.ShowSpellToolTip(content, header);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ToolTipSystem.HideSpellToolTip();
    }
}
