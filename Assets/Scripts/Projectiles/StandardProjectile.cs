using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StandardProjectile : ProjectileBehavior {

	public float lifetime = 0.2f;
	public float dist = 10, angle=60;
	public float numberOfRaycasts = 10;
	public float maxIntensity;


	private List<RaycastHit2D> alreadyHit;

	public override void Start() {
		alreadyHit = new List<RaycastHit2D> ();
		Boom();
		Invoke ("DestroyBullet", lifetime);
	}


	protected override void Boom () {

		for (int i = 0; i < numberOfRaycasts; i++) {

			RaycastHit2D[] hit = Physics2D.RaycastAll((Vector2)transform.position,
													  RotatedDirection(i), dist);

			Debug.DrawRay (transform.position, RotatedDirection(i) * 10, Color.red, 1f);
			foreach (RaycastHit2D element in hit) {
				if (ShouldHitTag (element.transform.tag) && !alreadyHit.Contains(element)) {
					alreadyHit.Add (element);

					Minion minion = element.transform.GetComponent<Minion> ();
					if (minion) {
						// Debug.Log ("Boom! In " + element.transform.tag);
                        
                        minion.changeMode(Minion.Mode.Neutral, element.transform.position - transform.position, Mathf.Max(maxIntensity * (1 - element.distance / dist), .5f));
					}
				}
			}
		}
			
		alreadyHit.Clear ();
	}

	Vector2 RotatedDirection(int i) {
		return (Quaternion.AngleAxis(-angle/2 + i * angle/numberOfRaycasts, Vector3.forward)) * direction ;
	}


	protected void DestroyBullet() {
		if (gameObject)
			Destroy (gameObject);
	}
}
