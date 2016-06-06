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

		PixelMover.Move (transform, currentDirection.x, currentDirection.y, speed * Time.deltaTime);

        /* check camera offset */
        #region CameraLimits
        var dist = (transform.position - Camera.main.transform.position).z;

        var leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;
        var rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, dist)).x;
        var bottomBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, dist)).y;

        // keep in mind that we must consider the bottom as the ground, which is corrected by .12
        var topBorder = Camera.main.ViewportToWorldPoint(new Vector3(.5f, 0, dist)).y;

        // now, checks if the player desired position has passed the limits - if so, force him to stay
        transform.position = new Vector3(
                                         Mathf.Clamp(transform.position.x, leftBorder, rightBorder),
                                         Mathf.Clamp(transform.position.y, topBorder, bottomBorder),
										 Mathf.Clamp(transform.position.y, topBorder, bottomBorder));
        #endregion
    }


    void SetState() {

		if (currentDirection.x == 0 && currentDirection.y == 0) {
			controller.SetMovement (false);
		} else {
			if (currentDirection.x < 0) {
				controller.SetWalkDirection(StateController.Direction.Left);
			} else if (currentDirection.x > 0) {
				controller.SetWalkDirection(StateController.Direction.Right);
			} else if (currentDirection.y > 0) {
				controller.SetWalkDirection(StateController.Direction.Up);
			} else if (currentDirection.y < 0) {
				controller.SetWalkDirection(StateController.Direction.Down);
			}

			controller.SetMovement (true);
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
