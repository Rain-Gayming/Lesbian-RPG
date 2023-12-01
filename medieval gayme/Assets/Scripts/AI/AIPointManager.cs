using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class AIPointManager : MonoBehaviour
{
    public static AIPointManager instance;
    [BoxGroup("AI")]
    public List<GameObject> points;

    void Awake() {
        instance = this;
        AIPoint[] a = GetComponentsInChildren<AIPoint>();

        for (int i = 0; i < a.Length; i++)
        {
            points.Add(a[i].gameObject);
        }
    }
}
