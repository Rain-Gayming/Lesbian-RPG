using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class PlayerMovement : MonoBehaviour
{
    [BoxGroup("References")]
    public CharacterController controller;
    [BoxGroup("References")]
    public Transform cam;
    [BoxGroup("References")]
    public Animator playerAnim;
    [BoxGroup("References")]
    public Transform playerModel;
    
    [BoxGroup("Movement")]
    public MovementState moveState;
    [BoxGroup("Movement")]
    public Vector3 direction;
    [BoxGroup("Movement")]
    public float currentSpeed = 6f;
    [BoxGroup("Movement")]
    public float speedMult = 1;
    [BoxGroup("Movement")]
    public float walkSpeed;
    [BoxGroup("Movement")]
    public float sprintSpeed;
    [BoxGroup("Movement")]
    public float crouchSpeed;
    
    [BoxGroup("Rotations")]
    public float turnSmoothTime = .1f;
    [BoxGroup("Rotations")]
    public float turnSmoothVelocity;

    [BoxGroup("Gravity")]
    public float gravity = -9.81f;
    [BoxGroup("Gravity")]
    public Vector3 velocity;

    [BoxGroup("Jump")]
    public float jumpHeight;
    [BoxGroup("Jump")]
    public Transform groundCheck;
    [BoxGroup("Jump")]
    public float groundDistance;
    [BoxGroup("Jump")]
    public LayerMask groundMask;
    [BoxGroup("Jump")]
    public bool grounded;

    [BoxGroup("Spell Effects")]
    [BoxGroup("Spell Effects/Bounce")]
    public bool isBounce;
    public bool hasSetBounceTime;
    [BoxGroup("Spell Effects")]
    public bool isLeap;
    [BoxGroup("Spell Effects/Speed Change")]
    public float speedTime;
    [BoxGroup("Spell Effects/Speed Change")]
    public float speedTimer;
    // Start is called before the first frame update
    void Start()
    {
        currentSpeed = walkSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameManager.instance.paused){
            grounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
            playerAnim.SetBool("isFalling", !grounded);
            if(grounded){
                //Spell Stuff
                if(isBounce){
                    velocity.y = 7.5f * SpellBook.instance.spellUsage.amplifyModifier;
                    isBounce = false;
                    hasSetBounceTime = false;
                }
                if(isLeap){
                    direction = Vector3.zero;
                    isLeap = false;
                }

                //Jump Stuff
                if(velocity.y < 0)
                {
                    velocity.y = -2f;
                }

                if(InputManager.instance.jump)
                {
                    InputManager.instance.jump = false;
                    velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                }
            }else{
            }

            if(InputManager.instance.sprint){
                InputManager.instance.sprint = false;

                if(moveState == MovementState.walking){
                    moveState = MovementState.sprinting;
                }
                else if(moveState == MovementState.sprinting){
                    moveState = MovementState.walking;
                }
            }
            if(InputManager.instance.crouch){
                InputManager.instance.sprint = false;
                
                if(moveState == MovementState.walking){
                    moveState = MovementState.crouching;
                }
                else if(moveState == MovementState.crouching){
                    moveState = MovementState.walking;
                }
            }

            if(speedTimer > 0){
                speedTimer -= Time.deltaTime; 
            }else{
                if(speedMult != 1){
                    speedMult = 1;
                }
            }

            float horizontal = InputManager.instance.walk.x;
            float vertical = InputManager.instance.walk.y;
            direction = new Vector3(horizontal, 0f, vertical).normalized;

            if(direction != Vector3.zero){
                playerAnim.SetBool("isWalking", true);
            }else{
                playerAnim.SetBool("isWalking", false);
            }

            switch (moveState)
            {
                case MovementState.walking:
                    currentSpeed = walkSpeed;
                break;
                case MovementState.sprinting:
                    currentSpeed = sprintSpeed;
                break;
                case MovementState.crouching:
                    currentSpeed = crouchSpeed;
                break;
            }


            if(direction.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(playerModel.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                playerModel.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                controller.Move(moveDir.normalized * currentSpeed * Time.deltaTime * speedMult);
            }
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }
    }

    public void SetRotation()
    {
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
        playerModel.rotation = Quaternion.Euler(0f, targetAngle, 0f);
    }
}

public enum MovementState
{
    walking,
    sprinting,
    crouching
}