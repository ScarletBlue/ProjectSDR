using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJump : MonoBehaviour {

	public float distance = 1f;
	public float speed = 2f;
	MovementController movement;
	KeyboardMovementInput controlKey;

	bool wallJumping;

	void Start ()
	{
		movement = GetComponent<MovementController>();
	}
	

	void Update ()
	{
		Physics2D.queriesStartInColliders = false;
		RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, distance);

		if(Input.GetKeyDown (controlKey.defaultJumpKey) && !movement.onFloor && hit.collider !=null)
		{
			GetComponent<Rigidbody2D>().velocity = new Vector2(speed * hit.normal.x, speed);
			movement.moveVelocity = speed * hit.normal.x;
			wallJumping = true;
			transform.localScale = transform.localScale.x == 1 ? new Vector2(-1, 1) : Vector2.one;
		}

		else if (hit.collider != null && wallJumping)
		{
			wallJumping = false;
		}
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if ((!wallJumping || movement.onFloor))
			movement.moveVelocity = 0;
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawLine(transform.position, transform.position + Vector3.right * transform.localScale.x * distance);
	}
}
