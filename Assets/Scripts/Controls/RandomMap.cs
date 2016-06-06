using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RandomMap : MonoBehaviour {
    public int t_obj;
    public int minions;

    public List<GameObject> obs;
    public GameObject minion;

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

            var leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(.1f, .1f, dist)).x;
            var rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(.9f, .1f, dist)).x;
            var bottomBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, .9f, dist)).y;
            var topBorder = Camera.main.ViewportToWorldPoint(new Vector3(.1f, .1f, dist)).y;

            x = Random.Range(leftBorder, rightBorder);
            y = Random.Range(bottomBorder, topBorder);

            Instantiate(obs[next], new Vector3(x, y, 0), obs[next].transform.rotation);
        }

        /* generate the plantas */
        for (int i = 0; i < t_obj/2; i++)
        {
            // Get camera offsets
            var dist = (transform.position - Camera.main.transform.position).z;

            var leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(.1f, .1f, dist)).x;
            var rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(.9f, .1f, dist)).x;
            var bottomBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, .9f, dist)).y;
            var topBorder = Camera.main.ViewportToWorldPoint(new Vector3(.1f, .1f, dist)).y;

            x = Random.Range(leftBorder, rightBorder);
            y = Random.Range(bottomBorder, topBorder);

            Instantiate(obs[0], new Vector3(x, y, 0), obs[0].transform.rotation);
        }

        for (int i = 0; i < minions; i++)
        {
            // Get camera offsets
            var dist = (transform.position - Camera.main.transform.position).z;

            var leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(.1f, .1f, dist)).x;
            var rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(.9f, .1f, dist)).x;
            var bottomBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, .9f, dist)).y;
            var topBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, .1f, dist)).y;

            x = Random.Range(leftBorder, rightBorder);
            y = Random.Range(bottomBorder, topBorder);

            Instantiate(minion, new Vector3(x, y, 0), minion.transform.rotation);
        }
    }
}
