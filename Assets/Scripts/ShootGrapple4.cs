using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System;

[System.Serializable]
public partial class ShootGrapple4 : MonoBehaviour
{
    public GameObject prefabGrapple;
    public float range;
    public int distanceFromCamera;
    public static GameObject player;
    public float grappleJumpUp;
    public float grappleJumpForward;
    public float rayCutOffLength;
    public static Transform grappleSpawnPoint;
    public int i;
    public static System.Collections.Generic.List<Vector3> grapplePositions;
    public static System.Collections.Generic.List<swingDirE> swingDirs; //var myList = new List.<direction>();
    public static float angle;
    public static bool hasShot;
    public int f;
    public static int finalAngle;
    public static int swingDir;
    private swingDirE swingDir2;
    private Vector3 prevGrapPos;
    private Vector3 compareAngle;
    public int count;
    public bool farAway;
    public GameObject gTrigger;
    private GameObject InstanceGTrigger;
    private GameObject InstanceGrapple;
    private object coroutineStarted;
    public virtual void Awake()
    {
        if (!ShootGrapple4.player)
        {
            ShootGrapple4.player = GameObject.FindWithTag("Player");
        }
        if (!ShootGrapple4.grappleSpawnPoint)
        {
            ShootGrapple4.grappleSpawnPoint = ShootGrapple4.player.transform;
        }
    }

    /*if(Input.GetButtonDown("Jump") && Movement.hasShot
	&& Physics.Raycast(transform.position, -Vector3.up, player.collider.bounds.extents.y + 0.1) == false){
		disableGrapple();
		player.rigidbody.AddForce(Vector3.up * grappleJumpUp);
		if(player.rigidbody.velocity.x > 0)player.rigidbody.AddForce(Vector3.right * grappleJumpForward);
		if(player.rigidbody.velocity.x < 0)player.rigidbody.AddForce(Vector3.left * grappleJumpForward);
	}*/    public virtual void Update()//StartCoroutine("printPos");
    {
        RaycastHit hit = default(RaycastHit);
         //on mouse click ...
        if ((Input.GetButtonDown("Fire1") && (ShootGrapple4.hasShot == false)) && Movement.rotated)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = this.distanceFromCamera;
            Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            if (Physics.Raycast(this.transform.position, worldMousePosition - this.transform.position, out hit, this.range))
            {
                if (hit.collider.tag != "gTrigger")
                {
                    //var InstanceGrapple = Instantiate(prefabGrapple, hit.point, Quaternion.identity);
                    this.InstanceGrapple = UnityEngine.Object.Instantiate(this.prefabGrapple, ShootGrapple4.grappleSpawnPoint.position, Quaternion.identity);
                    ConfigurableJoint joint = (ConfigurableJoint) this.InstanceGrapple.GetComponent(typeof(ConfigurableJoint));
                    float distance = Vector3.Distance(ShootGrapple4.player.transform.position, hit.point);
                    joint.connectedBody = ShootGrapple4.player.GetComponent<Rigidbody>();

                    {
                        float _64 = distance;
                        SoftJointLimit _65 = joint.linearLimit;
                        _65.limit = _64;
                        joint.linearLimit = _65;
                    }
                    joint.xMotion = ConfigurableJointMotion.Limited;
                    joint.yMotion = ConfigurableJointMotion.Limited;
                    if (hit.collider.tag == "Grappable")
                    {
                        this.InstanceGrapple.transform.position = hit.point;
                        Vector3 gTriggerSP = (this.InstanceGrapple.transform.position + ShootGrapple4.player.transform.position) / 2;
                        this.InstanceGTrigger = UnityEngine.Object.Instantiate(this.gTrigger, gTriggerSP, Quaternion.LookRotation(this.InstanceGrapple.transform.position));
                    }
                    if ((hit.collider.transform.rotation.x == 0.5f) && (hit.collider.tag != "Enemy"))
                    {
                        ShootGrapple4.hasShot = true;
                        Movement.lerp = true;
                    }
                    else
                    {
                        if (hit.collider.tag == "Enemy")
                        {

                            {
                                float _66 = GameObject.Find("Enemy").transform.position.x;
                                Vector3 _67 = this.InstanceGrapple.transform.position;
                                _67.x = _66;
                                this.InstanceGrapple.transform.position = _67;
                            }

                            {
                                float _68 = GameObject.Find("Enemy").transform.position.y;
                                Vector3 _69 = this.InstanceGrapple.transform.position;
                                _69.y = _68;
                                this.InstanceGrapple.transform.position = _69;
                            }
                            //EnemyMovement.lerp = true;
                            //EnemyMovement.allowMovement = false;
                            hit.collider.gameObject.SendMessage("LerpTrue");
                            this.InstanceGrapple.GetComponent<Rigidbody>().constraints = this.InstanceGrapple.GetComponent<Rigidbody>().constraints & ~RigidbodyConstraints.FreezePositionX;
                        }
                        else
                        {
                            //grapplePositions.Add(GameObject.FindWithTag("Grapple").transform.position);
                            //player.rigidbody.constraints &= ~RigidbodyConstraints.FreezeRotationZ;
                            ShootGrapple4.hasShot = true;
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
            if (Input.GetButtonDown("Fire1") && ShootGrapple4.hasShot)
            {
                if (EnemyMovement.lerp)
                {
                    EnemyMovement.lerp = false;
                }
                UnityEngine.Object.Destroy(this.InstanceGTrigger);
                this.disableGrapple();
            }
        }
        if (ShootGrapple4.hasShot == true)
        {

        try
            {
                this.InstanceGTrigger.transform.position = (this.InstanceGrapple.transform.position + ShootGrapple4.player.transform.position) / 2;
                this.InstanceGTrigger.transform.Translate(0, 0.15f, 0);
                this.InstanceGTrigger.transform.LookAt(this.InstanceGrapple.transform.position);
                this.InstanceGTrigger.transform.Rotate(90, 0, 0);
                ((CapsuleCollider) this.InstanceGTrigger.GetComponent(typeof(CapsuleCollider))).height = Vector3.Distance(this.InstanceGrapple.transform.position, ShootGrapple4.player.transform.position) - 0.5f;
            }
           catch
            {
                return;
            }
        }
        if (Input.GetKey("y"))
        {
            Debug.Log(ShootGrapple4.grapplePositions.Count);
            int i = 0;
            while (i < ShootGrapple4.grapplePositions.Count)
            {
                Debug.Log((ShootGrapple4.grapplePositions[i] + " ") + ShootGrapple4.swingDirs[i]);
                i++;
            }
        }
    }

    public virtual IEnumerator CheckAngles()
    {
        while (Movement.hasShot)
        {
            this.i = ShootGrapple4.swingDirs.Count;
            int lastElementIdx = ShootGrapple4.swingDirs.Count - 1;
            try
            {
                this.swingDir2 = ShootGrapple4.swingDirs[this.i - 1];
                this.prevGrapPos = ShootGrapple4.grapplePositions[this.i - 1];
                this.compareAngle = Gtrigger.compareAngles[this.i - 1];
            }
            catch(Exception e)
            {
                Debug.Log(e.Message);
                this.StopCoroutine("CheckAngles");
                this.coroutineStarted = false;
                yield break;
            }
            ShootGrapple4.angle = Vector3.Angle(this.compareAngle, this.prevGrapPos - GameObject.FindWithTag("Grapple").transform.position);
            /*var cross = Vector3.Cross(prevGrapPos - GameObject.FindWithTag("Grapple").transform.position, Vector3.forward);
		var dotProduct = Vector3.Dot(cross, GameObject.FindWithTag("Grapple").transform.position - player.transform.position);*/
            //Debug.Log(dotProduct);
            //var dotProduct1 = Vector3.Dot(cross, GameObject.FindWithTag("Grapple").transform.position - prevGrapPos);
            //Debug.Log(dotProduct);
            //if(dotProduct < -0.1)
            Debug.Log(this.compareAngle);
            Debug.Log(ShootGrapple4.angle);
            Debug.Log(ShootGrapple4.finalAngle);
            ShootGrapple4.finalAngle = (int) Vector3.Angle(this.compareAngle, GameObject.FindWithTag("Grapple").transform.position - ShootGrapple4.player.transform.position);
            if ((this.swingDir2 == swingDirE.LEFT) && (ShootGrapple4.finalAngle > ShootGrapple4.angle)) //player swinging left
            {
                this.DoStuff();
                if (lastElementIdx == 0)
                {
                    //Debug.Log(frame);
                    this.coroutineStarted = false;
                    //Debug.Log("stopped");
                    break;
                }
            }
            if ((this.swingDir2 == swingDirE.RIGHT) && (ShootGrapple4.finalAngle < ShootGrapple4.angle)) //player swinging right
            {
                //Gtrigger.hitObs[lastElementIdx].tag = "Grappable";
                //if(Gtrigger.hitObs[lastElementIdx].transform.parent)Gtrigger.hitObs[lastElementIdx].transform.parent.tag = "Grappable";
                //Gtrigger.hitObs[lastElementIdx].layer = LayerMask.NameToLayer("hasNotHit");
                //if(Gtrigger.hitObs[lastElementIdx].transform.parent)Gtrigger.hitObs[lastElementIdx].transform.parent.gameObject.layer = LayerMask.NameToLayer("Default");
                this.DoStuff();
                if (lastElementIdx == 0)
                {
                    //Debug.Log(frame);
                    this.coroutineStarted = false;
                    //Debug.Log("stopped");
                    break;
                }
            }
            /*if(swingDir2 == swingDirE.LEFT && finalAngle > angle + 4){
			//EditorApplication.isPaused = true;
			i -= 1;
			DoStuff();
			//done = true;
			//yield;			
			//StopCoroutine("RaycastCheck2");
		}
		if(swingDir2 == swingDirE.RIGHT && finalAngle < angle - 4){
			i -= 1;
			DoStuff();

			//if(grapplePositions.Count)	
			//yield;
			//StopCoroutine("RaycastCheck2");		
		}
		if(swingDir2 == swingDirE.UP && finalAngle > angle - 4){
			i -= 1;
			DoStuff();
		}*/
            GameObject.FindWithTag("GrapGun").GetComponent<DrawGrappleLine>().getLinePositions();
            yield return null;
        }
    }

    public virtual void DoStuff()
    {
        try 
        {
            //Gtrigger.hitObs[i].tag = "Untagged";
            //Gtrigger.hitObs.RemoveAt(i);
            this.moveGrapple(this.prevGrapPos);
            ShootGrapple4.grapplePositions.RemoveAt(this.i);
            ShootGrapple4.swingDirs.RemoveAt(this.i);
            Gtrigger.compareAngles.RemoveAt(this.i);
            Gtrigger.i = 0;
        }
      catch(Exception )
        {
            //Gtrigger.hitObs[i].tag = "Untagged";
            //Gtrigger.hitObs.RemoveAt(i);
            /*moveGrapple(prevGrapPos);
		grapplePositions.RemoveAt(i);
		swingDirs.RemoveAt(i);
		Gtrigger.compareAngles.RemoveAt(i);*/
            this.StopCoroutine("CheckAngles");
            this.coroutineStarted = false;
            Gtrigger.i = 0;
            return;
        }
    }

    //create an object to swing into
     //if(swingDir == 1)
     //swingOb.transform.Translate(-0.1,0,0);
     //if(swingDir == 2)
     //swingOb.transform.Translate(0.1,0,0);
    public virtual IEnumerator printPos()
    {
        foreach (swingDirE swingDir in ShootGrapple4.swingDirs)
        {
            Debug.Log((object) swingDir);
            Debug.Log(ShootGrapple4.swingDirs.Count);
            yield return null;
        }
        foreach (Vector3 grapPos in ShootGrapple4.grapplePositions)
        {
            Debug.Log(ShootGrapple4.grapplePositions.Count);
            Debug.Log(grapPos);
            yield return null;
        }
    }

    public virtual void disableGrapple()
    {
        //StopCoroutine("RaycastCheck");
        ShootGrapple4.grapplePositions.Clear();
        ShootGrapple4.swingDirs.Clear();
        this.i = 0;
        DrawGrappleLine.i = 2;
        DrawGrappleLine.n = 0;
        Movement.hasShot = false;
        ShootGrapple4.hasShot = false;
        UnityEngine.Object.Destroy(GameObject.FindWithTag("Grapple"));
        UnityEngine.Object.Destroy((ConfigurableJoint) ShootGrapple4.player.GetComponent(typeof(ConfigurableJoint)));
        ShootGrapple4.player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        ShootGrapple4.player.GetComponent<Rigidbody>().constraints = ShootGrapple4.player.GetComponent<Rigidbody>().constraints & ~RigidbodyConstraints.FreezePositionX;
        ShootGrapple4.player.GetComponent<Rigidbody>().constraints = ShootGrapple4.player.GetComponent<Rigidbody>().constraints & ~RigidbodyConstraints.FreezePositionY;
        ShootGrapple4.player.GetComponent<Rigidbody>().useGravity = true;
    }

    public virtual void moveGrapple(Vector3 movePos)
    {
        float distance = Vector3.Distance(ShootGrapple4.player.transform.position, movePos);
        GameObject.FindWithTag("Grapple").transform.position = ShootGrapple4.grappleSpawnPoint.position;

        {
            float _70 = distance;
            SoftJointLimit _71 = ((ConfigurableJoint) GameObject.FindWithTag("Grapple").GetComponent(typeof(ConfigurableJoint))).linearLimit;
            _71.limit = _70;
            ((ConfigurableJoint) GameObject.FindWithTag("Grapple").GetComponent(typeof(ConfigurableJoint))).linearLimit = _71;
        }
        GameObject.FindWithTag("Grapple").transform.position = movePos;
        if (this.coroutineStarted == null)
        {
            this.StartCoroutine("CheckAngles");
            this.coroutineStarted = true;
        }
    }

    public ShootGrapple4()
    {
        this.range = 100f;
        this.distanceFromCamera = 10;
        this.grappleJumpUp = 200f;
        this.grappleJumpForward = 200f;
        this.rayCutOffLength = 0.2f;
    }

    static ShootGrapple4()
    {
        ShootGrapple4.grapplePositions = new List<Vector3>();
        ShootGrapple4.swingDirs = new List<swingDirE>();
    }

}