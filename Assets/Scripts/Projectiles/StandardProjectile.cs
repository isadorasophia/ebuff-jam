using UnityEngine;
using System.Collections;

public class StandardProjectile : ProjectileBehavior {

	public float lifetime = 3;



	protected override void Boom () {
		Invoke ("DestroyBullet", lifetime);
	}


	protected void DestroyBullet() {
		if (gameObject)
			Destroy (gameObject);
	}
}
