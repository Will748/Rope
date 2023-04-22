using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

    public float r;
	// Use this for initialization
	void Start () {

        Collider[] colliders = Physics.OverlapSphere(transform.position, r);
        foreach(Collider coll in colliders) {
            Debug.Log(coll);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
