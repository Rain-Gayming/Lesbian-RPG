using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class SpellAOE : MonoBehaviour
{
    [BoxGroup("Spell")]

    public SpellUsage spellUsage;
    public void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<EntityManager>()){
            other.GetComponent<EntityManager>().GetSpellEffects(spellUsage);
        }
        
        StartCoroutine(StopCo());
    }

    public IEnumerator StopCo()
    {
        yield return new WaitForSeconds(0.2f);
        gameObject.SetActive(false);
    }
}
