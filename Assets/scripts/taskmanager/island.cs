using UnityEngine;
using System.Collections.Generic;

public class IslandStatusManager : MonoBehaviour
{
    [Header("资源配置")]
    public ItemType producedItem;                     
    public List<ItemType> requiredItems;              

    [Header("UI 气泡")]
    public GameObject bubbleRoot;                     
    private Dictionary<ItemType, GameObject> itemIcons = new Dictionary<ItemType, GameObject>();
    private HashSet<ItemType> fulfilledItems = new HashSet<ItemType>();

    void Start()
    {
        foreach (Transform child in bubbleRoot.transform)
        {
            string nameLower = child.name.ToLower();
            if (System.Enum.TryParse<ItemType>(UppercaseFirst(nameLower), out var type))
            {
                if (requiredItems.Contains(type))
                {
                    itemIcons[type] = child.gameObject;
                    child.gameObject.SetActive(true);
                }
                else
                {
                    child.gameObject.SetActive(false);
                }
            }
        }

        bubbleRoot.SetActive(true);
    }

    private string UppercaseFirst(string s)
    {
        if (string.IsNullOrEmpty(s)) return s;
        return char.ToUpper(s[0]) + s.Substring(1);
    }

    public void OnBoatEnter(Boat boat)
    {
        // 装货
        boat.LoadItem(producedItem);

        // 卸货否
        foreach (var need in requiredItems)
        {
            if (!fulfilledItems.Contains(need) && boat.HasItem(need))
            {
                fulfilledItems.Add(need);
                if (itemIcons.ContainsKey(need))
                    itemIcons[need].SetActive(false);

                Debug.Log($"{gameObject.name} 已获得资源：{need}");
            }
        }

        // 检查任务
        if (fulfilledItems.Count == requiredItems.Count)
        {
            bubbleRoot.SetActive(false);
            Debug.Log($"{gameObject.name} 所有资源已满足！");
            if (TaskManager.Instance != null)
            {
                TaskManager.Instance.MarkTaskComplete(this);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Boat boat = other.GetComponent<Boat>();
        if (boat != null)
        {
            OnBoatEnter(boat);
        }
    }
}
