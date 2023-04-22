using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quad : MonoBehaviour {

    // Use this for initialization

    private int gScore;         //distance between quad and start point  
    private int hScore;         //distance betweeen quad and target 
    private int fScore;         //combined score of h and g

    private GameObject parent;

    private void Start()
    {
        FindNeighbours();
    }

    public int GScore
    {
        get {
            return gScore;
        }

        set
        {
            gScore = value;
        }
        
    }

    public int HScore
    {
        get
        {
            return hScore;
        }

        set
        {
            gScore = value;
        }

    }

    public int FScore
    {
        get
        {
            return hScore + gScore;
        }

    }

    public GameObject Parent
    {
        get
        {
            return parent;
        }
        set
        {
            parent = value;
        }
    }

    public List<GameObject> FindNeighbours()
    {
        RaycastHit ray;
        List<GameObject> arr = new List<GameObject>();
        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        if (Physics.Raycast(transform.position, Vector3.right, out ray, 1)) { 
            Debug.Log("right" + ray.transform.gameObject);
            if(ray.transform.gameObject.tag == "Untaken")arr.Add(ray.transform.gameObject);
        }
        if (Physics.Raycast(transform.position, Vector3.forward, out ray, 1))
        {
            Debug.Log("up" + ray.transform.gameObject);
            if (ray.transform.gameObject.tag == "Untaken") arr.Add(ray.transform.gameObject);
        }
        if (Physics.Raycast(transform.position, Vector3.left, out ray, 1))
        {
            Debug.Log("left" +ray.transform.gameObject);
            if (ray.transform.gameObject.tag == "Untaken") arr.Add(ray.transform.gameObject);
        }
        if (Physics.Raycast(transform.position, -Vector3.forward, out ray, 1))
        {
            Debug.Log("down"+ray.transform.gameObject);
            if (ray.transform.gameObject.tag == "Untaken") arr.Add(ray.transform.gameObject);
        }
        return arr;
    }
}
