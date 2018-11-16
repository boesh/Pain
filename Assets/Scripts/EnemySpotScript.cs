﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpotScript : MonoBehaviour {

    public Enemy enemy;

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {

            enemy.currentState = EnemyStates.Chase;

           
           
        }
    }

    void Start ()
    {
        enemy = GetComponentInChildren<Enemy>();
	}
	
	void Update ()
    {
		if(enemy == null)
        {
            Destroy(gameObject);
        }
	}
}
