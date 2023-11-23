using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using TMPro;
using Unity.VisualScripting;

[RequireComponent(typeof(EntityHealthManager))]
public class EntityHealthDisplay : MonoBehaviour
{
    [BoxGroup("References")]
    public EntityHealthManager healthManager;
    [BoxGroup("References")]
    public TMP_Text healthText;

    public void UpdateDisplay()
    {
        healthText.text = "[" + healthManager.currentHealth + " / " + healthManager.maxHealth + "]";
    }
}
