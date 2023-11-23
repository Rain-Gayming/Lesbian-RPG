using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using TMPro;

public class ResponseUI : MonoBehaviour
{
    [BoxGroup("References")]
    public DialogResponseObject response;

    [BoxGroup("UI")]
    public TMP_Text responseText;
    

    public void Pressed()
    {
        DialogBoxUI.instance.UpdateLine(response.dialogLine);   
    }
}
