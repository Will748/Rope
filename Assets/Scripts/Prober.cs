using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prober : MonoBehaviour {

    public GameObject grapple;
    private Gtrigger gt;
    private Vector3 dir;

    // Use this for initialization
	void Start () {

        Physics.IgnoreCollision(this.GetComponent<Collider>(), GameObject.FindWithTag("Grapple").GetComponent<Collider>());

    }

    // Update is called once per frame
    void Update () {
		
	}

    public void Initialize(Vector3 dir)
    {
        this.dir = dir;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag != "Player" && other.gameObject.tag != "gtrigger" && other.gameObject.tag != "Grapple")
        {
            if (!gt) gt = GameObject.FindWithTag("gTrigger").GetComponent<Gtrigger>();
            gt.TimeStepCount = 0;
            dir = dir.normalized;
            transform.position += (dir.normalized * 0.01f);
            //Debug.Log(transform.position.ToString("F6"));
            //Instantiate(new GameObject("yo"), (transform.position + dir.normalized * 0.01f), Quaternion.identity);

            //Debug.Log(transform.position.ToString("F6"));
            gameObject.SetActive(false);
        }
    }
}
