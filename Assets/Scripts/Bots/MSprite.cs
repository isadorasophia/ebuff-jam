using UnityEngine;
using System.Collections;

public class MSprite : MonoBehaviour {
    private Animator minion_a;

    // Use this for initialization
    void Awake() {
        minion_a = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {

    }

    public void set_direction(int x, int y)
    {
        if (x != 0)
        {
            /* Side */
            minion_a.SetInteger("Direction", 0);
               
            if (x > 0)
            {
                /* Right */
				transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            } else
            {
                /* Left */
				transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }

        } else if (y == 1)
        {
            /* Up */
            minion_a.SetInteger("Direction", 1);
        } else if (y == -1)
        {
            /* Down */
            minion_a.SetInteger("Direction", -1);
        }
    }

    public void set_team(Minion.Team team)
    {
        minion_a.SetInteger("Team", (int)team);
    }
}
