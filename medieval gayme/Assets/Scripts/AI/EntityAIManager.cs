using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.AI;
using Unity.VisualScripting;

public class EntityAIManager : MonoBehaviour
{
    [BoxGroup("References")]
    public EntityManager entityManager;
    [BoxGroup("References")]
    public NavMeshAgent agent;
    [BoxGroup("References")]
    public Rigidbody rigidbody;
    
    [BoxGroup("AI")]
    public AIState currentState;
    [BoxGroup("AI")]
    public List<GameObject> personalPoints;
    [BoxGroup("AI")]
    public GameObject currentPoint;
    [BoxGroup("AI/Waiting")]
    public float waitTimeMin;
    [BoxGroup("AI/Waiting")]
    public float waitTimeMax;
    [BoxGroup("AI/Waiting")]
    public float waitTimer;
    [BoxGroup("AI/Waiting")]
    public bool waiting;
    
    [BoxGroup("Movement")]
    public float currentSpeed;
    [BoxGroup("Movement")]
    public float speed;
    [BoxGroup("Movement")]
    public float speedMult;
    [BoxGroup("Movement")]
    public Vector3 velocity;

    void Start() {
        entityManager = GetComponent<EntityManager>();
        agent = GetComponent<NavMeshAgent>();

        SetPoint(Vector3.zero, null);
    }

    public void Jump()
    {
        rigidbody.AddForce(velocity, ForceMode.Impulse);
    }

    public void Update()
    {
        if(currentSpeed != (speed * speedMult)){
            currentSpeed = speed * speedMult;

            agent.speed = currentSpeed;
        }

        if(currentPoint){
            if(transform.position.x == currentPoint.transform.localPosition.x){
                if(transform.position.z == currentPoint.transform.localPosition.z){
                    waitTimer = Random.Range(waitTimeMin, waitTimeMax);
                    waitTimer -= Time.deltaTime;
                    waiting = true;
                    currentPoint = null;
                    print("points are same");
                }
            }
        }
        
        if(waitTimer <= 0 && waiting){
            SetPoint(Vector3.zero, null);
        }
    }   

    public Vector3 FindPoint()
    {   
        int currentPointVal = Random.Range(0, AIPointManager.instance.points.Count + personalPoints.Count);
        
        if(currentPointVal <= AIPointManager.instance.points.Count){
            currentPoint = AIPointManager.instance.points[currentPointVal];
        }else if(currentPointVal > AIPointManager.instance.points.Count){
            currentPoint = personalPoints[currentPointVal - AIPointManager.instance.points.Count];
        }

        return currentPoint.transform.position;
    } 

    public void SetPoint(Vector3 position, Transform trans)
    {     
        if(trans == null || position == Vector3.zero){
            FindPoint();
        }else{
            if(trans){
                currentPoint = trans.gameObject;
            }
        }
        waiting = false;
        
        float dist = Vector3.Distance(currentPoint.transform.position, entityManager.playerObject.transform.position);

        if(dist <= entityManager.npcObject.patrolRadius){
    
            agent.SetDestination(currentPoint.transform.position);

        }else{
            SetPoint(Vector3.zero, null);
        }
    }

    
}


public enum AIState
{
    roaming,
    waiting,
    chasing,
    attacking,
}