using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class GunController : MonoBehaviour
{

    public int reflections = 5;
    public float maxLength = 50f;

    private LineRenderer lineRenderer;
    private Ray2D ray;
    private RaycastHit2D hit;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {

        if (!DimensionManager.instance.IsDark())
        {
            DrawTrajectory();
        }
    }

    /// <summary>
    /// Draws a line from the player position to the cursor position, and reflects that line on objects
    /// </summary>
    void DrawTrajectory()
    {
        Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));

        ray = new Ray2D(transform.position, point - transform.position);
        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, transform.position);
        float remainingLength = maxLength;

        for (int i = 0; i < reflections; i++)
        {
            hit = Physics2D.Raycast(ray.origin, ray.direction, remainingLength, ~(1 << 2));
            if (hit)
            {
                if (hit.collider.CompareTag("Mirror"))
                    remainingLength += 10f;
                lineRenderer.positionCount += 1;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, hit.point);
                remainingLength -= Vector2.Distance(ray.origin, hit.point);
                Vector2 newDirection = Vector2.Reflect(ray.direction, hit.normal);
                ray = new Ray2D(hit.point, newDirection);
            }
            else
            {
                lineRenderer.positionCount += 1;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, ray.origin + ray.direction * remainingLength);
                break;
            }
        }
    }

}
