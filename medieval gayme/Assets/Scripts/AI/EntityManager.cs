using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering;

public class EntityManager : MonoBehaviour
{
    [BoxGroup("References")]
    public NPCObject npcObject;
    
    [BoxGroup("References")]
    public SphereCollider sphereCollider;
    [BoxGroup("References/Scripts")]
    public EntityHealthManager entityHealthManager;
    [BoxGroup("References/Scripts")]
    public EntityAIManager entityAIManager;
    [BoxGroup("References/Scripts")]
    public EntityCombatManager entityCombatManager;



    [BoxGroup("UI")]
    public TMP_Text nameText;

    public void Awake()
    {
        sphereCollider.radius = npcObject.chaseRange;
        sphereCollider.center = new Vector3(0, 1, npcObject.chaseRange - 0.5f);
        entityHealthManager.maxHealth = Random.Range(npcObject.minHealth, npcObject.maxHealth);
    }

    void Start() {
        nameText.text = npcObject.npcName;
    }

    public void GetSpellEffects(SpellUsage spellUsage)
    {            
        if(spellUsage.isHealing){
            entityHealthManager.HealthChange(5 * spellUsage.amplifyModifier, false);
        }
        if(spellUsage.isDamage){
            entityHealthManager.HealthChange(5 * spellUsage.amplifyModifier, true);
        }
        if(spellUsage.isQuickening){
            entityAIManager.speedMult = entityAIManager.speed * 2 * spellUsage.lengthenModifier;
        }
        if(spellUsage.isSlowing){
            entityAIManager.speedMult = entityAIManager.speed * 0.5f * spellUsage.lengthenModifier;
        }
        if(spellUsage.isJump){
            entityAIManager.velocity.y = 7 * spellUsage.amplifyModifier;
            entityAIManager.Jump();
        }
        if(spellUsage.isBounce){
            entityAIManager.velocity.y = 7 * spellUsage.amplifyModifier;
            entityAIManager.Jump();
            //bounceWaitTimer = bounceWaitTime;
        }
        if(spellUsage.isLeaping){
            entityAIManager.velocity.y = 7 * spellUsage.amplifyModifier;
            entityAIManager.Jump();
            //playerMovement.isLeap = true;
            //playerMovement.direction += Camera.main.transform.forward * spellUsage.amplifyModifier;
            //playerMovement.velocity.y = 10 * spellUsage.amplifyModifier;
        }
        if(spellUsage.isStrengthening){
            entityCombatManager.attackModifier = 2 * spellUsage.amplifyModifier;
        }
        if(spellUsage.isWeakening){
            entityCombatManager.attackModifier = 0.5f * spellUsage.amplifyModifier;
        }
        if(spellUsage.isCalming){
            entityCombatManager.isCalmed = true;
            entityCombatManager.calmingTimer = 5f * spellUsage.lengthenModifier;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, npcObject.chaseRange);
        Gizmos.color = Color.red;    
        
        Gizmos.DrawWireSphere(transform.position, npcObject.patrolRadius);
        Gizmos.color = Color.green;    
    }
}
