using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Sirenix.OdinInspector;
using UnityEngine.UI;
using System.IO;

public class PlayerSpellManager : MonoBehaviour
{
    public static PlayerSpellManager instance;
    
    [BoxGroup("References")]
    public MenuManager menuManager;
    [BoxGroup("References/Player")]    
    public PlayerHealthManager playerHealthManager;
    [BoxGroup("References/Player")]    
    public PlayerMovement playerMovement;
    [BoxGroup("References/Player")]    
    public PlayerEquipmentManager playerEquipmentManager;

    [BoxGroup("UI")]
    public Slider manaBarSlider;

#region Spells    
    [BoxGroup("Spells")]
    public Spell currentSpell;
    [BoxGroup("Spells")]
    public SpellUsage spellUsage;
    [BoxGroup("Spell Casting")]
    public GameObject spellProjectile;
    [BoxGroup("Spell Casting")]
    public Transform spellSpawnPoint;
    [BoxGroup("Spell Casting")]
    public GameObject spellGoPoint;
    [BoxGroup("Spell Casting")]
    public LayerMask spellGoMask;
    [BoxGroup("Spell Casting")]
    public GameObject spellGoImage;
    [BoxGroup("Spell Casting")]
    public bool isCurrentlyHoldingSpell;
    [BoxGroup("Spell Casting")]
    public bool isCurrentlyCasting;
    
    [BoxGroup("Spell Casting/Mana")]
    public float currentMana;
    [BoxGroup("Spell Casting/Mana")]
    public float maxMana;
    [BoxGroup("Spell Casting/Mana")]
    public float manaRegenTime;
    [BoxGroup("Spell Casting/Mana")]
    public float manaRegenTimer;
    [BoxGroup("Spell Casting/Mana")]
    public float manaRegenAmount = 1;


    [BoxGroup("Spell Effects")]
    [BoxGroup("Spell Effects/Bounce")]
    public float bounceWaitTime;
    [BoxGroup("Spell Effects/Bounce")]
    public float bounceWaitTimer;
    [BoxGroup("Spell Effects/Bounce")]
#endregion

    void Awake() {
        instance = this;
    }

    public void Start()
    {
        currentMana = maxMana;
        UpdateManaBar();
        manaBarSlider.value = currentMana;
    }

    void Update() {
        
        if(currentMana < maxMana){
            manaRegenTimer -= Time.deltaTime;
            manaBarSlider.value = currentMana;

            if(manaRegenTimer <= 0){
                currentMana += manaRegenAmount;
                manaRegenTimer = manaRegenTime;
            }
        }

        if(currentSpell.spellName != string.Empty && !menuManager.isInMenu){
            if(currentMana >= currentSpell.manaCost){
                if(InputManager.instance.castSpell){
                    isCurrentlyHoldingSpell = true;
                    isCurrentlyCasting = true;
                    playerMovement.SetRotation();
                    spellGoPoint.SetActive(true);

                    if(!PlayerCameraManager.instance.isMagicCam && currentSpell.effects[0].effectName != "Self"){
                        PlayerCameraManager.instance.MagicCamera();
                    }

                    RaycastHit hit;
                    if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity, spellGoMask, QueryTriggerInteraction.Ignore)){
                        spellGoPoint.transform.position = hit.point;
                        spellGoPoint.transform.LookAt(Camera.main.transform);
                        float distance = Vector3.Distance(Camera.main.transform.position, hit.point);
                        distance = distance / 10;
                        spellGoImage.transform.localScale = new Vector3(distance, distance, distance);
                    }
        
                }else{
                    if(isCurrentlyHoldingSpell){
                        isCurrentlyHoldingSpell = false;
                        InputManager.instance.castSpell = false;
                        spellGoPoint.SetActive(false);
                        PlayerCameraManager.instance.NormalCamera();
                    }
                }   
            }
        }

        if(!isCurrentlyHoldingSpell && isCurrentlyCasting){
            CastSpell();
            isCurrentlyCasting = false;
        }

        if(bounceWaitTimer >= -1){
            bounceWaitTimer -= Time.deltaTime;
        }

        if(bounceWaitTimer <= 0 && spellUsage.isBounce){
            playerMovement.isBounce = true;

            spellUsage.isBounce = false;
        }
    }

    public void UpdateManaBar()
    {
        manaBarSlider.maxValue = maxMana;
    }

    public void CastSpell()
    {
        CheckSpellEffects();
        currentMana -= currentSpell.manaCost;
        //Changes the type of spell based off the first effect
        switch (currentSpell.effects[0].effectName)
        {
            case "Self":
                StartCoroutine(SpellAnimationCo("Self"));
            break;    
            case "Ranged":
                StartCoroutine(SpellAnimationCo("Ranged"));
                GameObject newSpellProjectile = Instantiate(spellProjectile);
                newSpellProjectile.transform.position = spellSpawnPoint.position;
                newSpellProjectile.GetComponent<SpellProjectile>().spellUsage = spellUsage;
                playerMovement.SetRotation();
            break; 
            case "Target":
                //Do target stuff
            break; 
            case "Touch":
                //Do touch stuff
            break;     
        } 
    }
    public IEnumerator SpellAnimationCo(string name)
    {
        switch (name)
        {
            case "Self":
                playerMovement.playerAnim.SetBool("isSelfSpell", true);
            break;    
            case "Ranged":
                playerMovement.playerAnim.SetBool("isRangedSpell", true);
            break; 
            case "Target":
                //Do target stuff
            break; 
            case "Touch":
                //Do touch stuff
            break;     
        } 

        yield return new WaitForSeconds(0.35f);
         
        if(name == "Self"){
            if(spellUsage.isHealing){
                playerHealthManager.HealthChange(5 * spellUsage.amplifyModifier, false);
            }
            if(spellUsage.isDamage){
                playerHealthManager.HealthChange(5 * spellUsage.amplifyModifier, true);
            }
            if(spellUsage.isQuickening){
                playerMovement.speedMult = 2 * spellUsage.amplifyModifier;
                playerMovement.speedTimer = playerMovement.speedTime * spellUsage.lengthenModifier;    
            }
            if(spellUsage.isSlowing){
                playerMovement.speedMult = 0.5f * spellUsage.amplifyModifier;
                playerMovement.speedTimer = playerMovement.speedTime * spellUsage.lengthenModifier;    
            }
            if(spellUsage.isJump){
                playerMovement.velocity.y = 10 * spellUsage.amplifyModifier;
            }
            if(spellUsage.isBounce){
                bounceWaitTimer = bounceWaitTime;
            }
            if(spellUsage.isLeaping){
                playerMovement.isLeap = true;
                playerMovement.direction += Camera.main.transform.forward * spellUsage.amplifyModifier;
                playerMovement.velocity.y = 10 * spellUsage.amplifyModifier;
            }
            if(spellUsage.isStrengthening){
                playerEquipmentManager.attackModifier = 2 * spellUsage.amplifyModifier;
            }
            if(spellUsage.isWeakening){
                playerEquipmentManager.attackModifier = 0.5f * spellUsage.amplifyModifier;
            }  
        }

        playerMovement.playerAnim.SetBool("isSelfSpell", false);
        playerMovement.playerAnim.SetBool("isRangedSpell", false);
    }

    public void CheckSpellEffects()
    {
        spellUsage = new SpellUsage();

        //God help me
        for (int i = 0; i < currentSpell.effects.Count; i++)
        {        
            switch (currentSpell.effects[i].effectName)
            {
            #region Effects
                case "Frenzy":
                   spellUsage.isFrenzy = true;
                break;
                case "Calming":
                    spellUsage.isCalming = true;
                break;
                case "Damage":
                    spellUsage.isDamage = true;
                break;
                case "Heal":
                    spellUsage.isHealing = true;
                break;
                case "Jump":
                    spellUsage.isJump = true;
                break;
                case "Leap":
                    spellUsage.isLeaping = true;
                break;
                case "Bounce":
                    spellUsage.isBounce = true;
                break;
                case "Quicken":
                    spellUsage.isQuickening = true;
                break;
                case "Slowness":
                    spellUsage.isSlowing = true;
                break;
                case "Strengthen":
                    spellUsage.isStrengthening = true;
                break;
                case "Weaken":
                    spellUsage.isWeakening = true;
                break;
            #endregion
            
            #region Modifiers
                case "Accelerate":
                    spellUsage.acceleratingModifier++; 
                break;
                case "Delay":
                    spellUsage.delayModifier++;
                break;
                case "AOE":
                    spellUsage.aoeModifier++;
                break;
                case "Amplify":
                    spellUsage.amplifyModifier++;
                break;
                case "Dampen":
                    spellUsage.amplifyModifier--;
                break;
                case "Lengthen":
                    spellUsage.lengthenModifier++;
                break;
                case "Pierce":
                    spellUsage.peirceModifier++;
                break;
            #endregion
            }
        }
    }
}
