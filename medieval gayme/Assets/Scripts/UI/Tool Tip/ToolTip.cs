using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine.UI;

public class ToolTip : MonoBehaviour
{
    [BoxGroup("References")]
    public RectTransform rectTransform;
    
    [BoxGroup("UI")]
    public TMP_Text headerText;
    [BoxGroup("UI")]
    public TMP_Text contentText;
    [BoxGroup("UI")]
    public LayoutElement layoutElement;
    
    [BoxGroup("Text")]
    public int characterWrapLimit;

    public virtual void SetText(string content, string header = "")
    {
        if(string.IsNullOrEmpty(header)){
            headerText.gameObject.SetActive(false);
        }else{
            headerText.gameObject.SetActive(true);
            headerText.text = header;
        }
        if(contentText)
            contentText.text = content;
        UpdateUI();
    }

    void UpdateUI()
    {
        int headerLength = headerText.text.Length;
        int contentLength = 0;
        
        if(contentText)
            contentLength = contentText.text.Length;

        layoutElement.enabled = (headerLength > characterWrapLimit || contentLength > characterWrapLimit) ? true : false;

    }

    void Update() {
        Vector2 pos = Input.mousePosition;

        float pivotX = pos.x / Screen.width;
        float pivotY = pos.y / Screen.height;

        //rectTransform.pivot = new Vector2(pivotX, pivotY);
        transform.position = pos;
    }
}
