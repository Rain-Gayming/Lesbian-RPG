using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Unity.VisualScripting;

public class ItemSlotTooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public static ItemSlotTooltipTrigger instance;

    [BoxGroup("Item")]
    public InventoryItem item;

    [BoxGroup("Tooltip")]
    public string content;
    [BoxGroup("Tooltip")]
    public string header;

    public void Awake() 
    {
        instance = this;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(ItemDrag.instance.hoveredSlot.currentItem.item != null){
            item = ItemDrag.instance.hoveredSlot.currentItem;
            ToolTipSystem.ShowItemTooltip(content, header, item);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ToolTipSystem.HideItemTooltip();
    }
}
