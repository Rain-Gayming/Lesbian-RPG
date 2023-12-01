using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using JetBrains.Annotations;
using Sirenix.Serialization;
using Unity.Mathematics;

public class EntityHealthManager : MonoBehaviour
{    
    [BoxGroup("References")]
    public EntityHealthDisplay healthDisplay;

    [BoxGroup("Health")]
    public float maxHealth;
    [BoxGroup("Health")]
    public float currentHealth;
    [BoxGroup("Health/Regen")]
    public float healthRegenAmount = 1;
    [BoxGroup("Health/Regen")]
    public float healthRegenTime = 1;
    [BoxGroup("Health/Regen")]
    public float healthRegenTimer;

    private void Start() {
        maxHealth = Mathf.RoundToInt(maxHealth);
        currentHealth = maxHealth;
        
        if(healthDisplay){
            healthDisplay.UpdateDisplay();
        }
    }

    public void Update()
    {
        if(currentHealth < maxHealth){
            healthRegenTimer -= Time.deltaTime;

            if(healthRegenTimer <= 0){
                HealthChange(healthRegenAmount, false);
                healthRegenTimer = healthRegenTime;
            }
        }
    }

    [Button]
    public void HealthChange(float change, bool damage)
    {
        if(damage){
            currentHealth -= change;
            healthRegenTimer = healthRegenTime;
        }else{
            currentHealth += change;
        }

        if(currentHealth <= 0){
            Die();
        }

        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        
        if(healthDisplay){
            healthDisplay.UpdateDisplay();
        }
    }

    public virtual void Die()
    {

    }
}
