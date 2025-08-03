using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MapSwitcherSimple : MonoBehaviour
{
    private GameObject bigmap;
    private GameObject smallmap;
    private Button smallmapButton;
    private Button closeiconButton;
    private CanvasGroup bigmapCanvasGroup;

    public float fadeDuration = 0.3f;

    void Start()
    {
        bigmap = transform.Find("bigmap").gameObject;
        smallmap = transform.Find("smallmap").gameObject;

        smallmapButton = smallmap.GetComponent<Button>();
        closeiconButton = bigmap.transform.Find("closeicon").GetComponent<Button>();

        if (!bigmap.TryGetComponent(out bigmapCanvasGroup))
            bigmapCanvasGroup = bigmap.AddComponent<CanvasGroup>();


        smallmap.SetActive(true);
        InitCanvasGroup(bigmapCanvasGroup);
        bigmap.SetActive(false);

        smallmapButton.onClick.AddListener(OnSmallMapClick);
        closeiconButton.onClick.AddListener(OnCloseIconClick);
    }

    void InitCanvasGroup(CanvasGroup cg)
    {
        cg.alpha = 0f;
        cg.interactable = false;
        cg.blocksRaycasts = false;
    }

    void OnSmallMapClick()
    {
        smallmap.SetActive(false);
        bigmap.SetActive(true);
        StartCoroutine(FadeCanvasGroup(bigmapCanvasGroup, 0f, 1f));
    }

    void OnCloseIconClick()
    {
        StartCoroutine(FadeCanvasGroup(bigmapCanvasGroup, 1f, 0f, () =>
        {
            bigmap.SetActive(false);
            smallmap.SetActive(true);
        }));
    }

    IEnumerator FadeCanvasGroup(CanvasGroup cg, float from, float to, System.Action onComplete = null)
    {
        float t = 0f;
        cg.interactable = true;
        cg.blocksRaycasts = true;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            cg.alpha = Mathf.Lerp(from, to, t / fadeDuration);
            yield return null;
        }

        cg.alpha = to;
        cg.interactable = to > 0.9f;
        cg.blocksRaycasts = to > 0.9f;

        onComplete?.Invoke();
    }
}
