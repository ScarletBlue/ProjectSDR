using UnityEngine;
using System.Collections;

public class SimpleAI : MonoBehaviour {

	public Transform target;

	MovementController movementController;

	CombatController combatController;

	public float trackBuffer;

	public float attackRange;

	public float attackSpeed;

	bool canAttack;

	void Awake() {
		movementController = GetComponent<MovementController> ();
		combatController = GetComponent<CombatController> ();
	}

	void Start() {
		canAttack = true;
	}

	void Update() {
		MoveTowardsTarget ();

		FaceTarget ();

		AttackIfInRange ();
	}

	void MoveTowardsTarget() {
		if (target.position.x < (transform.position.x - trackBuffer)) {
			movementController.Move (MovementController.Direction.Left);
		} 
		else if (target.position.x > (transform.position.x + trackBuffer)) {
			movementController.Move (MovementController.Direction.Right);
		} 
		else {
			movementController.Move (MovementController.Direction.None);
		}
	}

	void FaceTarget() {
		if (target.position.x < transform.position.x) {
			if (movementController.IsFacingRight()) {
				movementController.SetSpriteDirection (MovementController.Direction.Left);
			}
		} 
		else if (target.position.x > transform.position.x) {
			if (!movementController.IsFacingRight()) {
				movementController.SetSpriteDirection (MovementController.Direction.Right);
			}
		}
	}

	void AttackIfInRange() {
		float distanceFromTarget = Mathf.Abs (target.position.x - transform.position.x);

		if (distanceFromTarget < attackRange) {
			if (canAttack) {
				canAttack = false;
				combatController.Attack ();
				StartCoroutine (WaitAttackSpeed ());
			}
		}
	}

	IEnumerator WaitAttackSpeed() {
		yield return new WaitForSeconds (attackSpeed);

		canAttack = true;
	}
}
