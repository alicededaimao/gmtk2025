using UnityEngine;
using System.Collections.Generic;

public class Boat : MonoBehaviour
{
    [Header("气泡图标容器")]
    public Transform bubbleBackground;

    private HashSet<ItemType> collectedItems = new HashSet<ItemType>();
    private ItemType lastCollectedItem = ItemType.None;
    private Dictionary<ItemType, GameObject> itemIcons = new Dictionary<ItemType, GameObject>();

    void Start()
    {
        foreach (Transform child in bubbleBackground)
        {
            string name = child.name.ToLower();
            if (System.Enum.TryParse<ItemType>(UppercaseFirst(name), out var type))
            {
                itemIcons[type] = child.gameObject;
                child.gameObject.SetActive(false);
            }
        }

        bubbleBackground.gameObject.SetActive(false);
    }

    private string UppercaseFirst(string s)
    {
        if (string.IsNullOrEmpty(s)) return s;
        return char.ToUpper(s[0]) + s.Substring(1);
    }

    public void LoadItem(ItemType item)
    {
        if (item == ItemType.None) return;

        lastCollectedItem = item;

        if (!collectedItems.Contains(item))
        {
            collectedItems.Add(item);

            if (itemIcons.ContainsKey(item))
                itemIcons[item].SetActive(true);

            bubbleBackground.gameObject.SetActive(true);
        }
    }

    public bool HasItem(ItemType item)
    {
        return collectedItems.Contains(item);
    }

    public ItemType GetCarriedItem()
    {
        return lastCollectedItem;
    }

    public void UnloadItem()
    {
        lastCollectedItem = ItemType.None;
    }
}
