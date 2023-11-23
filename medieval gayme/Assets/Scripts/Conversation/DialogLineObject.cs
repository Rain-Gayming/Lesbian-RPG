using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "NPC/Dialog/Dialog Line")]
public class DialogLineObject : ScriptableObject
{
    [BoxGroup("Dialog Line Info")]
    [TextArea]
    public string line;
    [BoxGroup("Dialog Line Info")]
    public List<DialogResponseObject> responses;

        
    [BoxGroup("Dialog Line Info/Gives")]
    public QuestObject questToGive;
    [BoxGroup("Dialog Line Info/Gives")]
    public InventoryItem itemReward;
}
