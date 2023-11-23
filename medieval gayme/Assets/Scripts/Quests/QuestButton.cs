using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using TMPro;

public class QuestButton : MonoBehaviour
{
    [BoxGroup("UI")]
    public TMP_Text questNameText;

    [BoxGroup("Quest")]
    public Quest quest;

    public void Start()
    {
        questNameText.text = quest.currentQuest.questName;
    }

    public void OnPressed()
    {
        QuestJournel.instance.UpdateQuestSteps(quest);
        QuestJournel.instance.UpdateQuestSteps(quest);
        QuestJournel.instance.UpdateQuestSteps(quest);
    }
}
