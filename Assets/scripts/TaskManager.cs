using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class TaskManager : MonoBehaviour
{
    public static TaskManager Instance;

    // 任务列表，表示船需要访问的岛屿顺序
    public List<GameObject> missionIslands;

    // UI 组件
    public Image backgroundImage; // 左上角的背景图
    public List<Text> missionTexts; // 两个任务的 Text 组件

    // 任务状态
    private bool mission1Completed = false;
    private bool mission2Completed = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // 确保任务列表至少有两个岛屿
        if (missionIslands.Count < 2)
        {
            Debug.LogError("任务列表需要至少两个岛屿！");
            return;
        }

        InitializeUI();
    }

    // 初始化任务 UI
    private void InitializeUI()
    {
        // 获取岛屿A和岛屿B的Island脚本
        Island islandA = missionIslands[0].GetComponent<Island>();
        Island islandB = missionIslands[1].GetComponent<Island>();

        // 设定任务需求并更新UI
        missionTexts[0].text = missionIslands[0].name + "需要 " + islandB.producedItem.ToString();
        missionTexts[1].text = missionIslands[1].name + "需要 " + islandA.producedItem.ToString();

        // 默认颜色
        missionTexts[0].color = Color.black;
        missionTexts[1].color = Color.black;
    }

    // 处理船与岛屿的碰撞
    public void HandleBoatCollision(Boat boat, Island island)
    {
        Debug.Log("test");
        if (island == missionIslands[0].GetComponent<Island>())
        {
            // 船接触到第一个岛屿 (gameObjectA)
            Debug.Log("船接触到 " + missionIslands[0].name);

            if (boat.GetCarriedItem() == ItemType.None)
            {
                // 如果船是空的，就装载第一个岛屿的物品
                boat.LoadItem(island.producedItem);
            }
            else if (boat.GetCarriedItem() == missionIslands[1].GetComponent<Island>().producedItem && !mission1Completed)
            {
                // 如果船有来自第二个岛屿的物品，且任务未完成，则送货
                boat.UnloadItem();
                mission1Completed = true;
                missionTexts[0].color = Color.gray;
                Debug.Log("任务1完成，" + missionIslands[0].name + "已收到货物。");
            }
        }
        else if (island == missionIslands[1].GetComponent<Island>())
        {
            // 船接触到第二个岛屿 (gameObjectB)
            Debug.Log("船接触到 " + missionIslands[1].name);

            if (boat.GetCarriedItem() == ItemType.None)
            {
                // 如果船是空的，就装载第二个岛屿的物品
                boat.LoadItem(island.producedItem);
            }
            else if (boat.GetCarriedItem() == missionIslands[0].GetComponent<Island>().producedItem && !mission2Completed)
            {
                // 如果船有来自第一个岛屿的物品，且任务未完成，则送货
                boat.UnloadItem();
                mission2Completed = true;
                missionTexts[1].color = Color.gray;
                Debug.Log("任务2完成，" + missionIslands[1].name + "已收到货物。");
            }
        }
    }
}