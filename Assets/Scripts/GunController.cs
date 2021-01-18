﻿using System.Collections;
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


    [SerializeField]
    public GameObject darkSelf;
    private GameObject darkS;
    private Vector3 initialPos;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        darkS = null;
    }

    void Update()
    {

        if (DimensionManager.instance.IsDark())
        {
            DrawTrajectory();
        }

        if (Input.GetMouseButtonDown(0))
        {
            initialPos = transform.position;
            transform.position = lineRenderer.GetPosition(lineRenderer.positionCount - 1);

            /*float temp = lightWall.position.z;
            lightWall.position = new Vector3(lightWall.position.x, lightWall.position.y, darkWall.position.z);
            darkWall.position = new Vector3(darkWall.position.x, darkWall.position.y, temp);
            isDark = true;*/

            Vector3[] points = new Vector3[lineRenderer.positionCount];
            lineRenderer.GetPositions(points);
            CreateDarkSelf(points);
        }

        if (darkS != null && Vector3.Distance(darkS.transform.position, initialPos) < 0.1f)
        {
            /*float temp = lightWall.position.z;
            lightWall.position = new Vector3(lightWall.position.x, lightWall.position.y, darkWall.position.z);
            darkWall.position = new Vector3(darkWall.position.x, darkWall.position.y, temp);
            isDark = false;*/
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

    /// <summary>
    /// Instantiates the dark self at the player position
    /// </summary>
    void CreateDarkSelf(Vector3[] points)
    {
        darkS = Instantiate(darkSelf, transform.position, Quaternion.identity);
        darkS.GetComponent<DarkSelfController>().points = points;
    }


}
