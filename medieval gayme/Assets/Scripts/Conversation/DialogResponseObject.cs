using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "NPC/Dialog/Dialog Response")]
public class DialogResponseObject : ScriptableObject
{   
    [BoxGroup("Response Info")]
    [TextArea]
    public string response;
    [BoxGroup("Response Info")]
    public DialogLineObject dialogLine;
    [BoxGroup("Response Info")]
    public QuestStep showOnStep;
    [BoxGroup("Response Info")]
    public QuestStep hideAfterStep;
    [BoxGroup("Response Info")]
    public QuestObject hideAfterQuest;
}
