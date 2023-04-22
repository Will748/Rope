using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System;

[System.Serializable]
public partial class ShootGrapple : MonoBehaviour
{
    public GameObject prefabGrapple;
    public float range;
    public int distanceFromCamera;
    public GameObject player;
    public float grappleJumpUp;
    public float grappleJumpForward;
    public float rayCutOffLength;
    public Transform grappleSpawnPoint;
    private int i;
    public static System.Collections.Generic.List<Vector3> grapplePositions;
    private System.Collections.Generic.List<int> swingDirs;
    private float angle;
    public static bool hasShot;
    public int f;
    public float finalAngle;
    public int swingDir;
    public int count;
    public bool farAway;
    public GameObject gTrigger;
    private GameObject InstanceGTrigger;
    private GameObject InstanceGrapple;
    private GameObject grapGun;

    bool done = false;
    int swingDir2 = 0;
    Vector3 prevGrapPos;

    public virtual void Awake()
    {
        if (!this.player)
        {
            this.player = GameObject.FindWithTag("Player");
        }
        if (!this.grappleSpawnPoint)
        {
            this.grappleSpawnPoint = this.player.transform;
        }
        grapGun = GameObject.Find("gunNull");
    }

    /*if(Input.GetButtonDown("Jump") && Movement.hasShot
	&& Physics.Raycast(transform.position, -Vector3.up, player.collider.bounds.extents.y + 0.1) == false){
		disableGrapple();
		player.rigidbody.AddForce(Vector3.up * grappleJumpUp);
		if(player.rigidbody.velocity.x > 0)player.rigidbody.AddForce(Vector3.right * grappleJumpForward);
		if(player.rigidbody.velocity.x < 0)player.rigidbody.AddForce(Vector3.left * grappleJumpForward);
	}*/    public virtual void Update()
    {
        RaycastHit hit = default(RaycastHit);
         //on mouse click ...
        if ((Input.GetButtonDown("Fire1") && (ShootGrapple.hasShot == false)) && Movement.rotated)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = this.distanceFromCamera;
            Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            if (Physics.Raycast(this.transform.position, worldMousePosition - this.transform.position, out hit, this.range))
            {
                if (hit.collider.tag != "gTrigger")
                {
                    //var InstanceGrapple = Instantiate(prefabGrapple, hit.point, Quaternion.identity);
                    this.InstanceGrapple = UnityEngine.Object.Instantiate(this.prefabGrapple, this.grappleSpawnPoint.position, Quaternion.identity);
                    ConfigurableJoint joint = (ConfigurableJoint) this.InstanceGrapple.GetComponent(typeof(ConfigurableJoint));
                    float distance = Vector3.Distance(this.player.transform.position, hit.point);
                    joint.connectedBody = this.player.GetComponent<Rigidbody>();

                    {
                        float _40 = distance;
                        SoftJointLimit _41 = joint.linearLimit;
                        _41.limit = _40;
                        joint.linearLimit = _41;
                    }
                    joint.xMotion = ConfigurableJointMotion.Limited;
                    joint.yMotion = ConfigurableJointMotion.Limited;
                    this.InstanceGrapple.transform.position = hit.point;
                    //var gTriggerSP = (InstanceGrapple.transform.position + player.transform.position)/2;
                    //InstanceGTrigger = Instantiate(gTrigger, gTriggerSP, Quaternion.LookRotation(InstanceGrapple.transform.position));  
                    if (hit.collider.transform.rotation.x == 0.5f)
                    {
                        Debug.Log("2");
                        ShootGrapple.hasShot = true;
                        Movement.lerp = true;
                    }
                    else
                    {
                        if (hit.collider.gameObject.tag == "Enemy")
                        {

                            {
                                float _42 = GameObject.Find("Enemy").transform.position.x;
                                Vector3 _43 = this.InstanceGrapple.transform.position;
                                _43.x = _42;
                                this.InstanceGrapple.transform.position = _43;
                            }

                            {
                                float _44 = GameObject.Find("Enemy").transform.position.y;
                                Vector3 _45 = this.InstanceGrapple.transform.position;
                                _45.y = _44;
                                this.InstanceGrapple.transform.position = _45;
                            }
                            EnemyMovement.lerp = true;
                            EnemyMovement.allowMovement = false;
                            this.InstanceGrapple.GetComponent<Rigidbody>().constraints = this.InstanceGrapple.GetComponent<Rigidbody>().constraints & ~RigidbodyConstraints.FreezePositionX;
                        }
                        else
                        {
                            //player.rigidbody.constraints &= ~RigidbodyConstraints.FreezeRotationZ;
                            ShootGrapple.hasShot = true;
                            Movement.hasShot = true;
                        }
                    }
                }
            }
        }
        else
        {
            //StartCoroutine("Raycastcheck");
            //InvokeRepeating("Raycastcheck",0.0,0.1);
            if (Input.GetButtonDown("Fire1") && ShootGrapple.hasShot)
            {
                if (EnemyMovement.lerp)
                {
                    EnemyMovement.lerp = false;
                }
                UnityEngine.Object.Destroy(this.InstanceGTrigger);
                this.disableGrapple();
            }
        }
        /*if(hasShot == true){
		InstanceGTrigger.transform.position = (InstanceGrapple.transform.position + player.transform.position)/2;
		InstanceGTrigger.transform.Translate(0,0.15,0);
		InstanceGTrigger.transform.LookAt(InstanceGrapple.transform.position);
		InstanceGTrigger.transform.Rotate(90,0,0);
		InstanceGTrigger.GetComponent(CapsuleCollider).height = Vector3.Distance(InstanceGrapple.transform.position, player.transform.position) - 0.5;
	}*/
        if (Input.GetKey("y"))
        {
            this.StartCoroutine("printPos");
        }
    }

    public virtual void FixedUpdate()//StopCoroutine("RaycastCheck2");		
    {
        RaycastHit hit = default(RaycastHit);
        //var count = 0;
        if (Movement.hasShot)
        {
            //Debug.Log("running");
            this.count++;
            float rayRange = Vector3.Distance(GameObject.FindWithTag("Grapple").transform.position, this.player.transform.position);
            rayRange = rayRange - this.rayCutOffLength;
            if (Vector3.Distance(hit.point, GameObject.FindWithTag("Grapple").transform.position) > 1)
            {
                this.farAway = true;
            }
            else
            {
                this.farAway = false;
            }
            Vector3 rsp = GameObject.FindWithTag("Grapple").transform.position;
            //rsp.y -=0.2;
            if (((((this.count >= 10) && Physics.Raycast(rsp, this.player.transform.position - GameObject.FindWithTag("Grapple").transform.position, out hit, rayRange)) && (hit.collider.tag != "Player")) && (hit.collider.tag != "Drum")) && this.farAway)/*Physics.Linecast(player.transform.position, 
	    GameObject.FindWithTag("Grapple").transform.position -Vector3(0.2,0.2,0),hit)*/
            {
                Debug.Log("hit");
                //hit.collider.tag = "used";	    
                if (this.player.GetComponent<Rigidbody>().velocity.x < 0)
                {
                    this.swingDir = 1;
                }
                if (this.player.GetComponent<Rigidbody>().velocity.x > 0)
                {
                    this.swingDir = 2;
                }
                ShootGrapple.grapplePositions.Add(GameObject.FindWithTag("Grapple").transform.position);
                this.swingDirs.Add(this.swingDir);
                this.i = this.i + 1;
                this.f = this.f + 1;
                done = true;
                //var angle = Vector3.Angle(Vector3.right,
                //GameObject.FindWithTag("Grapple").transform.position - hit.point);
                this.moveGrapple(hit.point);
                grapGun.GetComponent<DrawGrappleLine>().getLinePositions();
            }
            // Node type not supported yet 
            try
            {
                swingDir2 = swingDirs[i - 1];
                prevGrapPos = ShootGrapple.grapplePositions[i - 1];
            }
            catch (Exception) {
                //except e as System.Exception:
                return;
            }

            //@F:\previousWindows 10\Windows8\Users\Will\Documents\Rope\Assets\Scripts\ShootGrapple.js(148,9)
            if(done == true)
            {
                this.angle = Vector3.Angle(Vector3.right, prevGrapPos - GameObject.FindWithTag("Grapple").transform.position);
                this.finalAngle = Vector3.Angle(Vector3.right, GameObject.FindWithTag("Grapple").transform.position - this.player.transform.position);
                if ((swingDir2 == 1) && (this.finalAngle > (this.angle + 4)))
                {
                    this.i = this.i - 1;
                    try {
                        moveGrapple(prevGrapPos);

                        grapplePositions.RemoveAt(i);

                        swingDirs.RemoveAt(i);
                        }
                    catch (Exception) {
                        StopCoroutine("RaycastCheck2");

                        return;
                    }

                    grapGun.GetComponent<DrawGrappleLine>().getLinePositions();
                }
                if (swingDir2 == 2 && finalAngle < angle - 4)
                    {
                    try
                    {
                        this.moveGrapple(prevGrapPos);
                        ShootGrapple.grapplePositions.RemoveAt(this.i);
                        this.swingDirs.RemoveAt(this.i);
                    }
                    catch (Exception)
                    {
                        return;
                    }
                    grapGun.GetComponent<DrawGrappleLine>().getLinePositions();
                }
                //done = true;
                //yield;			
                //StopCoroutine("RaycastCheck2");
                if ((swingDir2 == 2) && (this.finalAngle < (this.angle - 4)))
                {
                    try
                    {
                        this.i = this.i - 1;
                        this.moveGrapple(prevGrapPos);
                        ShootGrapple.grapplePositions.RemoveAt(this.i);
                        this.swingDirs.RemoveAt(this.i);
                    }

                    catch (Exception)
                    {
                        return;
                    }
                    grapGun.GetComponent<DrawGrappleLine>().getLinePositions();
                }
            }
        }
    }

    public virtual IEnumerator printPos()
    {
        foreach (int swingDir in this.swingDirs)
        {
            Debug.Log(swingDir);
            Debug.Log(this.swingDirs.Count);
            yield return null;
        }
        foreach (Vector3 grapPos in ShootGrapple.grapplePositions)
        {
            Debug.Log(ShootGrapple.grapplePositions.Count);
            Debug.Log(grapPos);
            yield return null;
        }
    }

    public virtual void disableGrapple()
    {
        this.StopCoroutine("RaycastCheck");
        ShootGrapple.grapplePositions.Clear();
        this.swingDirs.Clear();
        this.i = 0;
        DrawGrappleLine.i = 2;
        DrawGrappleLine.n = 0;
        Movement.hasShot = false;
        ShootGrapple.hasShot = false;
        UnityEngine.Object.Destroy(GameObject.FindWithTag("Grapple"));
        UnityEngine.Object.Destroy((ConfigurableJoint) this.player.GetComponent(typeof(ConfigurableJoint)));
        this.player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        this.player.GetComponent<Rigidbody>().constraints = this.player.GetComponent<Rigidbody>().constraints & ~RigidbodyConstraints.FreezePositionX;
        this.player.GetComponent<Rigidbody>().constraints = this.player.GetComponent<Rigidbody>().constraints & ~RigidbodyConstraints.FreezePositionY;
        this.player.GetComponent<Rigidbody>().useGravity = true;
    }

    public virtual IEnumerator Raycastcheck()
    {
        RaycastHit hit = default(RaycastHit);
        //var count = 0;
        while (Movement.hasShot)
        {
            Debug.Log("running");
            this.count++;
            float rayRange = Vector3.Distance(GameObject.FindWithTag("Grapple").transform.position, this.player.transform.position);
            rayRange = rayRange - this.rayCutOffLength;
            if (Vector3.Distance(hit.point, GameObject.FindWithTag("Grapple").transform.position) > 1)
            {
                this.farAway = true;
            }
            else
            {
                this.farAway = false;
            }
            Vector3 rsp = GameObject.FindWithTag("Grapple").transform.position;
            //rsp.y -=0.2;
            if (((((this.count >= 10) && Physics.Raycast(rsp, this.player.transform.position - GameObject.FindWithTag("Grapple").transform.position, out hit, rayRange)) && (hit.collider.tag != "Player")) && (hit.collider.tag != "Drum")) && this.farAway)/*Physics.Linecast(player.transform.position, 
	    GameObject.FindWithTag("Grapple").transform.position -Vector3(0.2,0.2,0),hit)*/
            {
                Debug.Log("hit");
                hit.collider.tag = "used";
                if (this.player.GetComponent<Rigidbody>().velocity.x < 0)
                {
                    this.swingDir = 1;
                }
                if (this.player.GetComponent<Rigidbody>().velocity.x > 0)
                {
                    this.swingDir = 2;
                }
                ShootGrapple.grapplePositions.Add(GameObject.FindWithTag("Grapple").transform.position);
                this.swingDirs.Add(this.swingDir);
                this.i = this.i + 1;
                this.f = this.f + 1;
                //var angle = Vector3.Angle(Vector3.right,
                //GameObject.FindWithTag("Grapple").transform.position - hit.point);
                this.moveGrapple(hit.point);
                grapGun.GetComponent<DrawGrappleLine>().getLinePositions();
                //done = false;	
                this.StartCoroutine("RaycastCheck2");
            }
            //yield WaitForSeconds(1);
            //StopCoroutine("Raycastcheck");
            //StopCoroutine("Raycastcheck");
            yield return null;
        }
    }

    public virtual IEnumerator RaycastCheck2()
    {
        while (Movement.hasShot)
        {

            try { 
	        swingDir2 = swingDirs[i - 1];
            prevGrapPos = ShootGrapple.grapplePositions[i - 1];
            }
            catch (Exception e) {
                Debug.Log(e.Message);

                StopCoroutine("RaycastCheck2");
                yield break;
            }
            this.angle = Vector3.Angle(Vector3.right, prevGrapPos - GameObject.FindWithTag("Grapple").transform.position);
            this.finalAngle = (int) Vector3.Angle(Vector3.right, GameObject.FindWithTag("Grapple").transform.position - this.player.transform.position);
            if ((swingDir2 == 1) && (this.finalAngle > (this.angle + 4)))
            {
                this.i = this.i - 1;

                try
                {
                    moveGrapple(prevGrapPos);

                    ShootGrapple.grapplePositions.RemoveAt(i);

                    swingDirs.RemoveAt(i);
                }
                catch (Exception) {
                    StopCoroutine("RaycastCheck2");
                    yield break;
                }
                grapGun.GetComponent<DrawGrappleLine>().getLinePositions();
            }
            //done = true;
            //yield;			
            //StopCoroutine("RaycastCheck2");
            if ((swingDir2 == 2) && (this.finalAngle < (this.angle - 4)))
            {
                try
                {
                    this.i = this.i - 1;
                    this.moveGrapple(prevGrapPos);
                    ShootGrapple.grapplePositions.RemoveAt(this.i);
                    this.swingDirs.RemoveAt(this.i);
                }
                catch(Exception)
                {
                    this.StopCoroutine("RaycastCheck2");
                    yield break;
                }
                grapGun.GetComponent<DrawGrappleLine>().getLinePositions();
            }
            //if(grapplePositions.Count)	
            //yield;
            //StopCoroutine("RaycastCheck2");		
            yield return null;
        }
    }

    public virtual void moveGrapple(Vector3 movePos)
    {
        float distance = Vector3.Distance(this.player.transform.position, movePos);
        GameObject.FindWithTag("Grapple").transform.position = this.grappleSpawnPoint.position;

        {
            float _46 = distance;
            SoftJointLimit _47 = ((ConfigurableJoint) GameObject.FindWithTag("Grapple").GetComponent(typeof(ConfigurableJoint))).linearLimit;
            _47.limit = _46;
            ((ConfigurableJoint) GameObject.FindWithTag("Grapple").GetComponent(typeof(ConfigurableJoint))).linearLimit = _47;
        }
        GameObject.FindWithTag("Grapple").transform.position = movePos;
    }

    public ShootGrapple()
    {
        this.range = 100f;
        this.distanceFromCamera = 10;
        this.grappleJumpUp = 200f;
        this.grappleJumpForward = 200f;
        this.rayCutOffLength = 0.2f;
        this.swingDirs = new List<int>();
    }

    static ShootGrapple()
    {
        ShootGrapple.grapplePositions = new List<Vector3>();
    }

}