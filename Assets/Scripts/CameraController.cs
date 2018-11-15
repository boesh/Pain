using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Transform target;
    public float smooth;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
        if (target != null)
        transform.position = new Vector3(transform.position.x + (target.position.x - transform.position.x)
            / smooth, transform.position.y + (target.position.y - transform.position.y) / smooth, transform.position.z);
	}
}
