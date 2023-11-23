using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Animations.Rigging;

public class EntityAIManager : MonoBehaviour
{
    [BoxGroup("References")]
    public Rigidbody rigidbody;
    
    [BoxGroup("Movement")]
    public float speed;
    [BoxGroup("Movement")]
    public float speedMult;
    [BoxGroup("Movement")]
    public Vector3 velocity;

    public void Jump()
    {
        rigidbody.AddForce(velocity, ForceMode.Impulse);
    }
}
