using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "NPC/NPC Object")]
public class NPCObject : ScriptableObject
{
    [BoxGroup("NPC Info")]
    public string npcName;
    [BoxGroup("NPC Info")]
    public DialogObject dialog;
}
