using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Sirenix.OdinInspector;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SpellSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [BoxGroup("References")]
    public Image itemIcon;

    [BoxGroup("Spell")]
    public bool isSpell;
    [BoxGroup("Spell")]
    public Spell slotSpell;
    [BoxGroup("Spell")]
    public SpellEffect effect;
    [BoxGroup("Spell")]
    public bool spellCraftSlot;

    [BoxGroup("Hovering")]
    public bool hovered;
    [BoxGroup("Hovering")]
    public GameObject hoveredObject;
    
    void Start() {
        if(effect){
            itemIcon.gameObject.SetActive(true);
            if(slotSpell.iconEffect){
                itemIcon.sprite = slotSpell.iconEffect.effectSprite;
            }else{
                itemIcon.sprite = effect.effectSprite;
            }
        }
    }

    void Update() {
        if(hovered && Input.GetMouseButtonDown(0)){
            if(!isSpell){
                if(!SpellBook.instance.inCreateMenu){                    
                    if(spellCraftSlot){
                        SpellBook.instance.RemoveEffect(effect, this);
                    }else{
                        if(effect != null)
                            SpellBook.instance.AddEffect(effect);
                    }
                }else{
                    SpellBook.instance.iconSlot.effect = effect;
                }
            }else{
                SpellBook.instance.currentSpell = slotSpell;
            }
        }
    }

    void FixedUpdate() 
    {       
        if(spellCraftSlot){
            if(effect){
                itemIcon.gameObject.SetActive(true);
                itemIcon.sprite = effect.effectSprite;
            }else{
                itemIcon.gameObject.SetActive(false);
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hovered = true;
        hoveredObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hovered = false;
        hoveredObject.SetActive(false);
    }

}
