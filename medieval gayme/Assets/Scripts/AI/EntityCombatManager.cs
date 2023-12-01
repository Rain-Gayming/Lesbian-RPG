using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class EntityCombatManager : MonoBehaviour
{
    [BoxGroup("Target")]
    public bool targetInRange;
    [BoxGroup("Target")]
    public Transform currentTarget;

    [BoxGroup("Stat Modifiers")]
    public float attackModifier;
}
