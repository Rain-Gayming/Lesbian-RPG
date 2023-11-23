using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using JetBrains.Annotations;
using Sirenix.Serialization;

public class EntityHealthManager : MonoBehaviour
{    
    [BoxGroup("References")]
    public EntityHealthDisplay healthDisplay;

    [BoxGroup("Health")]
    public float maxHealth;
    [BoxGroup("Health")]
    public float currentHealth;

    private void Start() {
        currentHealth = maxHealth;
        
        if(healthDisplay){
            healthDisplay.UpdateDisplay();
        }
    }

    [Button]
    public void HealthChange(float change, bool damage)
    {
        if(damage){
            currentHealth -= change;
        }else{
            currentHealth += change;
        }

        if(healthDisplay){
            healthDisplay.UpdateDisplay();
        }

        if(currentHealth <= 0){
            Die();
        }
        if(currentHealth >= maxHealth){
            currentHealth = maxHealth;
        }
    }

    public virtual void Die()
    {

    }
}
