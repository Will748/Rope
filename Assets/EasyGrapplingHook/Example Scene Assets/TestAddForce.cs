using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAddForce : MonoBehaviour {

    public float force;
    public Rigidbody2D rb;

    // Use this for initialization
    void Awake () {
        rb = gameObject.GetComponent<Rigidbody2D>();

    }
	
	// Update is called once per frame
	void FixedUpdate () {

        float h = Input.GetAxis("Horizontal");
        {
            rb.AddForce(new Vector3(h * (Time.deltaTime * force), 0));
        }
		
	}
}
