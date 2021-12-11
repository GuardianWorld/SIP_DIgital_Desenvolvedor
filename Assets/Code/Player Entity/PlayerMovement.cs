using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Values
    public float jump;
    public float fallSpeed = 2;
    public float maxSpeed = 3;
    public float speedIncrements;
    float speed;
    int direction;

    //Vectors
    Vector2 velocity;
    Vector2 vel;

    //Components
    Rigidbody2D rb;
    SpriteRenderer sr;
    BoxCollider2D bc;
    Animator an;
    
    //Bools
    public bool isGrounded = false;


    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        sr = gameObject.GetComponent<SpriteRenderer>();
        bc = gameObject.GetComponent<BoxCollider2D>();
        an = gameObject.GetComponent<Animator>();
        velocity = new Vector2(1f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Horizontal") > 0)
        {
            sr.flipX = false;
            direction = 1;
        }
        if (Input.GetAxis("Horizontal") < 0) //Negative
        {
            sr.flipX = true;
            direction = -1;
        }
        if (Input.GetAxis("Horizontal") == 0)
        {
            speed = Mathf.Clamp(speed - speedIncrements * 100 * Time.deltaTime, 0, maxSpeed);
        }
        else
        {
            if (speed <= maxSpeed - speedIncrements)
            {
                speed += speedIncrements;
            }
        }
        if (Input.GetAxis("Jump") != 0 && isGrounded)
        {
            vel.y = jump;
           // rb.velocity = new Vector2(rb.velocity.x, jump);
            //rb.AddForce(transform.up * jump, ForceMode2D.Impulse);
            //rb.AddForceAtPosition(transform.position, transform.up * jump, ForceMode2D.Impulse);
        }

        vel.x = direction * speed;
        if(vel.y > 0)
        {
            vel.y = Mathf.Clamp(vel.y - fallSpeed * Time.deltaTime, 0, jump);
        }

        an.SetBool("isGrounded", isGrounded);
        if(speed > 0)
        {
            an.SetBool("Walk", true);
        }
        else
        {
            an.SetBool("Walk", false);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Ground")
        {
            isGrounded = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
        {
            isGrounded = false;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = vel;
       // rb.MovePosition(rb.position + velocity * speed * Time.fixedDeltaTime * direction);
    }

}
