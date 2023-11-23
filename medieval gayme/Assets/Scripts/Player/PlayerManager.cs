using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    [BoxGroup("Player Info")]
    public string playerName;
    [BoxGroup("Player Info/Leveling")]
    public int currentLevel;
    [BoxGroup("Player Info/Leveling")]
    public float currentExp;
    [BoxGroup("Player Info/Leveling")]
    public float expToNext;

    public void Awake() 
    {
        instance = this;
    }

    public void AddExp(float exp)
    {
        currentExp += exp;

        if(currentExp >= expToNext){
            currentExp = 0;
            currentLevel++;
        }
    }
}
