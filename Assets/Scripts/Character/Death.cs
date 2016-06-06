﻿using UnityEngine;
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
                    pm.RemovePlayer(gameObject.GetComponent<StateController>());
                } else if (minion.c_team == Minion.Team.Blue && gameObject.tag == "Orange")
                {
                    pm.RemovePlayer(gameObject.GetComponent<StateController>());
                }
            }
        }
    }
}