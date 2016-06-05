using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PauseGameObject : MonoBehaviour {

	[HideInInspector]
	public float pauseDuration;

	public bool resumeWithPrevious = false;

	[HideInInspector]
	public bool paused = false;

	private Rigidbody2D rb;
	private Vector2 previousVelocity;
	private float previousAngularVelocity;
	private List<MonoBehaviour> movementsToEnable;
	private List<MonoBehaviour> movements;

	// Use this for initialization
	void Awake () {
		rb = GetComponent<Rigidbody2D> ();
		movementsToEnable = new List<MonoBehaviour> ();
		movements = new List<MonoBehaviour>();

		movements.Add(GetComponent<Walker>());
		movements.Add(GetComponent<Shooter>());

	}

	public void Pause() {
		paused = true;
		previousVelocity = rb.velocity;
		previousAngularVelocity = rb.angularVelocity;
		rb.isKinematic = true;

		foreach (MonoBehaviour mov in movements) {
			if (mov.enabled) {
				mov.enabled = false;
				movementsToEnable.Add (mov);
			}
		}
	}

	public void Pause(float pauseDuration) {
		Pause ();
		Invoke ("Resume", pauseDuration);
	}


	public void Resume() {
		if (paused) {
			paused = false;
			CancelInvoke ("Resume");
			rb.isKinematic = false;
			rb.WakeUp ();

			if (resumeWithPrevious) {
				rb.velocity = previousVelocity;
				rb.angularVelocity = previousAngularVelocity;
			}

			foreach (MonoBehaviour mov in movementsToEnable)
				mov.enabled = true;

			movementsToEnable.Clear ();
		}

	}
}
