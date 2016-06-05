using UnityEngine;
using System.Collections;

public class SpecialProjectileGiver : MonoBehaviour {

	public GameObject projectilePrefab;

	public string[] tagsToHit;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnTriggerEnter2D(Collider2D other) {
		if (ShouldHitTag(other.tag)) {
			Shooter shooter = other.gameObject.GetComponent<Shooter> ();
			shooter.numberOfBullets = 1;
			shooter.specialProjectilePrefab = projectilePrefab;
			Destroy (gameObject);
		}
	}



	public bool ShouldHitTag(string tag) {
		foreach (string element in tagsToHit)
			if (tag == element)
				return true;
		return false;
	}
}
