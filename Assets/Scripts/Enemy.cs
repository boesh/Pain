using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyStates
{
    Patrol,
    Chase
}

[SerializePrivateVariables]
public class Enemy : MonoBehaviour {

    public EnemyStates currentState;

    public Transform target;
    SpriteRenderer sr;
    public UnitData unitData = new UnitData(2, 1, 0, 50);
    Rigidbody2D rb2d;
    float elapsedAttackTime;

    private void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (elapsedAttackTime > unitData.attackInterval)
            {
                col.gameObject.GetComponent<Player>().unitData.currentHP -= unitData.damage;
                col.gameObject.GetComponent<Player>().rb.AddForce(unitData.isLooksRight ? (Vector2.up + Vector2.right) * 4 : (Vector2.up - Vector2.right) * 4, ForceMode2D.Impulse);
                elapsedAttackTime = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "DeathZone")
        {
            unitData.currentHP = 0;
        }

        if (col.gameObject.name == "EndEnemySpot")
        {
            currentState = EnemyStates.Patrol;
            unitData.moveBoost = 1;

            if (unitData.isLooksRight)
            {
                unitData.isLooksRight = false;
            }
            else
            {
                unitData.isLooksRight = true;
            }
        }
    }

    void Start ()
    {
        rb2d = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        target = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
        elapsedAttackTime += Time.deltaTime;
        sr.flipX = !unitData.isLooksRight;
    }

    void FixedUpdate () {

        if (unitData.currentHP <= 0)
        {
            Destroy(gameObject);
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            currentState = EnemyStates.Patrol;
        }
        
        if (currentState == EnemyStates.Chase)
        {
            unitData.isLooksRight = target.transform.position.x > transform.position.x;
            unitData.moveBoost = 1.5f;

            unitData.HorizontalMove(transform, unitData.isLooksRight);
        }

        if (currentState == EnemyStates.Patrol)
        {
            unitData.HorizontalMove(transform, unitData.isLooksRight);    
        }

    }
}
