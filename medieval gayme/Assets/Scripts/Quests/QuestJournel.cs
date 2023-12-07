using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine.UI;

public class QuestJournel : MonoBehaviour
{
    public static QuestJournel instance;
    [BoxGroup("UI")]
    public Menu questJournel;
    [BoxGroup("UI/Quests")]
    public GameObject questPrefab;
    [BoxGroup("UI/Quests")]
    public Transform questPosition;
    [BoxGroup("UI/Quests")]
    public List<GameObject> questButtonList;
    
    [BoxGroup("UI/Quest Information")]
    public VerticalLayoutGroup layoutGroup;
    [BoxGroup("UI/Quest Information")]
    public TMP_Text questNameText;    
    [BoxGroup("UI/Quest Information")]
    public GameObject questStepPrefab;
    [BoxGroup("UI/Quest Information")]
    public Transform questStepPosition;
    [BoxGroup("UI/Quest Information")]
    public List<GameObject> questStepList;
    
    
    public void Awake()
    {
        instance = this;
    }
    
    public void Start()
    { 
    }

    public void Update()
    {
        if(InputManager.instance.questJournel){
            InputManager.instance.questJournel = false;

            MenuManager.instance.ChangeMenuWithPause(questJournel);
        }
    }

    public void UpdateQuestJournel(List<Quest> quests)
    {
        for (int i = 0; i < questButtonList.Count; i++)
        {
            Destroy(questButtonList[i]);
        }

        questButtonList.Clear();

        for (int i = 0; i < quests.Count; i++)
        {
            GameObject newQuest = Instantiate(questPrefab);
            newQuest.transform.SetParent(questPosition);

            newQuest.GetComponent<QuestButton>().quest = quests[i];
            questButtonList.Add(newQuest);
        }
    }

    [Button]
    public void UpdateQuestSteps(Quest quest)
    {
        questNameText.text = quest.currentQuest.questName;
        for (int i = 0; i < questStepList.Count; i++)
        {
            Destroy(questStepList[i]);
        }

        questStepList.Clear();

        for (int i = 0; i < quest.currentQuest.questSteps.Count; i++)
        {
            if(quest.currentQuest.questSteps[i].step <= quest.currentStep){
                GameObject newStep = Instantiate(questStepPrefab);
                newStep.transform.SetParent(questStepPosition);

                newStep.GetComponentInChildren<TMP_Text>().text = quest.currentQuest.questSteps[i].stepDescription;
                questStepList.Add(newStep);
                newStep.transform.position = new Vector3(100, 100);
                newStep.SetActive(false);
                newStep.SetActive(true);
                Canvas.ForceUpdateCanvases();
            }
        }
    }
}
