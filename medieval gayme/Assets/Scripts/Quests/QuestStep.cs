using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "Quests/Quest Step")]
public class QuestStep : ScriptableObject
{
    [BoxGroup("Quest Step")]
    public EQuestStepType questStepType;
    [BoxGroup("Quest Step")]
    [TextArea] public string stepDescription;

    [BoxGroup("Quest Step/Kill")][ShowIf("questStepType", EQuestStepType.kill)]
    public NPCObject whatToKill;
    [BoxGroup("Quest Step/Kill")][ShowIf("questStepType", EQuestStepType.kill)]
    public int howManyToKill;
    
    [BoxGroup("Quest Step/Get")][ShowIf("questStepType", EQuestStepType.get)]
    public ItemObject whatToGet;
    [BoxGroup("Quest Step/Get")][ShowIf("questStepType", EQuestStepType.get)]
    public int howManyToGet;

    [BoxGroup("Quest Step/Talk")][ShowIf("questStepType", EQuestStepType.talk)]
    public string whoToTalkTo;
    [BoxGroup("Quest Step/Talk")][ShowIf("questStepType", EQuestStepType.talk)]
    public DialogLineObject dialogLine;

    [BoxGroup("Quest Step")]
    public int step;
}
