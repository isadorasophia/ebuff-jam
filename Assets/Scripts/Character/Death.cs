using UnityEngine;
using System.Collections;

public class Death : MonoBehaviour {
    PlayerManager pm;

	// Use this for initialization
	void Start () {
	    pm = GameObject.FindWithTag("GameManager").GetComponent<PlayerManager>();
    }

    // Update is called once per frame
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.tag == "Minion")
        {
            var minion = col.gameObject.GetComponent<Minion>();

            if (minion.c_team != Minion.Team.Neutral)
            {
                if (minion.c_team == Minion.Team.Orange && gameObject.tag == "Blue")
                {
                    StartCoroutine(Die());
                } else if (minion.c_team == Minion.Team.Blue && gameObject.tag == "Orange")
                {
                    StartCoroutine(Die());
                }
            }
        }
    }

    IEnumerator Die()
    {
        gameObject.GetComponent<Animator>().SetBool("Dead", true);

        yield return new WaitForSeconds(1);

        pm.RemovePlayer(gameObject.GetComponent<StateController>());
    }
}
