using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterController : MonoBehaviour {


    public float speed;
    public float dashSpeed;
    public float jump1Speed;
    public float jump2Speed;
    public Character character;
    public float dashDelayTime = 0.3f;
    public float airDashTime = 0.5f;
    public float knockBackTime = 0.3f;

    public Text hpText; //테스트용

    public KeyCode key_jump;
    public KeyCode key_right;
    public KeyCode key_left;

    Rigidbody2D rb;
    Animator anim;    

    int jumpCount = 0;
    float dashDelay = 0f;
    int dashInput = 0;
    public bool isDashing = false;
    int dashDirection = 0;
    float airDashDelay = 0f;

    public float hp = 1000;

    bool hit = false;

    ///캐릭터 컨트롤러 구분
    public int thisCharacter = 1;
    int player1 = 2;
    int player2 = 1;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();


        ///캐릭터 컨트롤러 구분
        if(player1 == thisCharacter)
        {
            key_jump = KeyCode.W;
            key_left = KeyCode.A;
            key_right = KeyCode.D;
        }
        else if(player1 == thisCharacter)
        {
            key_jump = KeyCode.T;
            key_left = KeyCode.F;
            key_right = KeyCode.H;
        }
        if (player2 == thisCharacter)
        {
            key_jump = KeyCode.T;
            key_left = KeyCode.F;
            key_right = KeyCode.H;
        }
        else if (player2 == thisCharacter)
        {
            key_jump = KeyCode.W;
            key_left = KeyCode.A;
            key_right = KeyCode.D;
        }

    }

    // Update is called once per frame
    void Update()
    {
        IsOnFloor();
        IsDashing();
        if (!isDashing && !hit)
        {
            Movement();
            anim.SetBool("isDashing", false);
            if(MovingDirection() != 0)
            {
                anim.SetBool("isWalking", true);
            }
            else
            {
                anim.SetBool("isWalking", false);
            }
        }
        else if (isDashing && !hit)
        {
            Dash();
            anim.SetBool("isDashing", true);
            anim.SetBool("isWalking", false);
        }
        if(Input.GetKeyDown(key_jump))
        {
            Jump();
        }
        if (hpText != null)
        {
            hpText.text = "hp : " + hp + "/1000";//테스트용
        }
        if(hp<=100)
        {
            anim.SetBool("hp_low", true);
        }
        else
        {
            anim.SetBool("hp_low", false);
        }
    }

    int MovingDirection() // retrun 1(right) , -1(left), 0(none)
    {
        if (Input.GetKey(key_right))
        {
            transform.localScale = new Vector3(1, 1, 1);
            return 1;
        }
        else if (Input.GetKey(key_left))
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
        if(Input.GetKeyDown(key_right) && dashInput ==0)
        {
            dashInput++;
            dashDelay = 0;
            return;
        }
        else if(Input.GetKeyDown(key_right) && dashInput < 0)
        {
            dashInput = 0;
            dashDelay = 0;
            return;
        }
        else if (Input.GetKeyDown(key_left) && dashInput == 0)
        {
            dashInput--;
            dashDelay = 0;
            return;
        }
        else if (Input.GetKeyDown(key_left) && dashInput > 0)
        {
            dashInput = 0;
            dashDelay = 0;
            return;
        }

        if (Input.GetKeyDown(key_right) && dashInput == 1 && dashDelay <= dashDelayTime)
        {
            airDashDelay = 0f;
            dashDirection = 1;
            isDashing = true;
        }
        else if (Input.GetKeyDown(key_left) && dashInput == -1 && dashDelay <= dashDelayTime)
        {
            airDashDelay = 0f;
            dashDirection = -1;
            isDashing = true;
        }

        if(Input.GetKeyUp(key_right) || Input.GetKeyUp(key_left))
        {
            if(isDashing)
            {
                dashInput = 0;
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
            airDashDelay += Time.deltaTime;
            rb.velocity = new Vector2(dashSpeed * dashDirection, Mathf.Max(0f, rb.velocity.y));
            if(airDashDelay >= airDashTime && isDashing)
            {
                dashDirection = 0;
                isDashing = false;
            }
        }
    }

    public bool IsOnFloor()
    {
        int layerMask = 1 << LayerMask.NameToLayer("Character");
        layerMask = ~layerMask;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, GetComponent<BoxCollider2D>().size.y / 2 + 0.1f, layerMask);
        if (hit.collider != null && hit.collider.tag == "Platform")
        {
            
            return true;
        }
        else if (hit.collider != null && hit.collider.tag == "movingPlatform")
        {
            transform.parent = hit.transform;
            return true;
        }
        else
        {
            transform.parent = null;
            return false;
        }
    }

    public void Hit(float damage, Attack attack, Vector2 knockBackDirection)
    {
        hit = true;
        hp -= damage;
        StartCoroutine(KnockBackDelay(damage));
        rb.velocity = new Vector2(0, 0);
        rb.AddForce(knockBackDirection * (2000 - hp) * (int)attack * 0.05f);
    }

    IEnumerator KnockBackDelay(float damage)
    {
        yield return new WaitForSeconds(knockBackTime);
        hit = false;
    }
}
