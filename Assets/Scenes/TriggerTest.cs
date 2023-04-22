using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTest : MonoBehaviour {

    int i = 5;
    public float speed;
    public GameObject other;
    public GameObject other2;
    // Use this for initialization
	void Start () {
        i = -i;
        Debug.Log(0.1 * 3);
		
	}
	
	// Update is called once per frame
	void Update () {
        //transform.Translate(speed * Time.deltaTime, 0, 0);
        Vector3 cross = Vector3.Cross(this.gameObject.transform.position - other.transform.position, Vector3.forward);
        Debug.DrawRay(other.gameObject.transform.position, cross.normalized, Color.red, 1000);
        float dotProduct = Vector3.Dot(cross.normalized, (gameObject.transform.position - other2.gameObject.transform.position).normalized);
        Debug.DrawRay(other.transform.position, Vector3.right, Color.white, 1000);
        Vector3 dir1 = other.transform.position - transform.position;
        Vector3 dir = transform.position - other.transform.position;
        //float n1 = this.gameObject.transform.position.y - other.transform.position.y;

        double a = (Mathf.Atan2(this.gameObject.transform.position.y, this.gameObject.transform.position.x)) * Mathf.Rad2Deg;      //this object
        double b =(Mathf.Atan2(other.transform.position.y,other.transform.position.x)) * Mathf.Rad2Deg;                 //other object
        //Debug.Log(a);
        //Debug.Log(b);
        //Debug.Log(Vector3.Angle(dir1, dir));
        double c = a - b;
        if ((c) < 0) c += 360;
        Debug.Log((float)c);

    }


}
