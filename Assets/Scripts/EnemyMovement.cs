using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class EnemyMovement : MonoBehaviour
{
    public static bool left;
    public static bool right;
    public static bool lerp;
    public static float speed;
    public float lerpSpeed;
    public GameObject player;
    public float stopDistance;
    public static bool allowMovement;
    private bool froze;
    public virtual void Awake()
    {
        if (!this.player)
        {
            this.player = GameObject.FindWithTag("Player");
        }
    }

    /*if(allowMovement){
		if(left){
			transform.Translate(-speed * Time.deltaTime, 0 ,0);
		}
		if(right){
			transform.Translate(speed * Time.deltaTime, 0,0);
		}
	}*//*if(lerp){
		if(!froze){
			froze = true;
			FreezePositions();	
		}	
		var distance = Vector3.Distance(transform.position, player.transform.position);
		if(distance > stopDistance){
			transform.position = Vector3.Lerp(transform.position, player.transform.position,
			lerpSpeed);
			var force = rigidbody.velocity.x;
			//Debug.Log(rigidbody.velocity);
		}
		else{
			//rigidbody.constraints = RigidbodyConstraints.FreezeAll;
			Destroy(GameObject.FindWithTag("Grapple"));
    		player.rigidbody.constraints &= ~RigidbodyConstraints.FreezePositionX;
    		player.rigidbody.constraints &= ~RigidbodyConstraints.FreezePositionY;
			//Debug.Log(rigidbody.velocity);
			//rigidbody.AddForce(Vector3.forward *force);
			lerp = false;
			froze = false;
		}
	}*/    public virtual void Update()//Debug.Log("kkk");
    {
    }

    public virtual void OnTriggerEnter(Collider other)//other.transform.position.y = transform.position.y;
    {
        //Debug.Log("Grapple");
        if (other.gameObject.tag == "Grapple")
        {
            //Debug.Log("Grapplehit");
            other.GetComponent<Rigidbody>().constraints = other.GetComponent<Rigidbody>().constraints & ~RigidbodyConstraints.FreezePositionX;
            other.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    public virtual void FreezePositions()
    {

        {
            float _18 = this.transform.position.x;
            Vector3 _19 = GameObject.FindWithTag("Grapple").transform.position;
            _19.x = _18;
            GameObject.FindWithTag("Grapple").transform.position = _19;
        }

        {
            float _20 = this.transform.position.y;
            Vector3 _21 = GameObject.FindWithTag("Grapple").transform.position;
            _21.y = _20;
            GameObject.FindWithTag("Grapple").transform.position = _21;
        }
        this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        this.GetComponent<Rigidbody>().constraints = this.GetComponent<Rigidbody>().constraints & ~RigidbodyConstraints.FreezePositionY;
        this.player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
    }

    /*function OnCollisionExit (other : Collision) {
    if(other.gameObject.tag == "Player") {
        other.transform.parent = null;
    }
}*/
    public virtual void ChangeSpeed(float newSpeed)
    {
        EnemyMovement.speed = newSpeed;
    }

    public virtual void SetAllowMovement(bool movement)
    {
        EnemyMovement.allowMovement = movement;
    }

    public EnemyMovement()
    {
        this.lerpSpeed = 0.05f;
        this.stopDistance = 1.5f;
    }

    static EnemyMovement()
    {
        EnemyMovement.left = true;
        EnemyMovement.speed = 4f;
        EnemyMovement.allowMovement = true;
    }

}