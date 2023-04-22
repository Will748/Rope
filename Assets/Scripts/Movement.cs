using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class Movement : MonoBehaviour
{
    public float speed;
    public float rotateSpeed;
    public float jumpPower;
    public float pulleySpeed;
    public int force;
    public float lerpSpeed;
    public float dropOff;
    public float upforce;
    public float grappleJumpUp;
    public float grappleJumpForward;
    //var smooth = 20.0;
    //var smoothTime = 1.0;
    public static bool hasShot;
    public static bool lerp;
    public static bool rotated;
    private bool rotatedR;
    private bool rotatedL;
    //private var yVelocity = 0.0;
    public virtual void Update()
    {
        if (Movement.lerp)
        {
            Vector3 grapplePosition = GameObject.FindWithTag("Grapple").transform.position;

            {
                float _28 = this.transform.position.y + this.upforce;
                Vector3 _29 = this.transform.position;
                _29.y = _28;
                this.transform.position = _29;
            }
            this.transform.position = Vector3.MoveTowards(this.transform.position, grapplePosition, this.lerpSpeed);
            if (Vector3.Distance(this.transform.position, grapplePosition) < this.dropOff)
            {
                Movement.hasShot = false;
                ShootGrapple.hasShot = false;
                this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                this.GetComponent<Rigidbody>().constraints = this.GetComponent<Rigidbody>().constraints & ~RigidbodyConstraints.FreezePositionX;
                this.GetComponent<Rigidbody>().constraints = this.GetComponent<Rigidbody>().constraints & ~RigidbodyConstraints.FreezePositionY;
                UnityEngine.Object.Destroy(GameObject.FindWithTag("Grapple"));
                //transform.rotation = Quaternion.Euler(Vector3(0, 0, 0));		
                Movement.lerp = false;
            }
        }
        float h = Input.GetAxis("Horizontal");
        if ((h != 0f) && (Movement.hasShot == false /*&& Physics.Raycast(transform.position, -Vector3.up, collider.bounds.extents.y + 0.1)*/))
        {
            //rigidbody.velocity = Vector3(h  * speed, 0, 0)
            if ((h < 0) && (this.rotatedL == false))
            {
                Movement.rotated = false;
                this.StartCoroutine(this.Rotate(180));
            }
            if ((h > 0) && (this.rotatedR == false))
            {
                Movement.rotated = false;
                this.StartCoroutine(this.Rotate(0));
            }
            if (Movement.rotated)
            {
                this.transform.Translate(this.speed * Time.deltaTime, 0, 0);
            }
        }
        if ((Input.GetButtonDown("Jump") && Physics.Raycast(this.transform.position, -Vector3.up, this.GetComponent<Collider>().bounds.extents.y + 0.1f)) && !Movement.hasShot)
        {
            this.GetComponent<Rigidbody>().AddForce(Vector3.up * this.jumpPower);
        }
        if (Movement.hasShot)
        {
             //var grapplePositions = GameObject.FindWithTag("Grapple").transform.position;
             /*if(Input.GetButtonDown("Jump") && Physics.Raycast(transform.position, -Vector3.up, collider.bounds.extents.y + 0.1) == false){
			rigidbody.AddForce(Vector3.up * grappleJumpUp);
			if(rigidbody.velocity.x > 0)rigidbody.AddForce(Vector3.right * grappleJumpForward);
			if(rigidbody.velocity.x < 0)rigidbody.AddForce(Vector3.left * grappleJumpForward);
		}*/
            float v = Input.GetAxis("Vertical");
            if (v != 0f)
            {
                if (((ConfigurableJoint) GameObject.FindWithTag("Grapple").GetComponent(typeof(ConfigurableJoint))).linearLimit.limit >= ((v * this.pulleySpeed) * Time.deltaTime))
                {

                    {
                        float _30 = ((ConfigurableJoint) GameObject.FindWithTag("Grapple").GetComponent(typeof(ConfigurableJoint))).linearLimit.limit - ((v * this.pulleySpeed) * Time.deltaTime);
                        SoftJointLimit _31 = ((ConfigurableJoint) GameObject.FindWithTag("Grapple").GetComponent(typeof(ConfigurableJoint))).linearLimit;
                        _31.limit = _30;
                        ((ConfigurableJoint) GameObject.FindWithTag("Grapple").GetComponent(typeof(ConfigurableJoint))).linearLimit = _31;
                    }
                }
            }
        }
    }

    public virtual void FixedUpdate()
    {
        if (Movement.hasShot)
        {
            if (Input.GetKey("a"))
            {
                this.GetComponent<Rigidbody>().AddForce(Vector3.right * -this.force);
            }
            if (Input.GetKey("d"))
            {
                this.GetComponent<Rigidbody>().AddForce(Vector3.right * this.force);
            }
        }
    }

    public virtual IEnumerator Rotate(float rotAmount)
    {
         /*var oldRotation = transform.rotation; 
   	transform.Rotate(0,-180,0); 
   	var newRotation = transform.rotation; 
   	for (var t = 0.0; t < 1.0; t += Time.deltaTime * rotateSpeed) 
   	{ 
		transform.rotation = Quaternion.Slerp(oldRotation, newRotation, t); 
       	Debug.Log(t);
       	yield; 
	}
	transform.rotation = newRotation;
	switch(rotAmount){
	case(1):
		Debug.Log("working");
		rotatedR = false;
		rotatedL = true;
		rotated = true;
	break;	
	case(2):
		Debug.Log("workingtoo");
		rotatedL = false;
		rotatedR = true;
		rotated = true;
	break;
	}*/
        if (rotAmount == 180)
        {
            while (this.transform.rotation.y < 0.9f)
            {
                this.transform.Rotate(0, Time.deltaTime * this.rotateSpeed, 0);
                //transform.eulerAngles.y = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotAmount, yVelocity, smoothTime, smooth);
                Movement.rotated = false;
                yield return null;
            }

            {
                float _32 = //transform.eulerAngles.y = Mathf.SmoothDampAngle(transform.eulerAngles, rotAmount, smoothTime, smooth);
                rotAmount;
                Vector3 _33 = this.transform.eulerAngles;
                _33.y = _32;
                this.transform.eulerAngles = _33;
            }
            Movement.rotated = true;
            this.rotatedL = true;
            this.rotatedR = false;
        }
        if (rotAmount == 0)
        {
            while (this.transform.rotation.y > 0)
            {
                this.transform.Rotate(0, Time.deltaTime * -this.rotateSpeed, 0);
                Movement.rotated = false;
                yield return null;
            }

            {
                float _34 = rotAmount;
                Vector3 _35 = this.transform.eulerAngles;
                _35.y = _34;
                this.transform.eulerAngles = _35;
            }

            {
                int _36 = 0;
                Vector3 _37 = this.transform.position;
                _37.z = _36;
                this.transform.position = _37;
            }
            Movement.rotated = true;
            this.rotatedR = true;
            this.rotatedL = false;
        }
    }

    public virtual void SetSpeed(float newSpeed)
    {
        this.speed = newSpeed;
    }

    public Movement()
    {
        this.speed = 4;
        this.rotateSpeed = 40f;
        this.jumpPower = 400;
        this.pulleySpeed = 0.1f;
        this.force = 100;
        this.lerpSpeed = 0.5f;
        this.dropOff = 3f;
        this.upforce = 10f;
        this.grappleJumpUp = 2f;
        this.grappleJumpForward = 2f;
    }

    static Movement()
    {
        Movement.rotated = true;
    }

}