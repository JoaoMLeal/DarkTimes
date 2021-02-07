using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    public float speed;
    public float jump;
    
    private float movement;
    private bool facingLeft;

    private Rigidbody2D rigidBody;
    private BoxCollider2D boxCollider;
    
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
        boxCollider = GetComponent<BoxCollider2D>();

        speed = 15f;
        jump = 25f;
        facingLeft = true;
    }

    // Update is called once per frame
    void Update()
    {
        movement = Input.GetAxis("Horizontal");
        
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            //velocityY = jump;
            Vector2 j = new Vector2(rigidBody.velocity.x, jump);
            rigidBody.velocity = j;
        }

        Vector2 move = new Vector2(movement * speed, rigidBody.velocity.y);
        rigidBody.velocity = move;


        if (facingLeft && movement > 0)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            facingLeft = false;
        }

        if (!facingLeft && movement < 0)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            facingLeft = true;
        }
    }

    private bool IsGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, .1f);
        return raycastHit.collider != null;
    }
}
