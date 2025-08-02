using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class TaskManager : MonoBehaviour
{
    public static TaskManager Instance;

    [System.Serializable]
    public class TaskButton
    {
        public IslandStatusManager island; 
        public Button button;           
    }

    [Header("任务配置")]
    public List<TaskButton> taskButtons;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void MarkTaskComplete(IslandStatusManager island)
    {
        foreach (var task in taskButtons)
        {
            if (task.island == null || task.button == null)
                continue;

            if (task.island == island)
            {
                task.button.interactable = false;
                return;
            }
        }

    }
}
