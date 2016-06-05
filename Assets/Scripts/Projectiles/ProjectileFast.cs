using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProjectileFast : ProjectileBehavior {

	public float lifetime = 3f;
	public float dist = 10, angle=360;
	public float numberOfRaycasts = 60;
	public float maxIntensity;


	private List<RaycastHit2D> alreadyHit;
	private bool alreadyBoom;

	public override void Start() {
		alreadyHit = new List<RaycastHit2D> ();
		Invoke ("Boom", lifetime);
	}
		

	protected override void Boom () {
		if (gameObject != null && !alreadyBoom) {
			alreadyBoom = true;

			for (int i = 0; i < numberOfRaycasts; i++) {

				RaycastHit2D[] hit = Physics2D.RaycastAll ((Vector2)transform.position,
					                     RotatedDirection (i), dist);

				Debug.DrawRay (transform.position, RotatedDirection (i) * 10, Color.red, 1f);
				foreach (RaycastHit2D element in hit) {
					if (ShouldHitTag (element.transform.tag) && !alreadyHit.Contains (element)) {
						alreadyHit.Add (element);

						Minion minion = element.transform.GetComponent<Minion> ();
						if (minion) {
							// Debug.Log ("Boom! In " + element.transform.tag);

							minion.changeMode ((Minion.Mode)mode);
							minion.changeTeam ((Minion.Team)team);
						}
					}
				}
			}

			DestroyBullet ();
		}
	}

	Vector2 RotatedDirection(int i) {
		return (Quaternion.AngleAxis(-angle/2 + i * angle/numberOfRaycasts, Vector3.forward)) * currentDirection ;
	}


	protected void DestroyBullet() {
		if (gameObject) {
			Destroy (gameObject);
		}
	}
}
