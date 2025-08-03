using UnityEngine;
using UnityEngine.UI;

public class HelpPanelController : MonoBehaviour
{
    private GameObject panel;
    private Button helpIconButton;
    private Button iGotItButton;

    void Start()
    {
        panel = transform.Find("Panel").gameObject;
        helpIconButton = transform.parent.Find("helpicon").GetComponent<Button>();
        iGotItButton = panel.transform.Find("igotit").GetComponent<Button>();

        panel.SetActive(false);

        helpIconButton.onClick.AddListener(ShowPanel);
        iGotItButton.onClick.AddListener(HidePanel);
    }

    void ShowPanel()
    {
        panel.SetActive(true);
    }

    void HidePanel()
    {
        panel.SetActive(false);
    }
}
