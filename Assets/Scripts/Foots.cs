using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foots : MonoBehaviour {

    public bool grounded;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            grounded = true;
        }
    }


    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            grounded = false;
        }
    }
}
