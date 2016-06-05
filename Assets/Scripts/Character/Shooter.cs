using UnityEngine;
using System.Collections;

public class Shooter : MonoBehaviour {


	public GameObject projectilePrefab;
	public float shotDelay;
	public AudioClip[] clips;

	private float timeOfLastShot;
	private Vector2 currentDirection;

	private StateController controller;

	// Use this for initialization
	void Start () {
		controller = gameObject.GetComponent<StateController> ();
	}


	void Update() {

		currentDirection = Vector2.zero;


		if (controller.actions.Left_alt.IsPressed) {
			currentDirection.x--;
		}
		if (controller.actions.Right_alt.IsPressed) {
			currentDirection.x++;
		}
		if (controller.actions.Up_alt.IsPressed) {
			currentDirection.y++;
		}
		if (controller.actions.Down_alt.IsPressed) {
			currentDirection.y--;
		}

		currentDirection = currentDirection.normalized;

		SetState ();


		if (Time.timeSinceLevelLoad > timeOfLastShot + shotDelay) {
			if (controller.actions.Attack.WasPressed) {
				Shoot ();
				timeOfLastShot = Time.timeSinceLevelLoad;
			}
		}
	}


	void Shoot() {

		GameObject currentProjectile = Instantiate (projectilePrefab) as GameObject;

		currentProjectile.transform.position = transform.position;

		ProjectileBehavior pb = currentProjectile.GetComponent<ProjectileBehavior> ();

		if (controller.aimDirection == StateController.Direction.Up) {
			pb.direction = Vector2.up;
		} else if (controller.aimDirection == StateController.Direction.Down) {
			pb.direction = Vector2.down;
		} else if (controller.aimDirection == StateController.Direction.Left) {
			pb.direction = Vector2.left;
		} else if (controller.aimDirection == StateController.Direction.Right) {
			pb.direction = Vector2.right;
		} else if (controller.aimDirection == StateController.Direction.UpLeft) {
			pb.direction = Vector2.up + Vector2.left;
		} else if (controller.aimDirection == StateController.Direction.UpRight) {
			pb.direction = Vector2.up + Vector2.right;
		} else if (controller.aimDirection == StateController.Direction.DownLeft) {
			pb.direction = Vector2.down + Vector2.left;
		} else if (controller.aimDirection == StateController.Direction.DownRight) {
			pb.direction = Vector2.down + Vector2.right;
		}


	}


	void SetState() {

		/*  if (currentDirection.x == 0 && currentDirection.y == 0) {
			controller.aimDirection = StateController.Direction.Down;
		} else */if (currentDirection.x < 0 && currentDirection.y == 0) {
			controller.SetAimDirection(StateController.Direction.Left);
		} else if (currentDirection.x > 0 && currentDirection.y == 0) {
			controller.SetAimDirection (StateController.Direction.Right);
		} else if (currentDirection.y > 0 && currentDirection.x == 0) {
			controller.SetAimDirection(StateController.Direction.Up);
		} else if (currentDirection.y < 0 && currentDirection.x == 0) {
			controller.SetAimDirection(StateController.Direction.Down);
		
		} else if (currentDirection.x > 0 && currentDirection.y > 0) {
			controller.SetAimDirection(StateController.Direction.UpRight);
		} else if (currentDirection.x > 0 && currentDirection.y < 0) {
			controller.SetAimDirection(StateController.Direction.DownRight);
		} else if (currentDirection.x < 0 && currentDirection.y > 0) {
			controller.SetAimDirection(StateController.Direction.UpLeft);
		} else if (currentDirection.x < 0 && currentDirection.y < 0) {
			controller.SetAimDirection(StateController.Direction.DownLeft);
		} 
	}
}
