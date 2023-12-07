using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Assertions.Must;

public class NPCInteractable : Interactable
{
    [BoxGroup("NPC")]
    public NPCObject npcInformation;
    [BoxGroup("NPC")]
    public bool talkedTo;

    public override void Interact()
    {
        DialogBoxUI.instance.StartDialog(npcInformation.dialog, talkedTo);

        talkedTo = true;

        transform.LookAt(FindAnyObjectByType<PlayerMovement>().transform);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
    }
}
