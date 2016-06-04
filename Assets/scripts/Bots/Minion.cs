using UnityEngine;
using System.Collections;

public class Minion : MonoBehaviour
{

    enum Mode { Neutral, Cloud, Drink };
    enum Team { Neutral, Blue, Orange };

    /* Essential information for gameplay */
    private Mode c_mode;
    private Team c_team;

    public float d_speed = 0.0f; /* default speed */

    private float c_speed = 0.0f; /* current speed */
    private float c_size = 0.0f; /* current size */

    /* Custom configurations */
    public float l_speed = 5.0f;
    public float h_speed = 15.0f;

    public float max_size = 2;
    public float min_size = .5f;

    public float min_dis = 1.5f;

	public string[] tagsToAvoid;

    /* Gameplay settings */
    public Transform target;

    // use this for initialization
    void Start()
    {
        changeMode(Mode.Neutral);
        changeTeam(Team.Neutral);
    }

    // update is called once per frame
    void Update()
    {

    }

    // called in fixed time, fancy!
    void FixedUpdate()
    {
        /* verify range */
        float range = Vector2.Distance(transform.position, target.position);

        if (range > min_dis)
        {
            /* check colision! */
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.forward, min_dis);

<<<<<<< HEAD
            if (hit.transform != null && shouldAvoidTag(hit.transform.tag))
=======
			if (hit.transform != null && ShouldAvoidTag(hit.transform.tag))
>>>>>>> c263b112acfa4aebd38530b587e422bc43f0479b
            {
                Vector3 dir = target.position - hit.transform.position;

                Debug.Log("Find obstacle!");
                
                /* Just turn around it */
                if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
                {
                    PixelMover.Move(transform, 0, Mathf.Sign(dir.y), d_speed * .5f * Time.deltaTime);
                }
                else
                {
                    PixelMover.Move(transform, Mathf.Sign(dir.x), 0, d_speed * .5f * Time.deltaTime);
                }
            }
            else
            {
                Vector3 dir = target.position - transform.position;
                float x, y;

                /* Check directions */
                if (Mathf.Abs(dir.x) > min_dis)
                {
                    x = Mathf.Sign(dir.x);
                }
                else
                {
                    x = 0;
                }

                if (Mathf.Abs(dir.y) > min_dis)
                {
                    y = Mathf.Sign(dir.y);
                }
                else
                {
                    y = 0;
                }

                /* Go to the player! */
                PixelMover.Move(transform, x, y, d_speed * Time.deltaTime);
                
                // transform.position = Vector2.MoveTowards(transform.position, target.position, d_speed * Time.deltaTime);
            }
        }
        else
        {
            // work on the attack
        }

        #region CameraLimits
        var dist = (transform.position - Camera.main.transform.position).z;

        var leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;
        var rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, dist)).x;
        var bottomBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, dist)).y;

        // keep in mind that we must consider the bottom as the ground, which is corrected by .12
        var topBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, .12f, dist)).y;

        // now, checks if the player desired position has passed the limits - if so, force him to stay
        transform.position = new Vector2(
                                         Mathf.Clamp(transform.position.x, leftBorder, rightBorder),
                                         Mathf.Clamp(transform.position.y, topBorder, bottomBorder));
        #endregion
    }

    // change mode of current minion
    void changeMode(Mode n_mode)
    {
        this.c_mode = n_mode;

        if (c_mode == Mode.Cloud)
        {
            d_speed = h_speed;
            c_size = min_size;
        }
        else if (c_mode == Mode.Drink)
        {
            d_speed = l_speed;
            c_size = max_size;
        }
        else if (c_mode == Mode.Neutral)
        {
            d_speed = (h_speed + l_speed) / 2;
            c_size = 1;
        }
    }

    // change team of current minion
    void changeTeam(Team n_team)
    {
        this.c_team = n_team;

        if (c_team == Team.Blue)
        {

        }
        else if (c_team == Team.Orange)
        {

        }
        else if (c_team == Team.Neutral)
        {

        }
    }



    bool shouldAvoidTag(string tag)
    {
        foreach (string element in tagsToAvoid)
        {
            if (tag == element)
            {
                return true;
            }
        }
   
        return false;
    }

}
