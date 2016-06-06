using UnityEngine;
using System.Collections;

public class PlayerDrag : MonoBehaviour {


	private Walker walker;
	private bool wasEnabled;

	// Use this for initialization
	void Start () {
		walker = GetComponent<Walker> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void Drag(Vector2 direction, float intensity) {
		Debug.Log ("got here!");
		StartCoroutine(drag(direction, intensity));
	}

	IEnumerator drag(Vector2 direction, float intensity)
	{
		float acc = 2 * intensity,
		delta = acc/15;

		while (acc > 0)
		{
			//wasEnabled = walker.enabled;
			walker.enabled = false;

			if (transform && transform.gameObject)
				PixelMover.Move (transform, direction.x, direction.y, acc * Time.fixedDeltaTime);
			else
				acc = 0;

			yield return new WaitForSeconds(Time.fixedDeltaTime);

			acc -= delta;
		}

		walker.enabled = true;
	}
}
