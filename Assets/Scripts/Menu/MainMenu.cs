using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public bool autoRun = false;
	public string toLoad = "Map";

	// Use this for initialization
	void Start () {
		if (autoRun)
			Invoke ("Load", 3);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.anyKeyDown) {
			SceneManager.LoadScene (toLoad);
		}
	}


	void Load() {
		SceneManager.LoadScene (toLoad);
	}
}
