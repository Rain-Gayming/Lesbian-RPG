using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class EntityCombatManager : MonoBehaviour
{
    [BoxGroup("References")]
    public EntityAIManager aiManager;

    [BoxGroup("Target")]
    public bool targetInRange;
    [BoxGroup("Target")]
    public Transform currentTarget;

    [BoxGroup("Stat Modifiers")]
    public float attackModifier;

    public void Update()
    {
        if(targetInRange){
            aiManager.SetPoint(currentTarget.position, currentTarget);
        }
    }
}
