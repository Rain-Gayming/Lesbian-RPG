using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quests/Database")]
public class QuestDatabase : ScriptableObject
{
    public List<QuestObject> quests;
}
