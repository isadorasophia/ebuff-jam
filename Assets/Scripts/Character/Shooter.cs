using UnityEngine;
using System.Collections;

public class Shooter : MonoBehaviour {


	public StateController controller;
	public GameObject projectilePrefab;
	public float moveDelay;
	public float shotDelay;
	public AudioClip[] clips;

	private float timeOfLastMovement;
	private float timeOfLastShot;
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


			if (controller.actions.Left_alt.IsPressed) {
				currentDirection.x--;
				timeOfLastMovement = Time.timeSinceLevelLoad;
			}
			if (controller.actions.Right_alt.IsPressed) {
				currentDirection.x++;
				timeOfLastMovement = Time.timeSinceLevelLoad;
			}
			if (controller.actions.Up_alt.IsPressed) {
				currentDirection.y++;
				timeOfLastMovement = Time.timeSinceLevelLoad;
			}
			if (controller.actions.Down_alt.IsPressed) {
				currentDirection.y--;
				timeOfLastMovement = Time.timeSinceLevelLoad;
			}

			currentDirection = currentDirection.normalized;

			SetState ();
		}


		if (Time.timeSinceLevelLoad > timeOfLastShot + shotDelay) {
			if (controller.actions.Attack.WasPressed) {
				GameObject currentProjectile = Instantiate (projectilePrefab) as GameObject;
				ProjectileBehavior pb = currentProjectile.GetComponent<ProjectileBehavior> ();
				pb.direction = Vector2.up * currentDirection.y + Vector2.right * currentDirection.x;
			}
		}
	}


	void SetState() {

		/*  if (currentDirection.x == 0 && currentDirection.y == 0) {
			controller.aimDirection = StateController.Direction.Down;
		} else */if (currentDirection.x < 0 && currentDirection.y == 0) {
			controller.aimDirection = StateController.Direction.Left;
		} else if (currentDirection.x > 0 && currentDirection.y == 0) {
			controller.aimDirection = StateController.Direction.Right;
		} else if (currentDirection.y > 0 && currentDirection.x == 0) {
			controller.aimDirection = StateController.Direction.Up;
		} else if (currentDirection.y < 0 && currentDirection.x == 0) {
			controller.aimDirection = StateController.Direction.Down;
		
		} else if (currentDirection.x > 0 && currentDirection.y > 0) {
			controller.aimDirection = StateController.Direction.UpRight;
		} else if (currentDirection.x > 0 && currentDirection.y < 0) {
			controller.aimDirection = StateController.Direction.DownRight;
		} else if (currentDirection.x < 0 && currentDirection.y > 0) {
			controller.aimDirection = StateController.Direction.UpLeft;
		} else if (currentDirection.x < 0 && currentDirection.y < 0) {
			controller.aimDirection = StateController.Direction.DownLeft;
		} 
	}
}
