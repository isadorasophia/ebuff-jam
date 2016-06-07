using UnityEngine;
using System.Collections;

public class GameAudioManager : MonoBehaviour {

	public AudioClip intro, chorus;
	public AudioSource source;

	// Use this for initialization
	void Start () {
		source.clip = chorus;
		source.Play ();

		source.clip = intro;
		source.loop = false;
		source.Play ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!source.isPlaying) {
			source.loop = true;
			source.clip = chorus;
			source.Play ();
		}
	}
}
