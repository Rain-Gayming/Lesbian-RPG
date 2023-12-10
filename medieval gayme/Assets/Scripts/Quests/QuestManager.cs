using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Unity.Mathematics;
using UnityEngine.InputSystem.LowLevel;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;
    [BoxGroup("Quests")]
    public Quest activeQuest;
    [BoxGroup("Quests")]
    public List<Quest> quests;
    [BoxGroup("Quests")]
    public List<QuestObject> questsCompleted;

    void Awake() {
        instance = this;
    }

    public void Start()
    {
        if(quests.Count != 0)
            QuestJournel.instance.UpdateQuestJournel(quests);
    }

    public void AddQuest(QuestObject quest)
    {
        bool completedQuest = false; 
        for (int i = 0; i < questsCompleted.Count; i++)
        {
            if(questsCompleted[i] == quest){
                completedQuest = true;
            }
        }
        if(!completedQuest){
            quests.Add(new Quest(quest, 0));
            QuestJournel.instance.UpdateQuestJournel(quests);
        }
    }

    public void CheckQuestsForItems(ItemObject item, int amount)
    {
        for (int i = 0; i < quests.Count; i++)
        {
            if(quests[i].currentQuest.questSteps[quests[i].currentStep].questStepType == EQuestStepType.get){
                if(quests[i].currentQuest.questSteps[quests[i].currentStep].whatToGet == item){
                    if(amount >= quests[i].currentQuest.questSteps[quests[i].currentStep].howManyToGet ){
                        UpdateQuest(quests[i].currentQuest);
                        print("Working");
                    }
                }
                
            }
        }
    }

    public void CheckQuestsForKills(NPCObject npc)
    {
        for (int i = 0; i < quests.Count; i++)
        {
            if(quests[i].currentQuest.questSteps[quests[i].currentStep].questStepType == EQuestStepType.kill){
                if(npc == quests[i].currentQuest.questSteps[quests[i].currentStep].whatToKill){
                    quests[i].amountKilled++;

                    if(quests[i].amountKilled >= quests[i].currentQuest.questSteps[quests[i].currentStep].howManyToKill){
                        UpdateQuest(quests[i].currentQuest);
                    }
                }
            }
        }
    }

    public void UpdateQuest(QuestObject quest)
    {
        for (int i = 0; i < quests.Count; i++)
        {
            if(quests[i].currentQuest == quest){
                quests[i].currentStep++;
                if(quests[i].currentQuest.questSteps.Count <= quests[i].currentStep){
                    print("Quest Complete");
                    if(quests[i].currentQuest.itemRewards.Count > 0){
                        for (int p = 0; p < quests[i].currentQuest.itemRewards.Count; p++)
                        {
                            if(Inventory.instance.CanAddItem(quests[i].currentQuest.itemRewards[p])){
                                Inventory.instance.AddItem(quests[i].currentQuest.itemRewards[p]);
                            }
                        }
                    }
                    questsCompleted.Add(quest);
                    
                    PlayerManager.instance.AddExp(quests[i].currentQuest.expReward);
                    quests.Remove(quests[i]);
                }
            }
        }

        QuestJournel.instance.UpdateQuestJournel(quests);
    }
}
