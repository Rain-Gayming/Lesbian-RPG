using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;

[RequireComponent(typeof(EntityHealthManager))]
public class EntityHealthDisplay : MonoBehaviour
{
    [BoxGroup("References")]
    public EntityHealthManager healthManager;
    [BoxGroup("References")]
    public GameObject healthObject;
    [BoxGroup("References")]
    public TMP_Text healthText;
    [BoxGroup("References")]
    public Slider healthSlider;

    public void Start()
    {
        if(healthObject){
            healthObject.SetActive(true);
        }
        UpdateDisplay();
    }

    public void UpdateDisplay()
    {
        if(healthText){
            healthText.text = "[" + healthManager.currentHealth + " / " + healthManager.maxHealth + "]";
        }
        else if(healthSlider){
            healthSlider.maxValue = healthManager.maxHealth;
            healthSlider.value = healthManager.currentHealth;

            if(healthManager.currentHealth < healthManager.maxHealth){
                healthSlider.gameObject.SetActive(true);
            }
        }
    }
}
