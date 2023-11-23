using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolTipSystem : MonoBehaviour
{
    public static ToolTipSystem instance;

    public ToolTip tooltip;
    public ItemToolTip itemTooltip;
    public SpellToolTip spellToolTip;

    void Awake() {
        instance = this;
    }

    public static void ShowItemTooltip(string content, string header, ItemObject item)
    {
        instance.itemTooltip.gameObject.SetActive(true);
        instance.itemTooltip.SetText(content, header);
        instance.itemTooltip.item = item;
    }
    public static void HideItemTooltip()
    {
        instance.itemTooltip.gameObject.SetActive(false);
    }

    public static void ShowSpellToolTip(string content, string header)
    {
        instance.spellToolTip.gameObject.SetActive(true);
        instance.spellToolTip.SetText(content, header);
    }
    public static void HideSpellToolTip()
    {
        instance.spellToolTip.gameObject.SetActive(false);
    }

    public static void Show(string content, string header)
    {
        instance.tooltip.gameObject.SetActive(true);
        instance.tooltip.SetText(content, header);
    }
    public static void Hide()
    {
        instance.tooltip.gameObject.SetActive(false);
    }
}
