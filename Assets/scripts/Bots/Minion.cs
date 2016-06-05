using UnityEngine;
using System.Collections;

public class Minion : MonoBehaviour
{
    public enum Mode { Neutral, Cloud, Drink };
    public enum Team { Neutral, Blue, Orange };

    /* Essential information for gameplay */
    private Mode c_mode = Mode.Neutral;
    private Team c_team = Team.Neutral;

    private float d_speed     = 0.0f; /* default speed */

    private float c_speed     = 0.0f; /* current speed */
    private float c_acc       = 0.0f; /* current acceleration */
    private float c_osc       = 0.0f; /* current acceleration oscilation */
    private float c_size      = 0.0f; /* current size */

    private bool d_gameplay   = true; /* check if can continue default gameplay */

    /* Custom configurations */
    public float min_speed  = 5.0f;
    public float max_speed  = 15.0f;

    public float weapon_acc = 0.0f;
    public float max_acc    = 0.0f; 
    public float osc        = 0.0f; /* speed of oscilation */

    public float max_size   = 2;
    public float min_size   = .5f;

    public float min_dis    = 1.5f;

	public string[] tagsToAvoid;

    /* Gameplay settings */
    public Transform target;

    // use this for initialization
    void Start()
    {
        changeTeam(Team.Neutral);
        changeMode(Mode.Neutral);
    }

    // update is called once per frame
    void Update()
    {
        if (d_gameplay)
        {
            #region Movement
            /* verify range */
            float range = Vector2.Distance(transform.position, target.position);

            /* work on accelaration */
            c_osc += Time.deltaTime * osc;
            c_acc = (Mathf.Abs(Mathf.Sin(c_osc)) + .25f) * max_acc;

            /* get current speed */
            c_speed = d_speed * c_acc;

            /* should i walk or attack!? */
            if (range > min_dis)
            {
                /* before I can walk, check colision! */
                RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.forward, min_dis);

                /* is there an object to be avoided? */
                if (hit.transform != null && shouldAvoidTag(hit.transform.tag))
                {
                    Vector3 dir = target.position - hit.transform.position;

                    Debug.Log("Find obstacle!");

                    /* Just turn around it */
                    if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
                    {
                        PixelMover.Move(transform, 0, Mathf.Sign(dir.y), c_speed * .5f * Time.fixedDeltaTime);
                    }
                    else
                    {
                        PixelMover.Move(transform, Mathf.Sign(dir.x), 0, c_speed * .5f * Time.fixedDeltaTime);
                    }
                }
                else
                {
                    Vector3 dir = target.position - transform.position;
                    float x, y;

                    /* Check directions */
                    x = Mathf.Abs(dir.x) > min_dis ? Mathf.Sign(dir.x) : 0;
                    y = Mathf.Abs(dir.y) > min_dis ? Mathf.Sign(dir.y) : 0;

                    /* go get the player! */
                    PixelMover.Move(transform, x, y, c_speed * Time.fixedDeltaTime);
                }
            }
            else
            {
                #region Attack
                /* attack! */
                #endregion
            }
            #endregion
        }

        /* check camera offset */
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
    public void changeMode(Mode n_mode)
    {
        changeMode(n_mode, Vector2.up);
    }

    // change mode of current minion (default signature)
    public void changeMode(Mode n_mode, Vector2 direction, float intensity = 1)
    {
        this.c_mode = n_mode;

        if (c_mode == Mode.Cloud)
        {
            d_speed = max_speed;
            c_size = min_size;
        }
        else if (c_mode == Mode.Drink)
        {
            d_speed = min_speed;
            c_size = max_size;
        }
        else if (c_mode == Mode.Neutral)
        {
            d_speed = (max_speed + min_speed) / 2; // standard speed
            c_size = 5;                            // size remains the default

            transform.localScale = new Vector3(c_size, c_size, 1);

            /* Minion is now dragged */
            d_gameplay = false;
            StartCoroutine(drag(direction, intensity));
        }
    }

    // change team of current minion
    public void changeTeam(Team n_team)
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
            d_speed = (max_speed + min_speed) / 2; // standard speed
            c_size = 5;                            // size remains the default

            transform.localScale = new Vector3(c_size, c_size, 1);
        }
    }

    /* check if a given tag should be avoided */
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

    IEnumerator drag(Vector2 direction, float intensity)
    {
        float acc = weapon_acc * intensity,
              delta = acc/25;
        
        while (acc > 0)
        {
            PixelMover.Move(transform, direction.x, direction.y, acc * Time.fixedDeltaTime);

            yield return new WaitForSeconds(Time.fixedDeltaTime);
      
            acc -= delta;
        }

        d_gameplay = true;
    }
}
