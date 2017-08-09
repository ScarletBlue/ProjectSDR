using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour {


    public float speed;
    public float dashSpeed;
    public float jump1Speed;
    public float jump2Speed;
    public Character character;
    public float dashDelayTime = 0.3f;
    public float AirDashTime = 0.5f;

    Rigidbody2D rb;
    Animator anim;    

    int jumpCount = 0;
    float dashDelay = 0f;
    int dashInput = 0;
    bool isDashing = false;
    int dashDirection = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        IsDashing();
        if (!isDashing)
        {
            Movement();
            anim.SetBool("isDashing", false);
        }
        else if (isDashing)
        {
            Dash();
            anim.SetBool("isDashing", true);
        }
        if(Input.GetKeyDown("space"))
        {
            Jump();
        }
    }

    int MovingDirection() // retrun 1(right) , -1(left), 0(none)
    {
        if (Input.GetKey("right"))
        {
            transform.localScale = new Vector3(1, 1, 1);
            return 1;
        }
        else if (Input.GetKey("left"))
        {
            transform.localScale = new Vector3(-1, 1, 1);
            return -1;
        }
        else return 0;
    }

    void Movement()
    {
        if(IsOnFloor())
        {
            if (jumpCount != 0 && rb.velocity.y <= 0.1)
            {
                jumpCount = 0;
            }
            rb.velocity = new Vector2(speed * MovingDirection(),rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(speed * MovingDirection(), rb.velocity.y);
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

    void IsDashing()
    {
        dashDelay += Time.deltaTime;
        if(dashDelay> dashDelayTime)
        {
            dashInput = 0;
        }
        if(Input.GetKeyDown("right") && dashInput ==0)
        {
            dashInput++;
            dashDelay = 0;
            return;
        }
        else if(Input.GetKeyDown("right") && dashInput < 0)
        {
            dashInput = 0;
            dashDelay = 0;
            return;
        }
        else if (Input.GetKeyDown("left") && dashInput == 0)
        {
            dashInput--;
            dashDelay = 0;
            return;
        }
        else if (Input.GetKeyDown("left") && dashInput > 0)
        {
            dashInput = 0;
            dashDelay = 0;
            return;
        }

        if (Input.GetKeyDown("right") && dashInput == 1 && dashDelay <= dashDelayTime)
        {
            dashDirection = 1;
            isDashing = true;
        }
        else if (Input.GetKeyDown("left") && dashInput == -1 && dashDelay <= dashDelayTime)
        {
            dashDirection = -1;
            isDashing = true;
        }

        if(Input.GetKeyUp("right") || Input.GetKeyUp("left"))
        {
            if(isDashing)
            {
                dashDirection = 0;
                isDashing = false;
            }
        }
    }

    void Dash()
    {
        if(IsOnFloor())
        {
            if (jumpCount != 0 && rb.velocity.y <= 0.1)
            {
                jumpCount = 0;
            }
            rb.velocity = new Vector2(dashSpeed * dashDirection, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(dashSpeed * dashDirection, Mathf.Max(0f, rb.velocity.y));
            StartCoroutine(AirDashDelay());
        }
    }

    IEnumerator AirDashDelay()
    {
        yield return new WaitForSeconds(AirDashTime);
        if (!IsOnFloor())
        {
            dashDirection = 0;
            isDashing = false;
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

    public void Hit(float Damage, Attack attack)
    {
        Debug.Log("hit!");
    }
}
