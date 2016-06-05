using UnityEngine;
using System.Collections;

public class Walker : MonoBehaviour {


	public float speed;
	public AudioClip[] clips;

	private Vector2 currentDirection;

	private StateController controller;

	// Use this for initialization
	void Start () {
		controller = gameObject.GetComponent<StateController> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
	}


	void Update() {

		currentDirection = Vector2.zero;


		if (controller.actions.Left.IsPressed) {
			currentDirection.x--;
		}
		if (controller.actions.Right.IsPressed) {
			currentDirection.x++;
		}
		if (controller.actions.Up.IsPressed) {
			currentDirection.y++;
		}
		if (controller.actions.Down.IsPressed) {
			currentDirection.y--;
		}

		//currentDirection = currentDirection.normalized;
		//Debug.Log (currentDirection.ToString ());

		SetState ();

		PixelMover.Move (transform, currentDirection.x, currentDirection.y, speed * Time.fixedDeltaTime);
	}


	void SetState() {

		if (currentDirection.x == 0 && currentDirection.y == 0) {
			controller.walkDirection = StateController.Direction.Down;
		} else if (currentDirection.x < 0) {
			controller.walkDirection = StateController.Direction.Left;
		} else if (currentDirection.x > 0) {
			controller.walkDirection = StateController.Direction.Right;
		} else if (currentDirection.y > 0) {
			controller.walkDirection = StateController.Direction.Up;
		} else if (currentDirection.y < 0) {
			controller.walkDirection = StateController.Direction.Down;
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Wall") {
			transform.position = transform.position - (Vector3)currentDirection;
		}
	}




	public void PlayClip() {
		AudioSource.PlayClipAtPoint (clips [Random.Range (0, clips.Length)], Vector3.zero, 0.5f);
	}
}
