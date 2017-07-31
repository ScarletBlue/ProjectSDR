using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour {

    public float jump1Speed;
    public float jump2Speed;

    Rigidbody2D rb;

    int jumpCount = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        if(Input.GetKeyDown("space"))
        {
            Jump();
        }
    }

    int MovingDirection() // retrun 1(right) , -1(left), 0(none)
    {
        if (Input.GetKey("right"))
        {
            return 1;
        }
        else if (Input.GetKey("left"))
        {
            return -1;
        }
        else return 0;
    }

    void Movement()
    {
        if(IsOnFloor())
        {
            jumpCount = 0;
            rb.velocity = new Vector2(5 * MovingDirection(),rb.velocity.y);
        }
        if(!IsOnFloor())
        {
            rb.velocity = new Vector2(5 * MovingDirection(), rb.velocity.y);
        }
    }

    void Jump()
    {
        if(jumpCount == 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jump1Speed);
            jumpCount++;
        }
        else if(jumpCount == 1)
        {
            rb.velocity = new Vector2(rb.velocity.x, jump2Speed);
            jumpCount++;
        }
    }

    bool IsOnFloor()
    {
        int layerMask = 1 << LayerMask.NameToLayer("Character");
        layerMask = ~layerMask;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, GetComponent<BoxCollider2D>().size.y / 2 + 0.1f, layerMask);
        if (hit.collider != null && hit.collider.tag == "Platform")
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
