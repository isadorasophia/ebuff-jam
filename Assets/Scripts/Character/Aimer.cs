using UnityEngine;
using System.Collections;

public class Walker : MonoBehaviour {


	public StateController controller;
	public float moveDelay;
	public AudioClip[] clips;

	private float timeOfLastMovement;
	private Vector2 currentDirection;

	private Animator animator;


	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
		timeOfLastMovement = -Time.timeSinceLevelLoad;
		controller = gameObject.GetComponent<StateController> ();
	}

	// Update is called once per frame
	void Update () {

	}


	void FixedUpdate() {

		if (Time.timeSinceLevelLoad > timeOfLastMovement + moveDelay) {
			currentDirection = Vector2.zero;


			if (controller.actions.Left.IsPressed) {
				currentDirection.x--;
				timeOfLastMovement = Time.timeSinceLevelLoad;
			}
			if (controller.actions.Right.IsPressed) {
				currentDirection.x++;
				timeOfLastMovement = Time.timeSinceLevelLoad;
			}
			if (controller.actions.Up.IsPressed) {
				currentDirection.y++;
				timeOfLastMovement = Time.timeSinceLevelLoad;
			}
			if (controller.actions.Down.IsPressed) {
				currentDirection.y--;
				timeOfLastMovement = Time.timeSinceLevelLoad;
			}

			currentDirection = currentDirection.normalized;
			Debug.Log (currentDirection.ToString ());

			SetState ();
		}
	}


	void SetState() {

		if (currentDirection.x == 0 && currentDirection.y == 0) {
			controller.aimDirection = StateController.Direction.Down;
		} else if (currentDirection.x < 0) {
			controller.aimDirection = StateController.Direction.Left;
		} else if (currentDirection.x > 0) {
			controller.aimDirection = StateController.Direction.Right;
		} else if (currentDirection.y > 0) {
			controller.aimDirection = StateController.Direction.Up;
		} else if (currentDirection.y < 0) {
			controller.aimDirection = StateController.Direction.Down;
		}
	}
}
