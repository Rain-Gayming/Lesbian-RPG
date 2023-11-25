using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using TMPro;

public class DialogBoxUI : MonoBehaviour
{
    public static DialogBoxUI instance;

    [BoxGroup("References")]
    public DialogObject currentDialog;
    [BoxGroup("References")]
    public DialogLineObject currentLine;
    [BoxGroup("References")]
    public QuestManager questManager;

    [BoxGroup("UI")]
    public Menu dialogMenu;
    [BoxGroup("UI")]
    public TMP_Text speakerText;
    [BoxGroup("UI")]
    public TMP_Text dialogText;
    [BoxGroup("UI/Responses") ]
    public Transform responseRegion;
    [BoxGroup("UI/Responses")]
    public GameObject responsePrefab;
    [BoxGroup("UI/Responses")]
    public List<GameObject> responseButtons;
    
    public void Awake() 
    {
        instance = this;
    }

    public void StartDialog(bool talkedTo)
    {
        MenuManager.instance.ChangeMenuWithPause(dialogMenu);
        if(!talkedTo){
            currentLine = currentDialog.firstLine;
        }else{
            currentLine = currentDialog.returningLine;
        }
        
        UpdateLine(currentLine);
        UpdateResponseUI();
    }
    
    public void UpdateLine(DialogLineObject line)
    {
        for (int i = 0; i < responseButtons.Count; i++)
        {
            Destroy(responseButtons[i]);
        }

        responseButtons.Clear();

        currentLine = line;
        speakerText.text = currentDialog.npc.npcName;
        dialogText.text = currentLine.line;

        dialogText.text.Replace("Y/N", PlayerManager.instance.playerName);


        if(questManager.quests.Count > 0){
            for (int i = 0; i < questManager.quests.Count; i++)
            {
                int currentStep = questManager.quests[i].currentStep;
                QuestObject quest = questManager.quests[i].currentQuest;
                if(quest.questSteps[currentStep].questStepType == EQuestStepType.talk){
                    if(quest.questSteps[currentStep].dialogLine == currentLine){
                        questManager.UpdateQuest(quest);
                    }
                }
            }
        }

        if(currentLine.questToGive != null){
            questManager.AddQuest(currentLine.questToGive);
        }

        if(currentLine.itemReward.item != null){
            if(Inventory.instance.CanAddItem(currentLine.itemReward)){
                Inventory.instance.AddItem(currentLine.itemReward);
            }else{
                print("Cant Add Item");
            }
        }

        UpdateResponseUI();
    }

    public void UpdateResponseUI()
    {
        for (int i = 0; i < responseButtons.Count; i++)
        {
            Destroy(responseButtons[i]);
        }
        responseButtons.Clear();
        
        for (int i = 0; i < currentLine.responses.Count; i++)
        {
            if(currentLine.responses[i].hideAfterQuest){
                for (int q = 0; q < questManager.questsCompleted.Count; q++)
                {
                    if(currentLine.responses[i].hideAfterQuest == questManager.questsCompleted[i]){

                    }else{
                        if(i >= questManager.questsCompleted.Count){
                            AddNewResponse(i);
                        }
                    }
                }
            }
            if(currentLine.responses[i].hideOnQuest){
                for (int q = 0; q < questManager.quests.Count; q++)
                {
                    if(questManager.quests[q].currentQuest == currentLine.responses[i].hideOnQuest){

                    }else{
                        if(i >= questManager.quests.Count){
                            AddNewResponse(i);
                        }
                    }
                }
            }
            if(!currentLine.responses[i].showOnStep){
                AddNewResponse(i);
            }else{
                for (int q = 0; q < questManager.quests.Count; q++)
                {
                    int currentStep = questManager.quests[q].currentStep; 
                    if(currentLine == questManager.quests[q].currentQuest.questSteps[currentStep].dialogLine){
                        AddNewResponse(i);
                    }
                }
            }
        }
    }

    public void AddNewResponse(int i)
    {
        GameObject newResponse = Instantiate(responsePrefab);
        newResponse.transform.SetParent(responseRegion);
        newResponse.GetComponent<ResponseUI>().response = currentLine.responses[i];

        string response = currentLine.responses[i].response;
        response.Replace("Y/N", PlayerManager.instance.playerName);

        newResponse.GetComponentInChildren<TMP_Text>().text = response;
        newResponse.transform.localScale = Vector3.one;
        responseButtons.Add(newResponse);
    }
}
