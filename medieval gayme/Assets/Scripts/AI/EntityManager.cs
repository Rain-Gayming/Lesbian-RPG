using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager : MonoBehaviour
{
    public EntityHealthManager healthManager;
    public EntityAIManager entityAIManager;
    public EntityCombatManager entityCombatManager;

    public void GetSpellEffects(SpellUsage spellUsage)
    {            
        if(spellUsage.isHealing){
            healthManager.HealthChange(5 * spellUsage.amplifyModifier, false);
        }
        if(spellUsage.isDamage){
            healthManager.HealthChange(5 * spellUsage.amplifyModifier, true);
        }
        if(spellUsage.isQuickening){
            entityAIManager.speedMult = entityAIManager.speed * 2 * spellUsage.amplifyModifier;
        }
        if(spellUsage.isSlowing){
            entityAIManager.speedMult = entityAIManager.speed * 0.5f * spellUsage.amplifyModifier;
        }
        if(spellUsage.isJump){
            entityAIManager.velocity.y = 7 * spellUsage.amplifyModifier;
            entityAIManager.Jump();
        }
        if(spellUsage.isBounce){
            //bounceWaitTimer = bounceWaitTime;
        }
        if(spellUsage.isLeaping){
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
    }
}
