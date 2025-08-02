using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    public float moveSpeed = 5f;  // 移动速度

    void Update()
    {

        float h = Input.GetAxisRaw("Horizontal"); // A/D 或 左/右键（-1 ~ 1）
        float v = Input.GetAxisRaw("Vertical");   // W/S 或 上/下键（-1 ~ 1）
        Vector3 moveDir = new Vector3(h, 0f, v).normalized;
        transform.Translate(moveDir * moveSpeed * Time.deltaTime, Space.World);
    }
}
