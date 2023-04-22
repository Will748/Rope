using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System;

[System.Serializable]
public partial class ShootGrapple2 : MonoBehaviour
{
    public GameObject prefabGrapple;
    public float range;
    public int distanceFromCamera;
    public static GameObject player;
    public float grappleJumpUp;
    public float grappleJumpForward;
    public float rayCutOffLength;
    public static Transform grappleSpawnPoint;
    public GameObject prober;
    public int i;
    public static System.Collections.Generic.List<Vector3> grapplePositions;
    public static System.Collections.Generic.List<swingDirE> swingDirs; //var myList = new List.<direction>();
    public static float angle;
    public static bool hasShot;
    public int f;
    //public static int finalAngle;
    public static int swingDir;
    private swingDirE swingDir2;
    private Vector3 prevGrapPos;
    private Vector3 compareAngle;
    public int count;
    public bool farAway;
    public GameObject gTrigger;
    private GameObject InstanceGTrigger;
    private GameObject InstanceGrapple;
    private DrawGrappleLine lineScript;
    private bool coroutineStarted;

    int last = 0;
    int first = 0;

    public virtual void Awake()
    {
        if (!ShootGrapple2.player)
        {
            ShootGrapple2.player = GameObject.FindWithTag("Player");
        }
        if (!ShootGrapple2.grappleSpawnPoint)
        {
            ShootGrapple2.grappleSpawnPoint = ShootGrapple2.player.transform;
        }
        coroutineStarted = false;

    }
    /*if(Input.GetButtonDown("Jump") && Movement.hasShot
	&& Physics.Raycast(transform.position, -Vector3.up, player.collider.bounds.extents.y + 0.1) == false){
		disableGrapple();
		player.rigidbody.AddForce(Vector3.up * grappleJumpUp);
		if(player.rigidbody.velocity.x > 0)player.rigidbody.AddForce(Vector3.right * grappleJumpForward);
		if(player.rigidbody.velocity.x < 0)player.rigidbody.AddForce(Vector3.left * grappleJumpForward);
	}*/    public virtual void FixedUpdate()//StartCoroutine("printPos");
    {
        RaycastHit hit = default(RaycastHit);
         //on mouse click ...
        if ((Input.GetButtonDown("Fire1") && (ShootGrapple2.hasShot == false)) && Movement.rotated)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = this.distanceFromCamera;
            Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            if (Physics.Raycast(this.transform.position, worldMousePosition - this.transform.position, out hit, this.range))
            {
                if (hit.collider.tag != "gTrigger")
                {
                    //var InstanceGrapple = Instantiate(prefabGrapple, hit.point, Quaternion.identity);
                    this.InstanceGrapple = UnityEngine.Object.Instantiate(this.prefabGrapple, ShootGrapple2.grappleSpawnPoint.position, Quaternion.identity);
                    ConfigurableJoint joint = (ConfigurableJoint) this.InstanceGrapple.GetComponent(typeof(ConfigurableJoint));
                    Vector3 moveDownABit = new Vector3(hit.point.x, hit.point.y - 0.05f, 0);
                    float distance = Vector3.Distance(ShootGrapple2.player.transform.position, moveDownABit);
                    joint.connectedBody = ShootGrapple2.player.GetComponent<Rigidbody>();

                    {
                        float _48 = distance;
                        SoftJointLimit _49 = joint.linearLimit;
                        _49.limit = _48;
                        joint.linearLimit = _49;
                    }
                    joint.xMotion = ConfigurableJointMotion.Limited;
                    joint.yMotion = ConfigurableJointMotion.Limited;
                    if (hit.collider.tag == "Grappable")
                    {
                        Vector3 dir = hit.point - hit.transform.position;
                        //GameObject prob = Instantiate(prober, hit.point, Quaternion.identity);
                        //prob.GetComponent<Prober>().Initialize(dir);
                        //yield return new WaitForSeconds(0.1f);                                                      //give time to wait for prober to 
                        Vector3 moveTo = hit.transform.position + (dir.normalized * (dir.magnitude + 0.01f));
                        this.InstanceGrapple.transform.position = moveDownABit;
                        //InstanceGrapple.transform.position = hit.point;
                        Vector3 gTriggerSP = (this.InstanceGrapple.transform.position + ShootGrapple2.player.transform.position) / 2;
                        this.InstanceGTrigger = UnityEngine.Object.Instantiate(this.gTrigger, gTriggerSP, Quaternion.LookRotation(this.InstanceGrapple.transform.position));
                    }
                    if ((hit.collider.transform.rotation.x == 0.5f) && (hit.collider.tag != "Enemy"))
                    {
                        ShootGrapple2.hasShot = true;
                        Movement.lerp = true;
                    }
                    else if (hit.collider.tag == "Enemy")
                        {

                            {
                                float _50 = GameObject.Find("Enemy").transform.position.x;
                                Vector3 _51 = this.InstanceGrapple.transform.position;
                                _51.x = _50;
                                this.InstanceGrapple.transform.position = _51;
                            }

                            {
                                float _52 = GameObject.Find("Enemy").transform.position.y;
                                Vector3 _53 = this.InstanceGrapple.transform.position;
                                _53.y = _52;
                                this.InstanceGrapple.transform.position = _53;
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
                            ShootGrapple2.hasShot = true;
                            Movement.hasShot = true;
                        }
                    }
                }
            }
       
        else if (Input.GetButtonDown("Fire1") && ShootGrapple2.hasShot)
            {
                if (EnemyMovement.lerp)
                {
                    EnemyMovement.lerp = false;
                }
                UnityEngine.Object.Destroy(this.InstanceGTrigger);
                disableGrapple();
            }
        
        if (ShootGrapple2.hasShot == true)
        {

        try
            {
                first = last;
                if(first !=(int) (Vector3.Distance(this.InstanceGrapple.transform.position, ShootGrapple2.player.transform.position) - 0.5f))
                {
                    //Debug.Log(Time.frameCount);
                }

                this.InstanceGTrigger.transform.position = (this.InstanceGrapple.transform.position + ShootGrapple2.player.transform.position) / 2;
                this.InstanceGTrigger.transform.Translate(0, 0.15f, 0);
                this.InstanceGTrigger.transform.LookAt(this.InstanceGrapple.transform.position);
                this.InstanceGTrigger.transform.Rotate(90, 0, 0);
                ((CapsuleCollider) this.InstanceGTrigger.GetComponent(typeof(CapsuleCollider))).height = Vector3.Distance(this.InstanceGrapple.transform.position, ShootGrapple2.player.transform.position) - 0.5f;
                last =  (int)(Vector3.Distance(this.InstanceGrapple.transform.position, ShootGrapple2.player.transform.position) - 0.5f);
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
                return;
            }
        }
        if (Input.GetKey("y"))
        {
            Debug.Log(ShootGrapple2.grapplePositions.Count);
            int i = 0;
            while (i < ShootGrapple2.grapplePositions.Count)
            {
                Debug.Log((ShootGrapple2.grapplePositions[i] + " ") + ShootGrapple2.swingDirs[i]);
                i++;
            }
        }
    }

    float g = 0;
    bool setOrder = false;
    float largerAngle = 0f;
    float smallerAngle = 0f;
    float finalAngle = 0f;
    bool a_bigger_than_b = false;


    private float Test(float a, float b)
    {
        if (!setOrder)
        {
            if (a > b)
            {
                largerAngle = a;
                smallerAngle = b;
                a_bigger_than_b = true;
            }
            else
            {
                largerAngle = b;
                smallerAngle = a;
                a_bigger_than_b = false;
            }
            setOrder = true;
        }
        else if(a_bigger_than_b)
        {
            finalAngle = a - b;
            return finalAngle;
        }
        else
        {
            return finalAngle = b - a;
        }
        return 0f;
    }

    public virtual IEnumerator CheckAngles()
    {
        //public const float dfinalAngle;

        //float finalAngle;
        setOrder = false;
        //ref float largerAngle;
        //ref float smallerAngle;
        float currAngle = 0;
        float prevAngle = 0;
        float firstInstanceDotProduct = 0f; ;
        bool setfirstInstanceDotProduct = false;
        bool flip = false;
        while (Movement.hasShot)
        {
            this.i = ShootGrapple2.swingDirs.Count;
            int lastElementIdx = ShootGrapple2.swingDirs.Count - 1;
            try
            {
                this.swingDir2 = ShootGrapple2.swingDirs[this.i - 1];
                this.prevGrapPos = ShootGrapple2.grapplePositions[this.i - 1];
                this.compareAngle = Gtrigger.compareAngles[this.i - 1];
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
                this.StopCoroutine("CheckAngles");
                this.coroutineStarted = false;
                yield break;
            }
            /*angle = Vector3.Angle(compareAngle, 
		prevGrapPos - GameObject.FindWithTag("Grapple").transform.position);*/
            Vector3 cross = Vector3.Cross(this.prevGrapPos - GameObject.FindWithTag("Grapple").transform.position, Vector3.forward);
            float dotProduct = Vector3.Dot(cross, GameObject.FindWithTag("Grapple").transform.position - ShootGrapple2.player.transform.position);
            //Debug.Log(dotProduct);
            prevAngle = InstanceGTrigger.GetComponent<Gtrigger>().CalculateAngle(prevGrapPos, GameObject.FindWithTag("Grapple").transform.position);
            currAngle = InstanceGTrigger.GetComponent<Gtrigger>().CalculateAngle(GameObject.FindWithTag("Grapple").transform.position, GameObject.FindWithTag("GrapGun").transform.position);

            //Debug.Log(prevAngle);// + " "+ prevGrapPos + " " + GameObject.FindWithTag("Grapple").transform.position);
            //Debug.Log(currAngle);// + " " + prevGrapPos + " " + GameObject.FindWithTag("GrapGun").transform.position);
            //float finalAngle;
            //float finalAngle = Test(prevAngle, currAngle);
            Debug.Log(dotProduct);
            if (!setfirstInstanceDotProduct) {
                setfirstInstanceDotProduct = true;
                firstInstanceDotProduct = dotProduct;
                Debug.Log("h");
            }
            if(firstInstanceDotProduct < 0)
            {
                flip = true;
            }
            if (flip)
            {
                dotProduct = dotProduct * -1;
            }
            Debug.Log(dotProduct);

            if (swingDir2 == swingDirE.LEFT) finalAngle = prevAngle - currAngle;
            else finalAngle = currAngle - prevAngle;
            //if (swingDir2 == swingDirE.LEFT) dotProduct = Math.Abs(dotProduct) + Math.Abs(dotProduct);
            /*if (!setOrder)
            {
                if (prevAngle > currAngle)
                {
                    largerAngle = prevAngle;
                    smallerAngle = currAngle;
                }
                else
                {
                    //largerAngle = currAngle;
                    //smallerAngle = prevAngle;
                    largerAngle = currAngle;
                    smallerAngle = prevAngle;
                }
                setOrder = true;
            }*/
            //finalAngle = largerAngle - smallerAngle;
            //Debug.Log(finalAngle);
            //finalAngle = largerAngle - smallerAngle;
            //float finalAngle = prevAngle - currAngle;
            //if (finalAngle < 0) finalAngle = -finalAngle;
            //Debug.Log(finalAngle);
            //if((prevAngle -currAngle < 0)
            //if (GameObject.FindWithTag("Player").transform.position.y > prevGrapPos.y) dotProduct = -dotProduct;
            //Debug.Log(dotProduct);
            //float dotProduct1 = Vector3.Dot(-cross.normalized, (GameObject.FindWithTag("Grapple").transform.position - ShootGrapple2.player.transform.position).normalized);
            //Debug.Log(dotProduct1);
            //var dotProduct1 = Vector3.Dot(cross, GameObject.FindWithTag("Grapple").transform.position - prevGrapPos);
            //Debug.Log(dotProduct);
            //if (dotProduct < -0.1f)
            //{
            //Debug.Log("U");
            //float b = InstanceGTrigger.GetComponent<Gtrigger>().TimeStepCount = 0;
            //Debug.Log(b);
            /*Debug.Log(compareAngle);
    Debug.Log(angle);
    Debug.Log(finalAngle);
    finalAngle = Vector3.Angle(compareAngle,
    GameObject.FindWithTag("Grapple").transform.position - player.transform.position);*/
            if ((this.swingDir2 == swingDirE.LEFT) && (dotProduct < 0)) //player swinging left
            {
                float b = InstanceGTrigger.GetComponent<Gtrigger>().TimeStepCount = 0;
                Debug.Log(b);
                this.DoStuff();
                if (lastElementIdx == 0)
                {
                    Debug.Log("L");
                    //Debug.Log(frame);
                    this.coroutineStarted = false;
                    //Debug.Log("stopped");
                    float a = InstanceGTrigger.GetComponent<Gtrigger>().TimeStepCount = 0;
                    a_bigger_than_b = false;
                    setOrder = false;
                    setfirstInstanceDotProduct = false;
                    flip = false;
                    Debug.Log(a);
                    break;
                }
                //}
            }
            if ((this.swingDir2 == swingDirE.RIGHT) && (dotProduct < 0)) //player swinging right
            {
                //Gtrigger.hitObs[lastElementIdx].tag = "Grappable";
                //if(Gtrigger.hitObs[lastElementIdx].transform.parent)Gtrigger.hitObs[lastElementIdx].transform.parent.tag = "Grappable";
                //Gtrigger.hitObs[lastElementIdx].layer = LayerMask.NameToLayer("hasNotHit");
                //if(Gtrigger.hitObs[lastElementIdx].transform.parent)Gtrigger.hitObs[lastElementIdx].transform.parent.gameObject.layer = LayerMask.NameToLayer("Default");
                float b = InstanceGTrigger.GetComponent<Gtrigger>().TimeStepCount = 0;

                Debug.Log(b);
                this.DoStuff();
                if (lastElementIdx == 0)
                {
                    Debug.Log("R");
                    //Debug.Log(frame);
                    this.coroutineStarted = false;
                    //Debug.Log("stopped");
                    InstanceGTrigger.GetComponent<Gtrigger>().TimeStepCount = 0;
                    float a = InstanceGTrigger.GetComponent<Gtrigger>().TimeStepCount = 0;
                    a_bigger_than_b = false;
                    setfirstInstanceDotProduct = false;
                    flip = false;
                    setOrder = false;
                    Debug.Log(a);
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
            //DrawGrappleLine.getLinePositions();
            yield return null;
        }
    }

    public virtual void DoStuff()
    {
        lineScript = GameObject.FindWithTag("Grapple").GetComponent<DrawGrappleLine>();
      
        try{
            //Gtrigger.hitObs[i].tag = "Untagged";
            //Gtrigger.hitObs.RemoveAt(i);
            this.moveGrapple(this.prevGrapPos);
            ShootGrapple2.grapplePositions.RemoveAt(this.i -1);
            ShootGrapple2.swingDirs.RemoveAt(this.i - 1);
            Gtrigger.compareAngles.RemoveAt(this.i - 1);
            Gtrigger.i = 0;
        }
       catch(Exception e)
        {
            Debug.Log(e.Message);
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
        finally
        {
            lineScript.getLinePositions();
            //Debug.Log("run");
        }
    }

    //create an object to swing into
     //if(swingDir == 1)
     //swingOb.transform.Translate(-0.1,0,0);
     //if(swingDir == 2)
     //swingOb.transform.Translate(0.1,0,0);
    public virtual IEnumerator printPos()
    {
        foreach (swingDirE swingDir in ShootGrapple2.swingDirs)
        {
            Debug.Log((object) swingDir);
            Debug.Log(ShootGrapple2.swingDirs.Count);
            yield return null;
        }
        foreach (Vector3 grapPos in ShootGrapple2.grapplePositions)
        {
            Debug.Log(ShootGrapple2.grapplePositions.Count);
            Debug.Log(grapPos);
            yield return null;
        }
    }

    public virtual void disableGrapple()
    {
        //StopCoroutine("RaycastCheck");
        ShootGrapple2.grapplePositions.Clear();
        ShootGrapple2.swingDirs.Clear();
        Gtrigger.compareAngles.Clear();
        Debug.Log("called");
        this.i = 0;
        DrawGrappleLine.i = 2;
        DrawGrappleLine.n = 0;
        Movement.hasShot = false;
        ShootGrapple2.hasShot = false;
        UnityEngine.Object.Destroy(GameObject.FindWithTag("Grapple"));
        UnityEngine.Object.Destroy((ConfigurableJoint) ShootGrapple2.player.GetComponent(typeof(ConfigurableJoint)));
        ShootGrapple2.player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        ShootGrapple2.player.GetComponent<Rigidbody>().constraints = ShootGrapple2.player.GetComponent<Rigidbody>().constraints & ~RigidbodyConstraints.FreezePositionX;
        ShootGrapple2.player.GetComponent<Rigidbody>().constraints = ShootGrapple2.player.GetComponent<Rigidbody>().constraints & ~RigidbodyConstraints.FreezePositionY;
        ShootGrapple2.player.GetComponent<Rigidbody>().useGravity = true;
    }

    public virtual void moveGrapple(Vector3 movePos)
    {
        float distance = Vector3.Distance(ShootGrapple2.player.transform.position, movePos);
        GameObject.FindWithTag("Grapple").transform.position = ShootGrapple2.grappleSpawnPoint.position;

        {
            float _54 = distance;
            SoftJointLimit _55 = ((ConfigurableJoint) GameObject.FindWithTag("Grapple").GetComponent(typeof(ConfigurableJoint))).linearLimit;
            _55.limit = _54;
            ((ConfigurableJoint) GameObject.FindWithTag("Grapple").GetComponent(typeof(ConfigurableJoint))).linearLimit = _55;
        }
        GameObject.FindWithTag("Grapple").transform.position = movePos;
        if (!coroutineStarted)
        {
            this.StartCoroutine("CheckAngles");
            this.coroutineStarted = true;
        }
    }

    public ShootGrapple2()
    {
        this.range = 100f;
        this.distanceFromCamera = 10;
        this.grappleJumpUp = 200f;
        this.grappleJumpForward = 200f;
        this.rayCutOffLength = 0.2f;
    }

    static ShootGrapple2()
    {
        ShootGrapple2.grapplePositions = new List<Vector3>();
        ShootGrapple2.swingDirs = new List<swingDirE>();
    }

}