using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public AudioClip JumpSound;
    public AudioClip WalkSound;

    public AudioSource AudioSource;


    [SerializeField]
    private float speed;
    [SerializeField]
    private float jump;
    
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

        facingLeft = true;
    }

    // Update is called once per frame
    void Update()
    {
        movement = Input.GetAxis("Horizontal");

        if (movement != 0 && !AudioSource.isPlaying)
        {
            AudioSource.clip = WalkSound;
            AudioSource.Play();
        }

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            AudioSource.clip = JumpSound;
            AudioSource.Play();

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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bolder") || other.CompareTag("Lava"))
            GameManager.instance.GameOver();
    }
}
