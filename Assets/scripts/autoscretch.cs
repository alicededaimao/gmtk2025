using UnityEngine;
using System.Collections.Generic;
using Unity.Collections;

[RequireComponent(typeof(RectTransform))]
public class BubbleWidthController : MonoBehaviour
{
    [Header("宽度设置")]
    public float defaultWidth = 1140f;      // 初始宽度（1个子物体时）
    public float perUnitWidth = 400f;       // 每多1个子物体时增加的宽度

    public float fixlenght = 850f;


    private RectTransform rectTransform;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        List<RectTransform> activeChildren = new List<RectTransform>();
        foreach (Transform child in transform)
        {
            if (child.gameObject.activeSelf)
                activeChildren.Add(child as RectTransform);
        }
           
        int count = activeChildren.Count;
        float targetWidth = defaultWidth + perUnitWidth * Mathf.Max(0, count - 1);
        rectTransform.sizeDelta = new Vector2(targetWidth, rectTransform.sizeDelta.y);
        float startX = fixlenght-targetWidth/2;  
    
        for (int i = 0; i < count; i++)
        {
            float x = startX + i * perUnitWidth;
            activeChildren[i].anchoredPosition = new Vector2(x, 55f);
        }
    }
}
