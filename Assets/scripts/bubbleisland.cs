using UnityEngine;

public class BubbleEffect : MonoBehaviour
{
    [Header("浮动设置")]
    public float floatAmplitude = 0.2f;      
    public float floatFrequency = 1f;        

    [Header("缩放设置")]
    public float scaleAmplitude = 0.05f;      
    public float scaleFrequency = 1.5f;       

    private Vector3 initialPosition;
    private Vector3 initialScale;

    void Start()
    {
        initialPosition = transform.localPosition;
        initialScale = transform.localScale;
    }

    void Update()
    {
        
        float floatOffset = Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;
        transform.localPosition = initialPosition + new Vector3(0f, floatOffset, 0f);

        
        float scaleOffset = Mathf.Sin(Time.time * scaleFrequency) * scaleAmplitude;
        transform.localScale = initialScale * (1f + scaleOffset);
    }
}
