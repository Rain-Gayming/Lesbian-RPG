using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Mono.CSharp;

public class OpenInteractable : Interactable
{
    [BoxGroup("Lever")]
    public bool isOn;

    [BoxGroup("Lever")]
    public Animator anim;
    
    [BoxGroup("Lever")]
    public Animator toOpenAnim;

    public override void Interact()
    {
        base.Interact();

        isOn = !isOn;

        anim.SetBool("isOn", isOn);
        
        if(toOpenAnim)
            toOpenAnim.SetBool("isOn", isOn);
    }
}
