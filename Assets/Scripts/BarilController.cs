using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarilController : MonoBehaviour
{

    private Rigidbody2D rigidBody;
    private bool isDark = false;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            if (isDark) 
            {
                transform.position = new Vector3(transform.position.x, 2, transform.position.z);
                rigidBody.gravityScale = -0.1f;
                rigidBody.velocity = new Vector2(0,0);
                isDark = false;
            }
            else
            {
                transform.position = new Vector3(transform.position.x, -2, transform.position.z);
                rigidBody.gravityScale = -0.3f;
                rigidBody.velocity = new Vector2(0,0);
                isDark = true;
            }
        }
    }
}
