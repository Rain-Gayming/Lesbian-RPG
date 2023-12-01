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
    
    [BoxGroup("NPC Stats")]
    public float minHealth;
    [BoxGroup("NPC Stats")]
    public float maxHealth;
    
    [BoxGroup("NPC Patrolling")]
    public float patrolRadius;
    
    [BoxGroup("NPC Aggression")]
    public Aggression aggressionLevel;
    [BoxGroup("NPC Aggression/Aggressive")]
    [ShowIf("aggressionLevel", Aggression.aggressive)]
    public float chaseRange;
}

public enum Aggression
{
    passive,
    neutral,
    aggressive,
}