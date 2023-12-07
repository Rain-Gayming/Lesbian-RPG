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

    [BoxGroup("Invincivibility")]
    public float invincivibilityTime = 0.3f;
    [BoxGroup("Invincivibility")]
    public float invincivibilityTimer;
    [BoxGroup("Invincivibility")]
    public bool canTakeDamage;


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
        if(healthRegenTimer > 0){
            healthRegenTimer -= Time.deltaTime;
        }

        if(currentHealth < maxHealth){

            if(healthRegenTimer <= 0){
                HealthChange(healthRegenAmount, false);
                healthRegenTimer = healthRegenTime;
            }
        }

        if(invincivibilityTimer > 0){
            canTakeDamage = false;
            invincivibilityTimer -= Time.deltaTime;                                                             
        }else{
            canTakeDamage = true;
        }
    }

    [Button]
    public void HealthChange(float change, bool damage)
    {
        if(damage && canTakeDamage){
            currentHealth -= change;
            healthRegenTimer = healthRegenTime;
            invincivibilityTimer = invincivibilityTime;
            canTakeDamage = false;
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
