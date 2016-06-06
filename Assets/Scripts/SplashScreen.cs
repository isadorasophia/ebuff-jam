using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour {

	public Text text;
	public AudioClip clip;

	public float changeTime = 1f;

<<<<<<< HEAD

	private bool changed1, changed2, changed3, changed4, changed5;
=======
	private bool changed1, changed2, changed3, changed4;
>>>>>>> a27f6fc10a0cad88a06f29f9c40f09c6704b25b2


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.timeSinceLevelLoad > changeTime && !changed1) {
			text.text = "Mr.Strings";
			AudioSource.PlayClipAtPoint(clip, transform.position, 5f);
			changed1 = true;
		}

		if (Time.timeSinceLevelLoad > 2*changeTime && !changed2) {
			text.text = "@MatheusMortatti";
			AudioSource.PlayClipAtPoint(clip, transform.position);
			changed2 = true;
		}
        
		if (Time.timeSinceLevelLoad > 3*changeTime && !changed3) {
			text.text = "@LeoCVF";
			AudioSource.PlayClipAtPoint(clip, transform.position);
			changed3 = true;
		}

		if (Time.timeSinceLevelLoad > 4*changeTime && !changed4) {
			text.text = "Isadora Sophia";
			AudioSource.PlayClipAtPoint(clip, transform.position);
			changed4 = true;
		}
<<<<<<< HEAD


		if (Time.timeSinceLevelLoad > 5*changeTime && !changed5) {
=======
        
		if (Time.timeSinceLevelLoad > 4*changeTime && !changed4) {
>>>>>>> a27f6fc10a0cad88a06f29f9c40f09c6704b25b2
			text.text = "Presents...";
			AudioSource.PlayClipAtPoint(clip, transform.position);
			changed4 = true;
		}

		if (Time.timeSinceLevelLoad > 5*changeTime) {
			text.text = "";
			SceneManager.LoadScene ("Main Menu");
		}
	}
}
