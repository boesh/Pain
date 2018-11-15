using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializePrivateVariables]
public class Player : MonoBehaviour {

    enum States
    {
        Idle,
        Walk,
        Run,
        Jump,
        Sit,
        Die,
        RangeAttack
    }

    States currentPlayerState = States.Idle;
    float elapsedAttackTime = 0;
    SpriteRenderer rend;
    Animator anim;
    Arrow arrow;
    BoxCollider2D bc2dBody;
    Foots foots;
    public Rigidbody2D rb;
    public UnitData unitData = new UnitData(2, 1, 10, 100);

    void AnimationSets()
    {
        anim.SetInteger("Move", currentPlayerState == States.Walk ? 1 : 0);

        anim.SetInteger("FastMove", currentPlayerState == States.Run ? 1 : 0);

        anim.SetBool("Jump", currentPlayerState == States.Jump);

        anim.SetBool("Sit", currentPlayerState == States.Sit);

        if (currentPlayerState == States.RangeAttack)
        {
            anim.SetTrigger("RangeAttack");
        }

        if (currentPlayerState == States.Die)
        {
            anim.SetTrigger("Die");
        }
    }

    void RangeAttack()
    {
        currentPlayerState = States.RangeAttack;

        if (rend.flipX)
        {

            Arrow clone = Arrow.Instantiate(arrow, gameObject.transform.position + -Vector3.right / 2f + Vector3.up / 4f, arrow.transform.rotation * new Quaternion(0, 180, 0, 0));
            clone.flipX = rend.flipX;
            clone.damage = unitData.damage;
        }
        else
        {
            Arrow clone = Arrow.Instantiate(arrow, gameObject.transform.position + Vector3.right / 2f + Vector3.up / 4f, arrow.transform.rotation);
            clone.flipX = rend.flipX;
            clone.damage = unitData.damage;
        }

        elapsedAttackTime = 0;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "DeathZone")
        {
            unitData.currentHP = 0;
        }
    }

    void Start () {
        rb = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        bc2dBody = GetComponent<BoxCollider2D>();     
    }
	
	void Update ()
	{
        unitData.grounded = foots.grounded;
        elapsedAttackTime += Time.deltaTime;
        bc2dBody.size = new Vector2(rend.sprite.textureRect.size.x / rend.sprite.pixelsPerUnit, rend.sprite.textureRect.size.y / rend.sprite.pixelsPerUnit);


        if (Input.GetButton("Horizontal") && currentPlayerState != States.Sit)
        {
            rend.flipX = Input.GetAxis("Horizontal") < 0;
            currentPlayerState = States.Walk;
            unitData.moveBoost = 1;
            unitData.HorizontalMove(transform, Input.GetAxis("Horizontal") > 0);
        }

        if (Input.GetButton("Horizontal") && Input.GetButton("Run") && currentPlayerState != States.Sit)
        {
            rend.flipX = Input.GetAxis("Horizontal") < 0;
            currentPlayerState = States.Run;
            unitData.moveBoost = 2;
            unitData.HorizontalMove(transform, Input.GetAxis("Horizontal") > 0);
        }

        if (Input.GetButtonDown("Jump") && unitData.grounded)
        {
            rb.AddForce(Vector2.up * unitData.moveSpeed * unitData.moveBoost, ForceMode2D.Impulse);
        }

        if ((Input.GetAxis("Horizontal") == 0 || Input.GetButtonDown("Jump")))
        {
            currentPlayerState = States.Idle;
            unitData.moveBoost = 1;
        }

        if (Input.GetButtonDown("Attack") && elapsedAttackTime > unitData.attackInterval)
        {
            RangeAttack();
        }

        if (Input.GetAxis("Vertical") < 0)
        {
            currentPlayerState = States.Sit;
        }

        if (!unitData.grounded)
        {
            currentPlayerState = States.Jump;
        }

        if (unitData.currentHP <= 0)
        {
            currentPlayerState = States.Die;
        }

        AnimationSets();

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("AfterDie"))
        {
            Destroy(gameObject);
        }

    }

    void FixedUpdate()
    {
       
    }
}
