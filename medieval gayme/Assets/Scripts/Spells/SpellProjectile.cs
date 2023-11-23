using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class SpellProjectile : MonoBehaviour
{
    [BoxGroup("References")]
    public Rigidbody rb;
    [BoxGroup("References")]
    public SpellDatabase spellDatabase;

    [BoxGroup("Spell")]
    public Spell spell;
    [BoxGroup("Spell")]
    public SpellUsage spellUsage;
    
    [BoxGroup("Velocity")]
    public float velocity;
    [BoxGroup("Velocity")]
    public float velocityMultiplier;

    public void Start()
    {
        velocityMultiplier = 1;
        velocity = velocity * velocityMultiplier;

        rb.AddForce(Camera.main.transform.forward * velocity, ForceMode.Impulse);
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<EntityManager>()){
            other.GetComponent<EntityManager>().GetSpellEffects(spellUsage);
        }
        if(other.tag != "Player" && other.tag != "World" && other.tag != "UI")
            Destroy(gameObject);
    }
}
