using UnityEngine;
using System.Collections;

public enum swingDirE
{
    RIGHT = 0,
    LEFT = 1,
    UP = 2
}

[System.Serializable]
public partial class Gtrigger : MonoBehaviour
{
    public GameObject player;
    public GameObject drum;
    public GameObject prober;
    public static System.Collections.Generic.List<Vector3> swingObs;
    public static System.Collections.Generic.List<GameObject> hitObs;
    public static System.Collections.Generic.List<Vector3> compareAngles;
    private swingDirE swingDir;
    public GameObject SwingOb;
    private GameObject swingOb;
    private bool done;
    public GameObject gun;
    public static int i;

    private float timeStepCount;
    private Vector3 dir1;
    private GameObject proberInstance;
    private LayerMask mask;

    private int u = 0;
    private int up = 0;

    private DrawGrappleLine lineScript;

    public float TimeStepCount
    {
        set
        {
            timeStepCount = value;
        }
    }

    public Vector3 Dir1
    {
        get{
            return dir1;
        }
    }


    public virtual void Start()
    {
        if (!this.player)
        {
            this.player = GameObject.FindWithTag("Player");
        }
        if (!this.drum)
        {
            this.drum = GameObject.FindWithTag("Drum");
        }
        Physics.IgnoreCollision(this.player.GetComponent<Collider>(), this.GetComponent<Collider>());
        Physics.IgnoreCollision(this.GetComponent<Collider>(), this.drum.GetComponent<Collider>());
        //Physics.IgnoreCollision(this.GetComponent<Collider>(),prober.GetComponent<Collider>());
        //prober = new GameObject();
        //prober.AddComponent<Prober>();
        //prober.tag = "Prober";
        //prober.SetActive(false);
        proberInstance = Instantiate(prober);
        proberInstance.SetActive(false);
        mask = LayerMask.GetMask("test");
        Ray ray;

    }

    public virtual void Update()
    {
        if (Input.GetKey("j"))
        {
            Debug.Log((object) this.swingDir);
        }
        up++;
    }

    float vel = 0;

    public void FixedUpdate()
    {
        u++;
        Vector3 grapplePos = GameObject.FindWithTag("Grapple").transform.position;
        Vector3 gunPos = GameObject.FindWithTag("GrapGun").transform.position;
        Vector3 dir = GameObject.FindWithTag("Grapple").transform.position -GameObject.FindWithTag("Player").transform.position;
        float rayLength =  Vector3.Distance(grapplePos, gunPos);
        float height = (float)gameObject.GetComponent<CapsuleCollider>().height;

        //Debug.DrawRay(gunPos, dir.normalized * rayLength, Color.white, 1000f);

        RaycastHit hit;
        if (Physics.Raycast(gunPos, dir.normalized, out hit, rayLength))
        {
            if (timeStepCount < 2) return;
            Debug.DrawRay(gunPos, dir.normalized * rayLength, Color.white, 1000f);

            //Debug.Log(hit.transform.gameObject);
            //Debug.Log(timeStepCount);
            Instantiate(new GameObject("hitpoint"), hit.point, Quaternion.identity);
            timeStepCount = 0;
            //Debug.Log(timeStepCount);
            Vector3 contact = hit.point;
            ShootGrapple2.grapplePositions.Add(GameObject.FindWithTag("Grapple").transform.position);
            //ShootGrapple2.swingDirs.Add(swingDir);
            vel = this.player.GetComponent<Rigidbody>().velocity.x;
            //Debug.Log(this.player.GetComponent<Rigidbody>().velocity.x);
            if (vel > 0)
            {
                Debug.Log(i + "right");
                // Debug.Log(compareAngles.Count);
                this.swingDir = swingDirE.RIGHT; //player swinging right
                Gtrigger.compareAngles.Add(Vector3.right);
            }
            if (vel < 0)
            {
                Debug.Log(i + "left");
                //Debug.Log(compareAngles.Count);
                this.swingDir = swingDirE.LEFT;
                //swingDir = 2; //player swinging left
                Gtrigger.compareAngles.Add(Vector3.right);
            }
            /*if (this.player.GetComponent<Rigidbody>().velocity.y > 2.5f)
            {
                this.swingDir = swingDirE.UP;
                Gtrigger.compareAngles.Add(Vector3.right);
            }*/
            //if(player.rigidbody.velocity.x < 0)swingDir = 2; //player swinging left
            //if(PlayerPrefs.rigidbody.velocity.y >
            ShootGrapple2.swingDirs.Add(this.swingDir);
            Instantiate(new GameObject("hitpoint"), hit.point, Quaternion.identity);
            Vector3 dir2 = hit.point - hit.transform.position;
            Vector3 moveTo = hit.transform.position + (dir2.normalized * (dir2.magnitude + 0.01f));
            //GameObject.FindWithTag("Grapple").transform.position += dir.normalized * 0.01f;

            //Debug.Log(moveTo.ToString("F4"));
            //Instantiate(new GameObject("1" + Time.frameCount + collision.gameObject), contact.point, Quaternion.identity);
            //Instantiate(new GameObject("2" + Time.frameCount + collision.gameObject), moveTo, Quaternion.identity);
            // Debug.Log(contact.point.ToString("F4"));
            //Debug.Log(moveTo);
            GameObject.Find("gunNull").SendMessage("moveGrapple", moveTo);
            //ShootGrapple2.moveGrapple(contact.point);
            GameObject.FindWithTag("Grapple").GetComponent<DrawGrappleLine>().getLinePositions();
            //lineScript.getLinePositions();
            //ShootGrapple2.swingDirs(player.rigidbody.velocity.;
            //CreateSwingOb();

            //this.checkGrapPos();
            //Destroy(GameObject.FindWithTag("Prober"));
            proberInstance.SetActive(false);
            //yield return new WaitForSeconds(0.1f);
        }
        //Debug.Log(Time.frameCount);
        //Debug.Log(timeStepCount);
        else
        {
            timeStepCount++;
        }
    }

    public float CalculateAngle(Vector3 start, Vector3 finish)                   //calculate angle when start and finish of a 'line' are supplied, up to 360 deg
    {
        Vector3 dir = start - finish;
        float angle1 = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (angle1 < 0) angle1 = -angle1;
        return angle1;            
    }

    int z = 1;
    public virtual void OnCollisionStay(Collision collision)
    {
        //Debug.Log(collision.gameObject);
        //Debug.Log(collision.contacts[0].point);
        if (collision.gameObject/* && !ShootGrapple2.grapplePositions.Contains(collision.transform.position)*/ != this.player && collision.gameObject.tag != "Prober" /*&& collision.gameObject.tag !="used" && i == 0*/  && z == 0)
        {
            if (!proberInstance.activeSelf){
                vel = this.player.GetComponent<Rigidbody>().velocity.x;
                ContactPoint contact1 = collision.contacts[0];
                dir1 = contact1.point - collision.transform.position;
                //GameObject prob = Instantiate(prober, contact1.point, Quaternion.identity);
                proberInstance.SetActive(true);
                proberInstance.GetComponent<Prober>().Initialize(dir1);
                proberInstance.transform.position = contact1.point;
                //prob.transform.position = contact1.point;
                //prob.gameObject.GetComponent<Prober>().Initialize(dir1);
            }
            timeStepCount += Time.fixedDeltaTime;
            Debug.Log(collision.gameObject.tag + Time.frameCount + " " + timeStepCount);
            if (timeStepCount >= 0.04f)
            {
                //collision.gameObject.tag = "used";
                //EditorApplication.isPaused = true;
                Gtrigger.i++;
                //Physics.IgnoreCollision(GetComponent.<Collider>(), collision.gameObject.GetComponent.<Collider>()); 
                if (Gtrigger.i == 2)
                {
                    //this.StartCoroutine("trues");
                }
                ContactPoint contact = collision.contacts[0];
                ShootGrapple2.grapplePositions.Add(GameObject.FindWithTag("Grapple").transform.position);
                //ShootGrapple2.swingDirs.Add(swingDir);
                Debug.Log(this.player.GetComponent<Rigidbody>().velocity.x);
                if (vel > 0)
                {
                   Debug.Log(i + "right");
                   // Debug.Log(compareAngles.Count);
                    this.swingDir = swingDirE.RIGHT; //player swinging right
                    Gtrigger.compareAngles.Add(Vector3.right);
                }
                if (vel < 0)
                {
                    Debug.Log(i + "left");
                    //Debug.Log(compareAngles.Count);
                    this.swingDir = swingDirE.LEFT;
                    //swingDir = 2; //player swinging left
                    Gtrigger.compareAngles.Add(Vector3.right);
                }
                /*if (this.player.GetComponent<Rigidbody>().velocity.y > 2.5f)
                {
                    this.swingDir = swingDirE.UP;
                    Gtrigger.compareAngles.Add(Vector3.right);
                }*/
                //if(player.rigidbody.velocity.x < 0)swingDir = 2; //player swinging left
                //if(PlayerPrefs.rigidbody.velocity.y >
                ShootGrapple2.swingDirs.Add(this.swingDir);
                //Vector3 dir = contact.point - collision.transform.position;
                Vector3 moveTo = collision.transform.position + (dir1.normalized * (dir1.magnitude + 0.01f));
                //Debug.Log(moveTo.ToString("F4"));
                //Instantiate(new GameObject("1" + Time.frameCount + collision.gameObject), contact.point, Quaternion.identity);
                //Instantiate(new GameObject("2" + Time.frameCount + collision.gameObject), moveTo, Quaternion.identity);
               // Debug.Log(contact.point.ToString("F4"));
                GameObject.Find("gunNull").SendMessage("moveGrapple",prober.transform.position);
                //ShootGrapple2.moveGrapple(contact.point);
                lineScript.GetComponent<DrawGrappleLine>().getLinePositions();
                //ShootGrapple2.swingDirs(player.rigidbody.velocity.;
                //CreateSwingOb();
                /*if(compareAngles.Count > 1){
            }*/
                //this.checkGrapPos();
                //Destroy(GameObject.FindWithTag("Prober"));
                proberInstance.SetActive(false);
                timeStepCount = 0;
                //yield return new WaitForSeconds(0.1f);
            }
        }
    }

    public virtual void checkGrapPos()
    {
        int i = 0;
        int f = 0;
        int n = 1;
        while (i < ShootGrapple2.grapplePositions.Count)
        {
            while (f < ShootGrapple2.grapplePositions.Count)
            {
                if (Vector3.Distance(ShootGrapple2.grapplePositions[i], ShootGrapple2.grapplePositions[f]) < n)
                {
                    if (!(ShootGrapple2.grapplePositions[i] == ShootGrapple2.grapplePositions[f]))
                    {
                        ShootGrapple2.grapplePositions.RemoveAt(Mathf.Max(i, f));
                        ShootGrapple2.swingDirs.RemoveAt(Mathf.Max(i, f));
                        Gtrigger.compareAngles.RemoveAt(Mathf.Max(i, f));
                    }
                }
                f++;
            }
            i++;
            f = 0;
        }
    }

    public virtual IEnumerator trues()
    {
        while (true)
        {
            yield return null;
        }
    }

    //create an object to swing into
    //if(swingDir == 1)
     //swingOb.transform.Translate(-0.1,0,0);
    //if(swingDir == 2)
     //swingOb.transform.Translate(0.1,0,0);
    public virtual void CreateSwingOb()
    {
        float i = 0.0f;
        bool a = false;
        float velo = this.player.GetComponent<Rigidbody>().velocity.x;
        //if(rigidbody.velocity.x > 0.0000)i = -0.2; //swinging left;
        //if(rigidbody.velocity.x < 0.0000)i = 0.2; //swing right;
        if (velo > 0)
        {
            i = 0.2f; //swinging right;
        }
        if (velo < 0)
        {
            i = -0.2f; //swing left;
        }
        //Debug.Log(i);	
        GameObject swingOb = UnityEngine.Object.Instantiate(this.SwingOb, this.player.transform.position, Quaternion.identity);
        //swingOb.transform.rotation = Quaternion.LookRotation(ShootGrapple2.grapplePositions[ShootGrapple2.grapplePositions.Count-2]);
        float distance = Vector3.Distance(this.player.transform.position, GameObject.FindWithTag("Grapple").transform.position);
        distance = distance - 0.5f;
        swingOb.transform.LookAt(ShootGrapple2.grapplePositions[ShootGrapple2.grapplePositions.Count - 2]);
        /*if((swingOb.transform.eulerAngles.y == 270) && (i >0.0) && a == false){
		Debug.Log(270);
		a = true;
		i = -0.2;
	}*/
        if (swingOb.transform.eulerAngles.y == 270)
        {

            {
                int _22 = 180;
                Vector3 _23 = swingOb.transform.eulerAngles;
                _23.z = _22;
                swingOb.transform.eulerAngles = _23;
            }
        }
        /*if((swingOb.transform.eulerAngles.y == 270) && (i<0.0)&& a == false){
		Debug.Log(2700);
		a = true;
		i = 0.2;
	}*/
        Debug.Log(i);
        Debug.Log(this.GetComponent<Rigidbody>().velocity.x);
        swingOb.transform.Translate(0, 0, distance);
        swingOb.transform.Translate(0, i, 0);
        Gtrigger.swingObs.Add(swingOb.transform.position);
        Physics.IgnoreCollision(swingOb.GetComponent<Collider>(), this.player.GetComponent<Collider>());
        Physics.IgnoreCollision(swingOb.GetComponent<Collider>(), this.drum.GetComponent<Collider>());
        a = false;
    }

    static Gtrigger()
    {
        Gtrigger.swingObs = new System.Collections.Generic.List<Vector3>();
        Gtrigger.hitObs = new System.Collections.Generic.List<GameObject>();
        Gtrigger.compareAngles = new System.Collections.Generic.List<Vector3>();
    }

}