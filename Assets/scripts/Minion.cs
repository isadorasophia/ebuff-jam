﻿using UnityEngine;
using System.Collections;

public class Minion : MonoBehaviour {

    enum Mode { Neutral, Cloud, Drink };
    enum Team { Neutral, Blue, Orange };

    /* Essential information for gameplay */
    private Mode c_mode;
    private Team c_team;

    public float d_speed      = 0.0f; /* default speed */

    private float c_speed      = 0.0f; /* current speed */
    private float c_size       = 0.0f; /* current size */
    private Vector2 tomove;

    /* Custom configurations */
    public float l_speed      = 5.0f;
    public float h_speed      = 15.0f;

    public float max_size     = 2;
    public float min_size     = .5f;

    public float acceleration = 20f;
    public float drag         = 1.2f;

    // use this for initialization
    void Start() {
        changeMode(Mode.Neutral);
        changeTeam(Team.Neutral);
    }

    // update is called once per frame
    void Update() {
        
    }

    // called in fixed time, fancy!
    void FixedUpdate()
    {
        c_speed = d_speed * Time.deltaTime;

        tomove.x = c_speed;

        // translates it!
        transform.Translate(tomove * Time.deltaTime);

        #region CameraLimits
        var dist = (transform.position - Camera.main.transform.position).z;

        var leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;
        var rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, dist)).x;
        var bottomBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, dist)).y;
        // Keep in mind that we must consider the bottom as the ground, which is corrected by .12
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
}
