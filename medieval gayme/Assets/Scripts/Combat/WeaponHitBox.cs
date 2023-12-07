using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class WeaponHitBox : MonoBehaviour
{
    [BoxGroup("Weapon")]
    public Collider hitBox;
    [BoxGroup("Weapon")]
    public ItemObject weaponItem;
    [BoxGroup("Weapon")]
    public float damage;

    void Start() {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<EntityHealthManager>()){
            if(GetComponentInParent<EntityCombatManager>()){
                damage = weaponItem.damage * GetComponentInParent<EntityCombatManager>().attackModifier;
            }else{
                damage = weaponItem.damage * PlayerEquipmentManager.instance.attackModifier;
            }
            other.GetComponent<EntityHealthManager>().HealthChange(damage, true);
        }
    }
}
