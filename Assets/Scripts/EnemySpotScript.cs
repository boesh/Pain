using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpotScript : MonoBehaviour {

    public Enemy enemy;

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            

            enemy.currentState = EnemyStates.Chase;
            enemy.unitData.moveBoost = 1.5f;
            if (enemy.target.position.x > enemy.transform.position.x)
            {
                enemy.looksRight = true;
            }
            else
            {
                enemy.looksRight = false;
            }
        }
    }

    // Use this for initialization
    void Start () {
        enemy = GetComponentInChildren<Enemy>();
	}
	
	// Update is called once per frame
	void Update () {
		if(enemy == null)
        {
            Destroy(gameObject);
        }
	}
}
