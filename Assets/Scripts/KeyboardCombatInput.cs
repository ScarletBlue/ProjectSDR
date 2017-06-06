using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CombatController))]
public class KeyboardCombatInput : MonoBehaviour {

	CombatController combatController;

	public KeyCode defaultAttackKey;
	public KeyCode defaultBlockKey;

	KeyCode attackKey;
	KeyCode blockKey;

	void Awake() {
		combatController = GetComponent<CombatController>();
	}

	void Start() {
		attackKey = defaultAttackKey;
		blockKey = defaultBlockKey;
	}

	void Update() {
		if (Input.GetKeyDown(attackKey)) {
			combatController.Attack();
		}

		if (Input.GetKeyDown (blockKey)) {
			combatController.SetBlocking (true);
		} 
		else if (Input.GetKeyUp (blockKey)) {
			combatController.SetBlocking (false);
		}
	}
}
