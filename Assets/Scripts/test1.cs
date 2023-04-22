using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public enum swingDirEn
{
    RIGHT = 0,
    LEFT = 1,
    UP = 2
}

[System.Serializable]
public partial class test1 : MonoBehaviour
{
    private System.Collections.Generic.List<int> grapplePositions;
    public GameObject gameO1;
    public GameObject gameO2;
    public static System.Collections.Generic.List<Vector3> compareAngles;
    private swingDirE swingDir;
    public static int i;
    public virtual void Start()
    {
        GameObject player = null;
        Transform worldMousePosition = null;
        this.StartCoroutine(this.TestForCollision(this.gameO1.transform.position, this.gameO2.transform.position));
    }

    public virtual void Update()
    {
    }

    /*function TestForCollision(){
	Debug.Log("hello");
}*/
    public virtual IEnumerator TestForCollision(Vector3 playerPos, Vector3 grapplePos)
    {
        RaycastHit hit = default(RaycastHit);
        int mask = 1 << 8;
        mask = ~mask;
        while (true)
        {
            GameObject player = GameObject.FindWithTag("Player");
            GameObject grapple = GameObject.FindWithTag("Grapple");
            if (Physics.Linecast(player.transform.position, grapple.transform.position, out hit, mask))
            {
                float dist = Vector3.Distance(player.transform.position, grapple.transform.position);
                Debug.Log("hit" + hit.transform.gameObject);
                test1.i++;
                ShootGrapple3.grapplePositions.Add(GameObject.FindWithTag("Grapple").transform.position);
                if (player.GetComponent<Rigidbody>().velocity.x > 0)
                {
                    this.swingDir = swingDirE.RIGHT; //player swinging right
                    test1.compareAngles.Add(Vector3.right);
                    Debug.Log("added1");
                }
                if (player.GetComponent<Rigidbody>().velocity.x < 0)
                {
                    this.swingDir = swingDirE.LEFT; //player swinging left
                    test1.compareAngles.Add(Vector3.right);
                    Debug.Log("added2");
                }
                /*if(player.GetComponent.<Rigidbody>().velocity.y > 2.5){
	   			swingDir = swingDirE.UP;
	   			compareAngles.Add(Vector3.right);
	   		}*/
                ShootGrapple3.swingDirs.Add(this.swingDir);
                GameObject.Find("gunNull").SendMessage("moveGrapple", hit.point);
                GameObject.FindWithTag("GrapGun").GetComponent<DrawGrappleLine>().getLinePositions();
            }
            else
            {
            }
            //Debug.Log("no hit");
            yield return null;
        }
    }

    public test1()
    {
        this.grapplePositions = new List<int>();
    }

    static test1()
    {
        test1.compareAngles = new List<Vector3>();
    }

}