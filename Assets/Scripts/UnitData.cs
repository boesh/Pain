using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class UnitData
{
    public float moveSpeed { get; private set; }
    public float attackInterval { get; private set; }
    public int damage { get; private set; }
    public int maxHP { get; private set; }
    public int currentHP;
    public float moveBoost;
    public bool grounded;


    public UnitData(float ms = 2, float ai = 2, int dmg = 5, int mHP = 100, float mb = 1, bool grnd = true)
    {
        moveSpeed = ms;
        attackInterval = ai;
        damage = dmg;
        maxHP = mHP;
        currentHP = mHP;
        moveBoost = mb;
        grounded = grnd;
    }

    public virtual void HorizontalMove (Transform unit, bool moveRight)
    {
        if (moveRight)
        {
            unit.position = new Vector3(unit.position.x + moveSpeed * moveBoost* Time.deltaTime, unit.position.y, unit.position.z);
        }
        else
        {
            unit.position = new Vector3(unit.position.x - moveSpeed * moveBoost * Time.deltaTime, unit.position.y, unit.position.z);
        }
    }
}
