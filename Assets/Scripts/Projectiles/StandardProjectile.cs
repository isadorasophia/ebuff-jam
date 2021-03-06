﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StandardProjectile : ProjectileBehavior {

	public float lifetime = 0.2f;
	public float dist = 10, angle=60;
	public float numberOfRaycasts = 10;
	public float maxIntensity;
	//public Vector3 weaponPosition;


	private List<RaycastHit2D> alreadyHit;

	public override void Start() {
		alreadyHit = new List<RaycastHit2D> ();
		Invoke ("DestroyBullet", lifetime);

		//weaponPosition = transform.position - (Vector3)Vector2.up * 3;
	}


	protected override void Boom () {

		for (int i = 0; i < numberOfRaycasts; i++) {

			RaycastHit2D[] hit = Physics2D.RaycastAll((Vector2)transform.position,
													  RotatedDirection(i), dist);

			Debug.DrawRay (transform.position, RotatedDirection(i) * dist, Color.red, 1f);
			foreach (RaycastHit2D element in hit) {
				if (ShouldHitTag (element.transform.tag) && !alreadyHit.Contains(element)) {
					alreadyHit.Add (element);

					Minion minion = element.transform.GetComponent<Minion> ();
					if (minion) {
						// Debug.Log ("Boom! In " + element.transform.tag);
                        
						minion.changeMode((Minion.Mode)mode, element.transform.position - transform.position,
							Mathf.Max(maxIntensity * (1 - element.distance / dist), .5f));

                        if (mode != PlayerManager.Mode.Neutral)
                        {
                            minion.changeTeam((Minion.Team)team);
                        }
					}


					PlayerDrag playerDrag = element.transform.GetComponent<PlayerDrag> ();
					StateController playerController = element.transform.GetComponent<StateController> ();

					if (playerDrag && playerController) {
						if (playerController.team != team) {
							playerDrag.Drag (element.transform.position - transform.position, 
								Mathf.Max (maxIntensity * (1 - element.distance / dist), 1f));
						}
					}
				}
			}
		}
		alreadyHit.Clear ();
	}

	Vector2 RotatedDirection(int i) {
		return (Quaternion.AngleAxis(-angle/2 + i * angle/numberOfRaycasts, Vector3.forward)) * currentDirection ;
	}


	protected void DestroyBullet() {
		if (gameObject)
			Destroy (gameObject);
	}
}
