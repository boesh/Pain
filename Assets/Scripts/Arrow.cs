using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializePrivateVariables]
public class Arrow : MonoBehaviour {

    public int damage = 10;
    public float speed = 5;
    //public Rigidbody2D rb2d;
    public PolygonCollider2D pc2d;
    public float timeoutDestructor = 5;
    float timer = 0;
    public bool flipX;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            col.gameObject.GetComponent<Enemy>().unitData.currentHP -= damage;
            Debug.Log(col.gameObject.GetComponent<Enemy>().unitData.currentHP);
            
            Destroy(gameObject);
        }

        else if (col.gameObject.tag != "Player" && col.gameObject.tag != "Arrow" && !col.isTrigger)
        {
            Destroy(gameObject);
        }

    }

    void Start () {
        //rb2d = GetComponent<Rigidbody2D>();
        pc2d = GetComponent<PolygonCollider2D>();
        
    }
	
	void Update () {
        timer += Time.deltaTime;
        if (timer > timeoutDestructor)
        {
            Destroy(gameObject);
        }

        if (flipX)
        {
            transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
        }
    }
}
