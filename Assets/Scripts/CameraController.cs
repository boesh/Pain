using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Transform target;
    public float smooth;

	void Start () {
        target = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	void LateUpdate () {
        if (target != null)
        transform.position = new Vector3(transform.position.x + (target.position.x - transform.position.x)
            / smooth, transform.position.y + (target.position.y - transform.position.y) / smooth, transform.position.z);
	}
}
