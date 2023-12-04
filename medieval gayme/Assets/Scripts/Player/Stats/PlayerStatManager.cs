using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerStatManager : MonoBehaviour
{
    [BoxGroup("Stats")]
    public CStats baseStats;
    [BoxGroup("Stats")]
    public CStats defaultStats; 
    [BoxGroup("Stats")]
    public CStats currentStats;
    private CStats emptyStats;

    public void Start()
    {
        if(baseStats == emptyStats){
            baseStats = defaultStats;
        }
        currentStats = baseStats;
    }
}
