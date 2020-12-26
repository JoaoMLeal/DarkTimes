using System.Collections;
using System.Collections.Generic;
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
    public GameObject darkPlayer;
    private bool isDark;
    private Vector3 initialPosition;
    private float speed;
    private int positions;


    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        darkPlayer.SetActive(false);
        initialPosition = transform.position;
        isDark = false;
        speed = 3;
    }

    void Update()
    {   
        
        if (!isDark) 
        {
            Vector3 point = new Vector3();

            point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));

            ray = new Ray2D(transform.position, point - transform.position);
            lineRenderer.positionCount = 1;
            lineRenderer.SetPosition(0, transform.position);
            float remainingLength = maxLength;
                        
            Ray2D[] rays = new Ray2D[reflections+1];
            rays[0] = ray;

            for (int i = 0; i < reflections; i++)
            {
                hit = Physics2D.Raycast(ray.origin, ray.direction, remainingLength);
                if(hit.collider != null)
                {   
                    lineRenderer.positionCount += 1;
                    lineRenderer.SetPosition(lineRenderer.positionCount - 1, hit.point);
                    remainingLength -= Vector2.Distance(ray.origin, hit.point);
                    Vector2 newDirection = Vector2.Reflect(ray.direction, hit.normal);
                    ray = new Ray2D(hit.point, newDirection);
                    rays[i+1] = ray;
                    Debug.Log(hit.normal);
                }
                else
                {
                    lineRenderer.positionCount += 1;
                    lineRenderer.SetPosition(lineRenderer.positionCount - 1, ray.origin + ray.direction * remainingLength);
                    break;
                }

            }

            Vector3[] positions = new Vector3[lineRenderer.positionCount];
            lineRenderer.GetPositions(positions);
            //Debug.Log("pos " + string.Join(", ", positions));
            //Debug.Log("rays " + string.Join(" ||| ", rays));
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

                darkPlayer.SetActive(false);
            }
            else
            {
                float temp = lightWall.position.z;
                lightWall.position = new Vector3(lightWall.position.x, lightWall.position.y, darkWall.position.z);
                darkWall.position = new Vector3(darkWall.position.x, darkWall.position.y, temp);
                isDark = true;

                darkPlayer.SetActive(true);
                darkPlayer.transform.position = lineRenderer.GetPosition(lineRenderer.positionCount - 1);
                positions = lineRenderer.positionCount - 1;

                
            }
        }
                    // READ THIS: EPIPHANY
            /*
                use last getposition to teleport
                use get positions of drawline to make directions and path of clone

                when organizing code, make isDark public on some controller and use that
            */

        if (isDark) 
        {   
            Rigidbody2D darkRigidBody = darkPlayer.GetComponent<Rigidbody2D>();
            darkRigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;

            //Vector3 pos1 = lineRenderer.GetPosition(positions);
            Vector3 pos1 = darkPlayer.transform.position;
            Vector3 pos2 = lineRenderer.GetPosition(positions - 1);
            if (pos1 == pos2) 
            {
                positions--;
                pos2 = lineRenderer.GetPosition(positions - 1);
            }
            Vector2 direction = new Vector2(pos2.x - pos1.x, pos2.y - pos1.y);
            Vector2 move = new Vector2(direction.x * speed, direction.y * speed);

            darkRigidBody.velocity = move;

            if (initialPosition == darkPlayer.transform.position) 
            {
                darkRigidBody.velocity = new Vector2(0,0);
            }

            if (positions == 0)
            {   
                darkPlayer.SetActive(false);
                isDark = false;
            }
        }


    }
}
