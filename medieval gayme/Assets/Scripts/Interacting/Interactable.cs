using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Sirenix.OdinInspector;
using Mono.CSharp;
using UnityEngine.InputSystem;


public class Interactable : MonoBehaviour
{
    [BoxGroup("Interactable")]
    public bool destroyOnUse;
    [BoxGroup("Interactable")]
    public bool beingLookedAt;
    [BoxGroup("Interactable")]
    public bool showInteractableUI = true;

    [BoxGroup("Interactable UI")]
    public TMP_Text interactPromptText;
    [BoxGroup("Interactable UI")]
    public UIBillboard interactBillboard;
    [BoxGroup("Interactable UI")]
    public UIBillboard nameBillboard;

    public void FixedUpdate()
    {
        if(showInteractableUI){
            interactBillboard.beingLookedAt = beingLookedAt;
            nameBillboard.beingLookedAt = beingLookedAt;
            if(beingLookedAt)
            {
                interactPromptText.text = "Press '" + InputManager.instance.inputs.Game.Interact.GetBindingDisplayString(0) + "' to interact";
                
                if(InteractionCamera.instance.hit.transform != transform){
                    beingLookedAt = false;
                }
            }    
        }else{
            interactBillboard.beingLookedAt = false;
            nameBillboard.beingLookedAt = false;
            beingLookedAt = false;
        }
    }
    
    public virtual void Interact()
    {
        if(destroyOnUse){
            Destroy(gameObject);
        }
    }
}
