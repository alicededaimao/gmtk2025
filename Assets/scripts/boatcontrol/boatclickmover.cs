using UnityEngine;
using System.Collections.Generic;

public class Boatclicdcontroller : MonoBehaviour
{
    public float moveSpeed = 5f;
    public GameObject clickEffectPrefab;
    public LayerMask groundMask;
    public LayerMask obstacleMask;
    public float avoidOffset = 3f;

    private List<Vector3> curvePoints = new List<Vector3>();
    private float t = 0f;
    private bool moving = false;
    private float curveLength = 0f;

    void Update()
    {
        HandleClick();

        if (moving && curvePoints.Count >= 3)
        {
            t += Time.deltaTime * (moveSpeed / curveLength);
            t = Mathf.Clamp01(t);
            Vector3 pos = GetQuadraticBezierPoint(t, curvePoints[0], curvePoints[1], curvePoints[2]);
            transform.position = new Vector3(pos.x, transform.position.y, pos.z);

            Vector3 dir = curvePoints[2] - transform.position;
            if (dir.sqrMagnitude > 0.01f)
            {
                Quaternion rot = Quaternion.LookRotation(Vector3.Cross(Vector3.up, dir), Vector3.up);
                float y = rot.eulerAngles.y;
                transform.rotation = Quaternion.Euler(0f, y, 0f);
            }

            if (t >= 1f)
                moving = false;
        }
    }

    void HandleClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (UnityEngine.EventSystems.EventSystem.current != null &&
                UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
                return;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 100f, groundMask))
            {
                SpawnClickEffect(hit.point);
                PrepareAvoidPath(hit.point);
            }
        }
    }

    void PrepareAvoidPath(Vector3 target)
    {
        Vector3 start = transform.position;
        Vector3 mid;

        Vector3 dir = (target - start).normalized;
        float distance = Vector3.Distance(start, target);
        Ray ray = new Ray(start, dir);

        if (Physics.SphereCast(ray, 0.5f, out RaycastHit hitInfo, distance, obstacleMask))
        {
            Vector3 right = Vector3.Cross(Vector3.up, dir);
            mid = (start + target) / 2 + right.normalized * avoidOffset;
        }
        else
        {
            mid = (start + target) / 2 + Vector3.up * 0.5f;
        }

        curvePoints.Clear();
        curvePoints.Add(start);
        curvePoints.Add(mid);
        curvePoints.Add(target);
        t = 0f;
        moving = true;

        curveLength = EstimateBezierLength(start, mid, target, 10);
    }

    Vector3 GetQuadraticBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1 - t;
        return u * u * p0 + 2 * u * t * p1 + t * t * p2;
    }

    float EstimateBezierLength(Vector3 p0, Vector3 p1, Vector3 p2, int segments)
    {
        float length = 0f;
        Vector3 prev = p0;
        for (int i = 1; i <= segments; i++)
        {
            float t = i / (float)segments;
            Vector3 point = GetQuadraticBezierPoint(t, p0, p1, p2);
            length += Vector3.Distance(prev, point);
            prev = point;
        }
        return length;
    }

    void SpawnClickEffect(Vector3 position)
    {
        if (clickEffectPrefab != null)
        {
            Vector3 spawnPos = position;
            spawnPos.y += 0.1f;
            GameObject obj = Instantiate(clickEffectPrefab, spawnPos, Quaternion.identity);
            Destroy(obj, 2f);
        }
    }
}
