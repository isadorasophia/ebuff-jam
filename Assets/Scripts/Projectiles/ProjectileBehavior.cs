using UnityEngine;
using System.Collections;

public abstract class ProjectileBehavior : MonoBehaviour {

	public Vector2 direction;
	public string[] tagsToHit;

	// Use this for initialization
	public virtual void Start () {

	}
	
	// Update is called once per frame
	public virtual void  Update () {

	}


	void OnCollisionEnter2D(Collision2D coll) {
		if (ShouldHitTag(coll.transform.tag))
			Boom ();
	}

	protected abstract void Boom ();

	public bool ShouldHitTag(string tag) {
		foreach (string element in tagsToHit)
			if (tag == element)
				return true;
		return false;
	}

		
}
