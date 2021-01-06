﻿using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class GunController : MonoBehaviour
{

    [SerializeField]
    public Transform lightWall;
    [SerializeField]
    public Transform darkWall;

    public int reflections = 5;
    public float maxLength = 5f;

    private LineRenderer lineRenderer;
    private Ray2D ray;
    private RaycastHit2D hit;
    private Vector3 direction;


    [SerializeField]
    public GameObject darkSelf;
    private bool isDark;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        isDark = false;
    }

    void Update()
    {

        if (!isDark)
        {
            DrawTrajectory();
        }

        if (Input.GetMouseButtonDown(0))
        {
            transform.position = lineRenderer.GetPosition(lineRenderer.positionCount - 1);

            if (isDark)
            {
                float temp = lightWall.position.z;
                lightWall.position = new Vector3(lightWall.position.x, lightWall.position.y, darkWall.position.z);
                darkWall.position = new Vector3(darkWall.position.x, darkWall.position.y, temp);
                isDark = false;

            }
            else
            {
                float temp = lightWall.position.z;
                lightWall.position = new Vector3(lightWall.position.x, lightWall.position.y, darkWall.position.z);
                darkWall.position = new Vector3(darkWall.position.x, darkWall.position.y, temp);
                isDark = true;

                Vector3[] points = new Vector3[lineRenderer.positionCount];
                lineRenderer.GetPositions(points);
                CreateDarkSelf(points);
            }
        }
    }

    /// <summary>
    /// Draws a line from the player position to the cursor position, and reflects that line on objects
    /// </summary>
    void DrawTrajectory()
    {
        Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));

        ray = new Ray2D(transform.position, point);
        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, transform.position);
        float remainingLength = maxLength;

        for (int i = 0; i < reflections; i++)
        {
            hit = Physics2D.Raycast(ray.origin, ray.direction, remainingLength);
            if (hit)
            {
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
        GameObject dakrS = Instantiate(darkSelf, transform.position, Quaternion.identity);
        dakrS.GetComponent<DarkSelfController>().points = points;
    }


}
