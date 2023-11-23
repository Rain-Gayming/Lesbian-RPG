using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;
    public PlayerControls inputs;

    [BoxGroup("Movement")]
    public Vector2 walk;
    [BoxGroup("Movement")]
    public Vector2 cameraLook;
    [BoxGroup("Movement")]
    public bool jump;
    [BoxGroup("Movement")]
    public bool crouch;
    [BoxGroup("Movement")]
    public bool sprint;

    
    [BoxGroup("Game")]
    public bool pause;
    [BoxGroup("Game")]
    public bool inventory;
    [BoxGroup("Game")]
    public bool dropItem;
    [BoxGroup("Game")]
    public bool spellBook;
    [BoxGroup("Game")]
    public bool interact;
    [BoxGroup("Game")]
    public Vector3 zoomValue;
    [BoxGroup("Game")]
    public bool questJournel;

    [BoxGroup("Combat")]
    public bool castSpell;

    public void Awake()
    {
        instance = this;
    }

    public void Start()
    {
        inputs = new PlayerControls();
        inputs.Enable();
    }

    public void FixedUpdate()
    {
        walk = inputs.Movement.Walk.ReadValue<Vector2>();
        cameraLook = inputs.Movement.CameraLook.ReadValue<Vector2>();
        zoomValue = inputs.Game.Zoom.ReadValue<Vector2>();
        
        inputs.Movement.Jump.performed += _ => jump = true;
        inputs.Movement.Jump.canceled += _ => jump = false;
        inputs.Movement.Crouch.performed += _ => crouch = true;
        inputs.Movement.Crouch.canceled += _ => crouch = false;        
        inputs.Movement.Sprint.performed += _ => sprint = true;
        inputs.Movement.Sprint.canceled += _ => sprint = false;        
        inputs.Movement.Jump.performed += _ => jump = true;
        inputs.Movement.Jump.canceled += _ => jump = false;
        
        inputs.Game.Pause.performed += _ => pause = true;
        inputs.Game.Pause.canceled += _ => pause = false;
        inputs.Game.Inventory.performed += _ => inventory = true;
        inputs.Game.Inventory.canceled += _ => inventory = false;
        inputs.Game.SpellBook.performed += _ => spellBook = true;
        inputs.Game.SpellBook.canceled += _ => spellBook = false;
        inputs.Game.QuestJournel.performed += _ => questJournel = true;
        inputs.Game.QuestJournel.canceled += _ => questJournel = false;

        inputs.Game.DropItem.performed += _ => dropItem = true;
        inputs.Game.DropItem.canceled += _ => dropItem = false;        
        inputs.Game.Interact.performed += _ => interact = true;
        inputs.Game.Interact.canceled += _ => interact = false;
        
        inputs.Combat.CastSpell.performed += _ => castSpell  = true;
        inputs.Combat.CastSpell.canceled += _ => castSpell = false;
    }
}
