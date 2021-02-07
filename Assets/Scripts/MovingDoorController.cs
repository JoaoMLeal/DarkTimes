using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingDoorController : MonoBehaviour
{

    [SerializeField]
    private float moveSpeed;

    private float speed;
    private Vector3 startPoint;
    private Vector3 endPoint;

    // Start is called before the first frame update
    void Start()
    {
        speed = moveSpeed;
        startPoint = transform.position;
        endPoint = new Vector3(startPoint.x, startPoint.y - 10, startPoint.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (!DimensionManager.instance.IsDark())
            MoveDoor(endPoint);
        else
            MoveDoor(startPoint);
    }

    void MoveDoor(Vector3 end)
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, end, step);
    }
}
