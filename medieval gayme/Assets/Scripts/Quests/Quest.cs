using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
    public QuestObject currentQuest;
    public int currentStep;
    public int amountKilled;

    public Quest(QuestObject _quest, int _step)
    {
        currentQuest = _quest;
        currentStep = _step;
    }
}
