using UnityEngine;

public class FollowTargetUI : MonoBehaviour
{
    public Transform target;           
    public Vector3 offset = new Vector3(0, 2f, 0);  
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        if (target == null)
        {
            Debug.LogWarning("FollowTargetUI: 未指定目标！");
        }
    }

    void LateUpdate()
    {
        if (target == null || mainCamera == null) return;

        transform.position = target.position + offset;

        transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.forward,
                         mainCamera.transform.rotation * Vector3.up);
    }
}
