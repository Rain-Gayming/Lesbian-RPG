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
    
    [BoxGroup("NPC Enemy")]
    public float patrolRadius;
    [BoxGroup("NPC Enemy")]
    public float alertRadius;
    
    [BoxGroup("NPC Aggression")]
    public Aggression aggressionLevel;
    [BoxGroup("NPC Aggression/Aggressive")]
    [ShowIf("aggressionLevel", Aggression.aggressive)]
    public float chaseRange;
    [BoxGroup("NPC Aggression/Aggressive")]
    [ShowIf("aggressionLevel", Aggression.aggressive)]
    public float attackRange;
}

public enum Aggression
{
    passive,
    neutral,
    aggressive,
}