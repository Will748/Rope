using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstarPF : MonoBehaviour {

    GameObject[] floors;

    public GameObject ob;
    public Transform target;
    public Transform startingNode;

    private List<GameObject> openList;
    private List<GameObject> closedList;

	// Use this for initialization
	void Start () {

        CalculateScore(startingNode.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(ManhattanDistance(ob.position, target.position));
    }

    void GiveSquaresPoints()
    {
        floors = GameObject.FindGameObjectsWithTag("Untaken");

    }

    int ManhattanDistance(Vector3 start, Vector3 end)
    {
        return Mathf.RoundToInt(Mathf.Abs(start.x - end.x) + Mathf.Abs(start.z - end.z));
    }

    List<GameObject> CalculateRoute(Transform gameObject, Transform target)
    {
        return new List<GameObject>();
    }

    void CalculateScore(GameObject node)
    {
        if (!closedList.Contains(node)) closedList.Add(node);

        if (node != null)
        {
            List<GameObject> arr = node.GetComponent<Quad>().FindNeighbours();
            foreach(GameObject a in arr)
            {
                if (!closedList.Contains(a)){
                    a.GetComponent<Quad>().HScore = ManhattanDistance(a.transform.position, target.transform.position);                                               //set the scores of neighbours
                    if ((a.GetComponent<Quad>().GScore > node.GetComponent<Quad>().GScore + 1))
                    {
                        a.GetComponent<Quad>().GScore = node.GetComponent<Quad>().GScore + 1;
                    }
                    openList.Add(a);                                                                                    // add to openlist
              
                }
            }
            int currentScore = 100;
            GameObject lowestScorer = null;
            foreach(GameObject b in openList)
            {
                if(b.GetComponent<Quad>().FScore < currentScore)                // find lowest scorer in open List,
                {                                                               //add them to closed list
                    currentScore = b.GetComponent<Quad>().FScore;
                    lowestScorer = b;
                }

            }
            if (lowestScorer == target) return;
            openList.Remove(lowestScorer);
            closedList.Add(lowestScorer);
            CalculateScore(lowestScorer);
        }
    }


    
}
