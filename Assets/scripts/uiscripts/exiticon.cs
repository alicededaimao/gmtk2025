using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExitToStartScene : MonoBehaviour
{
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnExitClicked);
    }

    void OnExitClicked()
    {
        SceneManager.LoadScene("start");
    }
}
