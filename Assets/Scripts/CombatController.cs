using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(MovementController))]
[RequireComponent(typeof(HealthController))]
public class CombatController : MonoBehaviour {

	SpriteRenderer spriteRenderer;
	MovementController movementController;
	HealthController healthController;
	Animator animator;

	[SerializeField]
	LayerMask attackableLayerMask;

	public GameObject blockCircle;

	public int strength;

	bool canBlock;
	public float maxBlockingTime;
	float blockingTime;
	public float maxBlockingDelay;
	float blockingDelay;
	public float blockingRefreshRate;
	public float blockingPenalty;

	public float knockbackForce;

	int strengthMultiplier;

	[SerializeField]
	// Color baseColor;

	bool isBlocking;
	public bool GetIsBlocking() {
		return isBlocking;
	}

	void Awake() {
		spriteRenderer = GetComponent<SpriteRenderer>();
		movementController = GetComponent<MovementController>();
		healthController = GetComponent<HealthController>();
		animator = GetComponent<Animator> ();
	}

	void Start() {
		// baseColor = spriteRenderer.color;

		canBlock = true;

		strengthMultiplier = 1;

		blockingTime = maxBlockingTime;

		SetBlocking(false);

		healthController.OnCharacterDeath += HealthController_OnCharacterDeath;
	}

	void HealthController_OnCharacterDeath ()
	{
		SetBlocking (false);
		canBlock = false;
	}

	void Update() {
		HandleBlockingTime ();

		// Debug.Log (blockingTime);
	}

	public void SetBlocking(bool value) {

		if (!canBlock) {
			Debug.Log ("Can't block");
			return;
		} 
		else {
			if (value) {
				Debug.Log ("Blocking");
			} 
			else {
				Debug.Log ("Not Blocking");
			}
		}

		isBlocking = value;
		if (value == false) {
			// spriteRenderer.color = baseColor;
			blockCircle.SetActive (false);
			blockingDelay = maxBlockingDelay;
		} 
		else {
			blockCircle.SetActive(true);
		}
	}

	void HandleBlockingTime() {
		// if blocking
		if (isBlocking) {

			// blocking time -= delta
			blockingTime -= Time.deltaTime;

			// If blocking time =< 0
			if (blockingTime <= 0) {

				// set blocking time = 0
				blockingTime = 0;

				// apply penalty time
				StartCoroutine (ResetBlocking (blockingPenalty));

				// Set to be not blocking
				SetBlocking (false);

				// disable blocking
				canBlock = false;
				Debug.Log ("Diabling blocking");

			}

			float blockingTimeAsPercent = blockingTime / maxBlockingTime;
			blockCircle.transform.localScale = new Vector3 (blockingTimeAsPercent, blockingTimeAsPercent, 1); 
			// spriteRenderer.color = Color.Lerp(baseColor, Color.black, (blockingTime/maxBlockingTime));
		}

		// else
		else if (!isBlocking && canBlock) {

			// decrement time
			blockingDelay -= Time.deltaTime;

			// If time since stopped blocking if greater than delay
			if (blockingDelay <= 0) {

				// increate blocking time by rate
				blockingTime += Time.deltaTime * blockingRefreshRate;

				// Cap blocking time
				if (blockingTime > maxBlockingTime) {
					blockingTime = maxBlockingTime;
				}
			}
		}
	}

	IEnumerator ResetBlocking(float time) {
		yield return new WaitForSeconds (time);

		blockingTime = maxBlockingTime;
		blockingDelay = maxBlockingDelay;
		canBlock = true;
		Debug.Log ("Enabling blocking");
	}

	public void Attack() {
		if (healthController.GetHealth () <= 0) {
			return;
		}

		// Perform Raycast
		Vector3 facingDir = movementController.IsFacingRight() ? Vector3.right : Vector3.left;
		Vector3 raycastStart = transform.position + (facingDir * (((spriteRenderer.bounds.size.x/5) + 0.35f)));

		Debug.DrawLine(raycastStart, (raycastStart + (facingDir * 0.5f)), Color.red, 0.5f, false);
		// Debug.DrawLine(transform.position, (transform.position + (facingDir * ((spriteRenderer.bounds.size.x/2)+0.5f))), Color.red, 0.5f, false);

		// Trigger Animation
		animator.SetTrigger("Kick");

		RaycastHit2D characterHit = Physics2D.Raycast(raycastStart, facingDir, 0.1f, attackableLayerMask);
		// RaycastHit2D characterHit = Physics2D.Raycast(transform.position, facingDir, (spriteRenderer.bounds.size.y/2)+0.5f, attackableLayerMask);
		if (characterHit.collider != null) {
			CombatController otherCharacterCombatController = characterHit.collider.gameObject.GetComponent<CombatController>();

			if (otherCharacterCombatController != null) {
				otherCharacterCombatController.TakeAttack(strength*strengthMultiplier, transform.position);
			}
		}

		strengthMultiplier = 1;
	}

	public void TakeAttack(int damage, Vector3 source) {

		if (healthController.GetHealth () <= 0) {
			return;
		}

		// Hand subtracting health
		int damageTaken = damage;

		if (isBlocking) {
			damageTaken = 0;
			strengthMultiplier++;
		}

		healthController.TakeDamage(damageTaken);

		// Handle Knockback
		if (damageTaken > 0) {
			float knockbackXDirection = 0;
			if (source.x < transform.position.x) {
				knockbackXDirection = 1;
			} 
			else if (source.x > transform.position.x) {
				knockbackXDirection = -1;
			}
			// Debug.Log ("direction: " + knockbackXDirection);
			Vector3 knockback = new Vector3 (knockbackXDirection * knockbackForce * Mathf.Cos (30*Mathf.Deg2Rad), knockbackForce * Mathf.Sin (30*Mathf.Deg2Rad));

			movementController.Knockback (knockback);
		}
	}
}
