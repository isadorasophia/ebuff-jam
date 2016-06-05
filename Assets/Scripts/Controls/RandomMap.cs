using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RandomMap : MonoBehaviour {
    public int t_obj;
    public List<GameObject> obs;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	public void SetMap () {
        int next;
        float x, y;

        for (int i = 0; i < t_obj; i++)
        {
            next = Random.Range(0, obs.Count);

            // Get camera offsets
            var dist = (transform.position - Camera.main.transform.position).z;

            var leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;
            var rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, dist)).x;
            var bottomBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, dist)).y;
            var topBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).y;

            x = Random.Range(leftBorder, rightBorder);
            y = Random.Range(bottomBorder, topBorder);

            Instantiate(obs[next], new Vector3(x, y, 0), obs[next].transform.rotation);
        }
	}
}
