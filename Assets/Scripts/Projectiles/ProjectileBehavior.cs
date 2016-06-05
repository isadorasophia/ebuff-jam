﻿using UnityEngine;
using System.Collections;

public abstract class ProjectileBehavior : MonoBehaviour {

	public PlayerManager.Team team;
	public Vector2 currentDirection;
	public float speed;
	public string[] tagsToHit;

	// Use this for initialization
	public virtual void Start () {

	}
	
	// Update is called once per frame
	public virtual void  Update () {
		PixelMover.Move (transform, currentDirection.x, currentDirection.y, speed * Time.deltaTime);
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
