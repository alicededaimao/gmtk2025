using UnityEngine;

public class CameraDragPanXZ : MonoBehaviour
{
    public float dragSpeed = 20f;

    public Vector2 minXZ = new Vector2(-100f, -100f);
    public Vector2 maxXZ = new Vector2(100f, 100f);

    private Vector3 lastMousePosition;
    private Camera cam;
    private float fixedY; 
    void Start()
    {
        cam = Camera.main;
        fixedY = transform.position.y;
    }

    void Update()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        HandleMouseDrag();
#elif UNITY_ANDROID || UNITY_IOS
        HandleTouchDrag();
#endif
    }

    void HandleMouseDrag()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lastMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 delta = Input.mousePosition - lastMousePosition;
            Vector3 move = new Vector3(-delta.x, 0f, -delta.y) * dragSpeed;

            Vector3 newPos = transform.position + move;
            newPos = ClampXZ(newPos);
            newPos.y = fixedY; // 强制保持 Y 不变

            transform.position = newPos;

            lastMousePosition = Input.mousePosition;
        }
    }

    void HandleTouchDrag()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                Vector2 delta = touch.deltaPosition;
                Vector3 move = new Vector3(-delta.x, 0f, -delta.y) * dragSpeed;

                Vector3 newPos = transform.position + move;
                newPos = ClampXZ(newPos);
                newPos.y = fixedY;

                transform.position = newPos;
            }
        }
    }

    Vector3 ClampXZ(Vector3 pos)
    {
        pos.x = Mathf.Clamp(pos.x, minXZ.x, maxXZ.x);
        pos.z = Mathf.Clamp(pos.z, minXZ.y, maxXZ.y);
        return pos;
    }
}
