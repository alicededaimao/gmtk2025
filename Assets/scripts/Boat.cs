using UnityEngine;

public class Boat : MonoBehaviour
{
    private ItemType carriedItem = ItemType.None; // 船上携带的物品
    
    // 碰撞检测
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("test1");
        Island island = collision.gameObject.GetComponent<Island>();
        if (island == null) return;

        // 通知 TaskManager 船与岛屿发生了碰撞
        TaskManager.Instance.HandleBoatCollision(this, island);
    }

    // 装货方法
    public void LoadItem(ItemType item)
    {
        carriedItem = item;
        Debug.Log("船已装载物品: " + carriedItem);
    }

    // 卸货方法
    public ItemType UnloadItem()
    {
        ItemType unloadedItem = carriedItem;
        carriedItem = ItemType.None; // 卸货后清空
        Debug.Log("船已卸载物品: " + unloadedItem);
        return unloadedItem;
    }

    // 获取船上物品
    public ItemType GetCarriedItem()
    {
        return carriedItem;
    }
}