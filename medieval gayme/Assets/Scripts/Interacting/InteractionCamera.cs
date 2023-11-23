using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class InteractionCamera : MonoBehaviour
{
    public static InteractionCamera instance;
    [BoxGroup("References")]
    public Transform interactPoint;

    
    [BoxGroup("Interacting")]
    public float interactRange;
    [BoxGroup("Interacting")]
    public LayerMask interactMask;

    [HideInInspector]
    public RaycastHit hit;

    void Awake() {
        instance = this;
    }

    public void Update()
    {

        if(Physics.Raycast(interactPoint.transform.position, interactPoint.forward, out hit ,interactRange, interactMask, QueryTriggerInteraction.Collide))
        {
            if(hit.transform.GetComponent<Interactable>()){
                hit.transform.GetComponent<Interactable>().beingLookedAt = true;
                if(InputManager.instance.interact){
                    InputManager.instance.interact = false;
                    hit.transform.GetComponent<Interactable>().Interact();
                }
            }
        }
    }    
}
