using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.AI;

public class EntityCombatManager : MonoBehaviour
{
    [BoxGroup("References")]
    public EntityAIManager entityAIManager;
    [BoxGroup("References")]
    public EntityManager entityManager;
    [BoxGroup("References")]
    public NavMeshAgent agent;
    [BoxGroup("References")]
    public Animator anim;

    [BoxGroup("Combat")]
    public Aggression currentAgression;
    [BoxGroup("Combat")]
    public bool isCalmed;
    [BoxGroup("Combat")]
    public float calmingTimer;

    [BoxGroup("Target")]
    public bool targetInRange;
    [BoxGroup("Target")]
    public Transform currentTarget;
    
    [BoxGroup("Alert")]
    public bool hasAlerted;

    [BoxGroup("Stat Modifiers")]
    public float attackModifier;

    [BoxGroup("Attacking")]
    public float attackTimer;
    [BoxGroup("Attacking")]
    public float attackTime;

    public void Start()
    {
        entityAIManager = GetComponent<EntityAIManager>();
        entityManager = GetComponent<EntityManager>();
        
        currentAgression = entityAIManager.npcObject.aggressionLevel;
    }

    public void Update()
    {
        if(targetInRange && currentAgression == Aggression.aggressive && !isCalmed){
            entityAIManager.SetPoint(currentTarget.position, currentTarget);

            float dist = Vector3.Distance(transform.position, currentTarget.position);

            if(dist <= (entityAIManager.npcObject.attackRange + 2f)){
                if(attackTimer <= 0){
                    StartCoroutine(AttackCo());
                }else{
                    attackTimer -= Time.deltaTime;
                }

                if(!hasAlerted){
                    
                    hasAlerted = true;
                }
            }
        }

        if(isCalmed){
            calmingTimer -= Time.deltaTime;

            if(calmingTimer <= 0){
                isCalmed = false;
            }
        }
    }
    void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player"){
            currentTarget = entityAIManager.playerObject.transform;
            targetInRange = true;
            entityAIManager.agent.stoppingDistance = entityManager.npcObject.attackRange;
        }    
    }
    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player"){
            currentTarget = null;
            targetInRange = false;
            entityAIManager.SetPoint(Vector3.zero, null);
            entityAIManager.agent.stoppingDistance = 0;
        }    
    }
    public IEnumerator AttackCo()
    {
        anim.SetBool("isMeleeAttacking", true);
        transform.LookAt(currentTarget);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        yield return new WaitForSeconds(0.2f);
        anim.SetBool("isMeleeAttacking", false);
        attackTimer = attackTime;
    }
}
