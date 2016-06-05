using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CloudSystem : MonoBehaviour {
    public List<GameObject> clouds;

	// Use this for initialization
	void Start () {
        StartCoroutine(SpawnCloud());
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator SpawnCloud()
    {
        int next_spawn, offset = 20;

        // Get camera offsets
        var dist = (transform.position - Camera.main.transform.position).z;

        var leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;
        var rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, dist)).x;
        var bottomBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, dist)).y;
        var topBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, .12f, dist)).y;

        while (true)
        {
            yield return new WaitForSeconds(Random.Range(5, 20));
            next_spawn = Random.Range(0, clouds.Count - 1);

            float x, y;

            if (Random.Range(0, 10) > 5)
            {
                x = leftBorder - offset;
            } else
            {
                x = rightBorder + offset;
            }

            y = Random.Range(topBorder, bottomBorder);

            Instantiate(clouds[next_spawn], new Vector3(x, y, 0), clouds[next_spawn].transform.rotation);
        }
    }
}
