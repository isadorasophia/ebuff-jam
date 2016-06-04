﻿using UnityEngine;
using System.Collections;

public class Shooter : MonoBehaviour {


	public GameObject projectilePrefab;
	public float shotDelay;
	public AudioClip[] clips;

	private float timeOfLastShot;
	private Vector2 currentDirection;

	private Animator animator;
	private StateController controller;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
		controller = gameObject.GetComponent<StateController> ();
	}

	// Update is called once per frame
	void Update () {

	}


	void FixedUpdate() {

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
				GameObject currentProjectile = Instantiate (projectilePrefab) as GameObject;
				ProjectileBehavior pb = currentProjectile.GetComponent<ProjectileBehavior> ();
				pb.direction = Vector2.up * currentDirection.y + Vector2.right * currentDirection.x;

				timeOfLastShot = Time.timeSinceLevelLoad;
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
