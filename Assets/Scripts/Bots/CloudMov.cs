using UnityEngine;
using System.Collections;

public class CloudMov : MonoBehaviour {
    private float speed;
    private int orientation;

	// Use this for initialization
	void Start () {
        speed = Random.Range(0f, 5f);

        orientation = transform.position.x < 0 ? 1 : -1;

        StartCoroutine(Die());
    }
	
	// Update is called once per frame
	void Update () {
        PixelMover.Move(transform, orientation, 0, speed * Time.fixedDeltaTime);
    }


    IEnumerator Die()
    {
        yield return new WaitForSeconds(240);

        Destroy(gameObject);
    }
}
