using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class MovementController : MonoBehaviour {

	Rigidbody2D rb2d;
	SpriteRenderer spriteRenderer;

	HealthController healthController;

	// Layers

	[SerializeField]
	LayerMask groundLayerMask; 

	// Movement

	public float moveVelocity;

	public float jumpVelocity;
	public float doubleJumpVelocity;

	bool onFloor;
	bool performedDoubleJump;
	bool movementDisabled;

	// Sprite Rendering

	bool facingRight;
	public bool IsFacingRight() {
		return facingRight;
	}

	void Awake() {
		rb2d = GetComponent<Rigidbody2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();

		healthController = GetComponent<HealthController> ();
	}

	void Start() {
		movementDisabled = false;

		facingRight = true;

		SetIfOnFloor();
	}

	void Update() {
		SetIfOnFloor();
	}

	public enum Direction {
		Left,
		Right,
		None
	}

	public void Move(Direction direction) {
		if (healthController != null) {
			if (healthController.GetHealth () <= 0) {
				return;
			}
		}

		if (movementDisabled) {
			return;
		}

		switch (direction) {
			case Direction.Left:
				rb2d.velocity = new Vector2(-moveVelocity, rb2d.velocity.y);
				break;
			case Direction.Right:
				rb2d.velocity = new Vector2(moveVelocity, rb2d.velocity.y);
				break;
			case Direction.None:
				rb2d.velocity = new Vector2(0, rb2d.velocity.y);
				break;
		}

		SetSpriteDirection(direction);
	}

	void SetIfOnFloor() {
		
		Debug.DrawLine(transform.position, (transform.position + (Vector3.down * ((spriteRenderer.bounds.size.y/2)+0.1f))), Color.red, 0, false);
		// Debug.DrawRay(transform.position, Vector3.down, Color.red, 1, false);
		RaycastHit2D floorHit = Physics2D.Raycast(transform.position, Vector2.down, (spriteRenderer.bounds.size.y/2)+0.1f, groundLayerMask);
		onFloor = (floorHit.collider != null);
		if (onFloor) performedDoubleJump = false;
	}

	public void Jump() {
		if (healthController != null) {
			if (healthController.GetHealth () <= 0) {
				return;
			}
		}

		if (onFloor) {
			rb2d.velocity = new Vector2(rb2d.velocity.x, jumpVelocity);
		}
		else if (!performedDoubleJump) {
			performedDoubleJump = true;
			rb2d.velocity = new Vector2(rb2d.velocity.x, doubleJumpVelocity);
		}
	}

	public void SetSpriteDirection(Direction direction) {
		switch (direction) {
			case Direction.Left:
				if (facingRight) {
					FlipSprite();
				}
				break;
			case Direction.Right:
				if (!facingRight) {
					FlipSprite();
				}
				break;
			default:
				break;
		}
	}

	void FlipSprite() {
		spriteRenderer.flipX = !spriteRenderer.flipX;
		facingRight = !facingRight;
	}

	public void Knockback(Vector3 force) {
		movementDisabled = true;
		StartCoroutine (EnableMovement (1));

		// Debug.Log (force);
		rb2d.AddForce (force);
	}

	IEnumerator EnableMovement(float time) {
		yield return new WaitForSeconds (time);
		movementDisabled = false;
	}
}
