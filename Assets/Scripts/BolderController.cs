using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BolderController : MonoBehaviour
{

    private Rigidbody2D rigidBody;
    private Vector3 startPoint;
    private Vector3 endPoint;

    [SerializeField]
    private float finalY;

    public float Speed { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        startPoint = transform.position;
        endPoint = new Vector3(transform.position.x, finalY, 0);
        Speed = 20f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!DimensionManager.instance.IsDark())
            MoveBolder(startPoint, endPoint);
        else
            MoveBolder(endPoint, startPoint);
    }

    void MoveBolder(Vector3 start, Vector3 end)
    {
        float step = Speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, end, step);

        if (Vector3.Distance(transform.position, end) < Mathf.Epsilon)
            transform.position = start;
    }
}
