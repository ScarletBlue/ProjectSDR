using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MovementController))]
public class KeyboardMovementInput : MonoBehaviour {

	MovementController movementController;

	public KeyCode defaultMoveLeftKey;
	public KeyCode defaultMoveRightKey;
	public KeyCode defaultJumpKey;

	KeyCode moveLeftKey;
	KeyCode moveRightKey;
	KeyCode jumpKey;

	void Awake() {
		movementController = GetComponent<MovementController>();
	}

	// Use this for initialization
	void Start () {
		moveLeftKey = defaultMoveLeftKey;
		moveRightKey = defaultMoveRightKey;
		jumpKey = defaultJumpKey;
	}
	
	// Update is called once per frame
	void Update () {

		int movement = 0;

		if (Input.GetKey(moveLeftKey))
			movement--;

		if (Input.GetKey(moveRightKey))
			movement++;

		switch (movement){
			case -1:
				movementController.Move(MovementController.Direction.Left);
				break;
			case 1:
				movementController.Move(MovementController.Direction.Right);
				break;
			default:
				movementController.Move(MovementController.Direction.None);
				break;
		}

		if (Input.GetKeyDown(jumpKey)) {
			movementController.Jump();
		}
	}
}
