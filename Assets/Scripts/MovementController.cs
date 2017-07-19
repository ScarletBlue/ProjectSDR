using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class MovementController : MonoBehaviour
{

	Rigidbody2D rb2d;
	SpriteRenderer spriteRenderer;

	HealthController healthController;

	//float Speed;
	Animator animator;
	// Layers

	[SerializeField]
	LayerMask groundLayerMask;

	// Movement
	public float moveVelocity;

	public float jumpVelocity;
	public float doubleJumpVelocity;

	[SerializeField]
	float wallSlideVelocity =-1f;

	[HideInInspector]
	public bool onFloor;
	[HideInInspector]
	public bool performedDoubleJump;
	[HideInInspector]
	public bool movementDisabled;
	/*
	bool onWall;
	bool performedWallJump;
	public float wallJumpDistance = 1f;
	*/

	// Sprite Rendering

	bool facingRight;
	public bool IsFacingRight()
	{
		return facingRight;
	}

	void Awake()
	{
		rb2d = GetComponent<Rigidbody2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();

		healthController = GetComponent<HealthController> ();

		animator = GetComponent<Animator>();
	}

	void Start()
	{
		movementDisabled = false;

		facingRight = true;

		SetIfOnFloor();
	}

	void Update()
	{
		//Debug.Log(Mathf.Abs(rb2d.velocity.x));
		SetIfOnFloor();
		//SetJumpAnim();
	}


	public enum Direction
	{
		Left,
		Right,
		None
	}

	public void Move(Direction direction)
	{
		if (healthController != null)
		{
			if (healthController.GetHealth () <= 0)
			{
				return;
			}
		}

		if (movementDisabled)
		{
			return;
		}

		switch (direction)
		{
			case Direction.Left:
				rb2d.velocity = new Vector2(-moveVelocity, rb2d.velocity.y);
				animator.SetFloat("Speed", Mathf.Abs(rb2d.velocity.x));
				//animator.SetBool("Grounded", true);
				break;
			case Direction.Right:
				rb2d.velocity = new Vector2(moveVelocity, rb2d.velocity.y);
				animator.SetFloat("Speed", Mathf.Abs(rb2d.velocity.x));
				//animator.SetBool("Grounded", true);
				break;
			case Direction.None:
				rb2d.velocity = new Vector2(0, rb2d.velocity.y);
				animator.SetFloat("Speed", Mathf.Abs(rb2d.velocity.x));
				//animator.SetBool("Grounded", true);
				break;
		}

		SetSpriteDirection(direction);
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		Debug.Log("Hit a wall");
		if(coll.gameObject.tag == "Wall")
		{
			rb2d.drag = 200f;
		}
	}



	void SetIfOnFloor()
	{
		Debug.DrawLine(transform.position, (transform.position + (Vector3.down * ((spriteRenderer.bounds.size.y/2)+0.1f))), Color.red, 0, false);
		// Debug.DrawRay(transform.position, Vector3.down, Color.red, 1, false);
		RaycastHit2D floorHit = Physics2D.Raycast(transform.position, Vector2.down, (spriteRenderer.bounds.size.y/2)+0.1f, groundLayerMask);
		onFloor = (floorHit.collider != null);
		if (onFloor)
		{
			Debug.Log(onFloor);
			performedDoubleJump = false;
			animator.SetBool("Grounded", true);
		}
		else if(!onFloor)
		{
			Debug.Log(onFloor);
			animator.SetBool("Grounded", false);
		}
	}

	/*
	void SetIfOnWall()
	{
		Debug.DrawLine(transform.position, (transform.position + (Vector3.right * ((spriteRenderer.bounds.size.y / 2) + 0.1f))), Color.green, 0, false);
		// Debug.DrawRay(transform.position, Vector3.down, Color.red, 1, false);
		RaycastHit2D wallHit = Physics2D.Raycast(transform.position, Vector2.right, (spriteRenderer.bounds.size.y / 2) + 0.1f, groundLayerMask);
		onWall = (wallHit.collider != null);
		if (onWall) performedWallJump = false;
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawLine(transform.position, transform.position + Vector3.right * transform.localScale.x * wallJumpDistance);
	}
	*/

	public void Jump()
	{
		if (healthController != null)
		{
			if (healthController.GetHealth () <= 0)
			{
				return;
			}
		}

		if (onFloor)
		{
			rb2d.velocity = new Vector2(rb2d.velocity.x, jumpVelocity);
			//animator.SetBool("Grounded", false);

		}
		else if (!performedDoubleJump)
		{
			performedDoubleJump = true;
			rb2d.velocity = new Vector2(rb2d.velocity.x, doubleJumpVelocity);
			//animator.SetBool("Grounded", false);

		}
	}

	void SetJumpAnim()
	{
		if(rb2d.velocity.y < 0)
		{
			animator.SetBool("FallingJump", true);
		}
		if(rb2d.velocity.y > 0.1)
		{
			animator.SetBool("RisingJump", true);
		}

	}

	public void SetSpriteDirection(Direction direction)
	{
		switch (direction)
		{
			case Direction.Left:
				if (facingRight)
				{
					FlipSprite();
				}
				break;
			case Direction.Right:
				if (!facingRight)
				{
					FlipSprite();
				}
				break;
			default:
				break;
		}
	}

	void FlipSprite()
	{
		spriteRenderer.flipX = !spriteRenderer.flipX;
		facingRight = !facingRight;
	}

	public void Knockback(Vector3 force)
	{
		movementDisabled = true;
		StartCoroutine (EnableMovement (1));

		// Debug.Log (force);
		rb2d.AddForce (force);
	}

	IEnumerator EnableMovement(float time)
	{
		yield return new WaitForSeconds (time);
		movementDisabled = false;
	}


}
