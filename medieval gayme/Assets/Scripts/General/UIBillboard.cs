using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class UIBillboard : MonoBehaviour
{
    [BoxGroup("References")]
    public Transform cam;
    [BoxGroup("References")]
    public GameObject uiBillboard;
    
    [BoxGroup("Regional")]
    public bool playerInRegion;
    
    [BoxGroup("Looking")]
    public bool showOnLook;
    [BoxGroup("Looking")]
    public bool beingLookedAt;

    public void Start()
    {
        cam = Camera.main.gameObject.transform;
    }

    public void FixedUpdate()
    {
        if(playerInRegion || beingLookedAt){
            uiBillboard.SetActive(true);
            uiBillboard.transform.LookAt(cam);
        }else{
            uiBillboard.SetActive(false);
        }

        if(showOnLook){
            uiBillboard.SetActive(beingLookedAt);
        }
    }

    public void SetLookingAt(bool value)
    {
        beingLookedAt = value;
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && !showOnLook){
            playerInRegion = true;
        }    
    }

    public void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player" && !showOnLook){
            playerInRegion = false;
        }            
    }
}
