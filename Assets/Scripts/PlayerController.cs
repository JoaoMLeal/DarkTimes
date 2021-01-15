using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    public float speed = 5;
    
    private float movement;
    private bool facingLeft = true;

    private Rigidbody2D rigidBody;
    
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    // Update is called once per frame
    void Update()
    {
        movement = Input.GetAxis("Horizontal");

        Vector2 move = new Vector2(movement * speed, 0);
        rigidBody.AddForce(move);

        if (facingLeft && movement > 0)
        {
            transform.localScale = new Vector3(-transform.localScale.x, -transform.localScale.y, -transform.localScale.z);
            facingLeft = false;
        }

        if (!facingLeft && movement < 0)
        {
            transform.localScale = new Vector3(-transform.localScale.x, -transform.localScale.y, -transform.localScale.z);
            facingLeft = true;
        }
    }
}
