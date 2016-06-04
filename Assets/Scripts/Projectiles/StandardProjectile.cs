using UnityEngine;
using System.Collections;

public class StandardProjectile : ProjectileBehavior {

	public float lifetime = 0.2f;
	public float dist = 3, angle=60;
	public float numberOfRaycasts = 10;
	public float maxIntensity;

	public override void Start() {
		Boom();
		Invoke ("DestroyBullet", lifetime);
	}


	protected override void Boom () {

		for (int i = 0; i < numberOfRaycasts; i++) {

			RaycastHit2D[] hit = Physics2D.RaycastAll((Vector2)transform.position,
													  RotatedDirection(i), dist);
			foreach (RaycastHit2D element in hit) {
				if (ShouldHitTag (element.transform.tag)) {
					Minion minion = element.transform.GetComponent<Minion> ();
					if (minion) {
						minion.changeMode(Minion.Mode.Neutral, RotatedDirection(i), maxIntensity*(1 - element.distance/dist));
					}
				}
			}
		}
	}

	Vector2 RotatedDirection(int i) {
		return (Quaternion.AngleAxis(-angle/2, Vector3.forward)) * direction ;
	}


	protected void DestroyBullet() {
		if (gameObject)
			Destroy (gameObject);
	}
}
