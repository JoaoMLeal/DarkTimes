using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BolderController : MonoBehaviour
{

    private Rigidbody2D rigidBody;
    private Vector3 startPoint;
    private Vector3 finalPoint;

    [SerializeField]
    private float finalY;

    private float speed = 20f; 

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        startPoint = transform.position;
        finalPoint = new Vector3(transform.position.x, finalY, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (!DimensionManager.instance.IsDark())
            MoveBolder(startPoint, finalPoint);
        else
            MoveBolder(finalPoint, startPoint);
    }

    void MoveBolder(Vector3 start, Vector3 final)
    {
        float step = speed * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, final, step);

        // Check if the position of the cube and sphere are approximately equal.
        if (Vector3.Distance(transform.position, final) < 0.001f)
        {
            // Swap the position of the cylinder.
            transform.position = start;
        }
    }

    public void SetSpeed(float s)
    {
        speed = s;
    }
}
