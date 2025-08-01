using UnityEngine;

public class CameraFollowNoRotation : MonoBehaviour
{
    public Transform target; 
    public Vector3 offset = new(0, 10, -10);

    void LateUpdate()
    {
        if (target == null) return;
        transform.position = target.position + offset;
    }
}
