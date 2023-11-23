using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "Quests/Quest")]
public class QuestObject : ScriptableObject
{
    [BoxGroup("Quest Info")]
    public string questName;
    [BoxGroup("Quest Info")]
    [TextArea] public string questDescription;
    [BoxGroup("Quest Info")]
    public List<QuestStep> questSteps;

    
    [BoxGroup("Quest Rewards")]
    public float expReward;
    public List<InventoryItem> itemRewards;

    [Button]
    public void SetStepPoints()
    {
        for (int i = 0; i < questSteps.Count; i++)
        {
            questSteps[i].step = i;
        }
    }
}
