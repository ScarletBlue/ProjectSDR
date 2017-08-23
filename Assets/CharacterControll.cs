using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterControll : MonoBehaviour {


    public float moveSpeed = 0;
    public float dashSpeed;
    public float jump1Speed;
    public float jump2Speed;
    public Character character;
    public float dashDelayTime = 0.3f;
    public float airDashTime = 0.5f;
    public float knockBackTime = 0.3f;
    public bool canJump = true;
    public float respawnTime = 5f;

    public KeyCode key_jump;
    public KeyCode key_right;
    public KeyCode key_left;
    public KeyCode key_up;
    public KeyCode key_down;

    Rigidbody2D rb;
    Animator anim;    

    int jumpCount = 0;
    float dashDelay = 0f;
    int dashInput = 0;
    public bool isDashing = false;
    int dashDirection = 0;
    float airDashDelay = 0f;
    bool isDead = false;
    Vector3 respawnPosition;


    public float hp = 1000;
    public bool isCasting = false;
    public bool hit = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        respawnPosition = GetComponent<Transform>().position;
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
        if(hp<=100)
        {
            anim.SetBool("hp_low", true);
        }
        else
        {
            anim.SetBool("hp_low", false);
        }

        if (GetComponent<CharacterControll>().hp <= 0 && isDead == false)
        {
            isDead = true;
            anim.SetTrigger("death");
            StartCoroutine(DeathDelay());
            

        }

    }

    int MovingDirection() // retrun 1(right) , -1(left), 0(none)
    {
        if (Input.GetKey(key_right) && !isCasting)
        {
            transform.localScale = new Vector3(1, 1, 1);
            return 1;
        }
        else if (Input.GetKey(key_left) && !isCasting)
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
            rb.velocity = new Vector2(moveSpeed * MovingDirection(),rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(moveSpeed * MovingDirection(), rb.velocity.y);
        }
    }

    void Jump()
    {
        if(jumpCount == 0 && canJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jump1Speed);
            jumpCount++;
        }
        else if(jumpCount == 1 && canJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jump2Speed);
            jumpCount++;
        }
    }

    IEnumerator DeathDelay()
    {
        yield return null;
        CharacterControll characterControll = GetComponent<CharacterControll>();
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        characterControll.enabled = false;
        yield return new WaitForSeconds(respawnTime);
        characterControll.enabled = true;
        hp = 1000;
        isDead = false;
        GetComponent<Transform>().position = respawnPosition;
        anim.SetTrigger("respawn");


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

    public void Hit(float damage, int attack, Vector2 knockBackDirection)
    {
        if(enabled)
        { 
            hit = true;
            hp -= damage;
            StartCoroutine(KnockBackDelay(damage));
            rb.velocity = new Vector2(0, 0);
            rb.AddForce(knockBackDirection * (2000 - hp) * attack * 0.05f);
            transform.localScale = new Vector3(-(knockBackDirection.x / Mathf.Abs(knockBackDirection.x)), 1, 1);
            anim.SetTrigger("hit");
        }
    }

    IEnumerator KnockBackDelay(float damage)
    {
        yield return new WaitForSeconds(knockBackTime);
        hit = false;
    }
}
