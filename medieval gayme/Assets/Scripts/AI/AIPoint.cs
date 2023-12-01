using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPoint : MonoBehaviour
{ 
    public GameObject visual;

    public void Start()
    {
        if(visual){
            visual.gameObject.SetActive(false); 
        }
    }
}
