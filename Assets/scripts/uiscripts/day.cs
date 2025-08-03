using UnityEngine;

public class ImageZoomAndMoveOnce : MonoBehaviour
{
    public Vector3 targetPosition = new Vector3(0, 548, 0);
    public float targetScale = 0.2f;                       
    public float duration = 1.0f;                          
    private Vector3 startPosition;
    private Vector3 startScale;
    private Vector3 endScale;

    private float timer = 0f;
    private bool animating = true;

    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        rectTransform.anchoredPosition = Vector3.zero;
        rectTransform.localScale = Vector3.one;

        startPosition = rectTransform.anchoredPosition;
        startScale = rectTransform.localScale;
        endScale = Vector3.one * targetScale;
    }

    void Update()
    {
        if (!animating) return;

        timer += Time.deltaTime;
        float t = Mathf.Clamp01(timer / duration);

        rectTransform.localScale = Vector3.Lerp(startScale, endScale, t);
        rectTransform.anchoredPosition = Vector3.Lerp(startPosition, targetPosition, t);

        if (t >= 1f)
        {
            rectTransform.localScale = endScale;
            rectTransform.anchoredPosition = targetPosition;
            animating = false;
        }
    }
}
