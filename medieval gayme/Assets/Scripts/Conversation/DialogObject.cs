using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "NPC/Dialog/Dialog")]
public class DialogObject : ScriptableObject
{
    [BoxGroup("Dialog Info")]
    public DialogLineObject firstLine;
    [BoxGroup("Dialog Info")]
    public DialogLineObject returningLine;
    [BoxGroup("Dialog Info")]
    public NPCObject npc;
}
