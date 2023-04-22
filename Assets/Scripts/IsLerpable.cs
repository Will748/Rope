using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class IsLerpable : MonoBehaviour
{
    public bool lerp;
    public GameObject player;
    public float stopDistance;
    public float lerpSpeed;
    private bool froze;
    public virtual void Awake()
    {
        if (!this.player)
        {
            this.player = GameObject.FindWithTag("Player");
        }
    }

    public virtual void Update()
    {
        if (this.lerp)
        {
            if (!this.froze)
            {
                this.froze = true;
                this.FreezePositions();
            }
            this.SendMessage("SetAllowMovement", false);
            float distance = Vector3.Distance(this.transform.position, this.player.transform.position);
            if (distance > this.stopDistance)
            {
                this.transform.position = Vector3.Lerp(this.transform.position, this.player.transform.position, this.lerpSpeed);
                float force = this.GetComponent<Rigidbody>().velocity.x;
            }
            else
            {
                //Debug.Log(rigidbody.velocity);
                //rigidbody.constraints = RigidbodyConstraints.FreezeAll;
                UnityEngine.Object.Destroy(GameObject.FindWithTag("Grapple"));
                this.player.GetComponent<Rigidbody>().constraints = this.player.GetComponent<Rigidbody>().constraints & ~RigidbodyConstraints.FreezePositionX;
                this.player.GetComponent<Rigidbody>().constraints = this.player.GetComponent<Rigidbody>().constraints & ~RigidbodyConstraints.FreezePositionY;
                //Debug.Log(rigidbody.velocity);
                //rigidbody.AddForce(Vector3.forward *force);
                this.lerp = false;
                this.froze = false;
            }
        }
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
            float _24 = this.transform.position.x;
            Vector3 _25 = GameObject.FindWithTag("Grapple").transform.position;
            _25.x = _24;
            GameObject.FindWithTag("Grapple").transform.position = _25;
        }

        {
            float _26 = this.transform.position.y;
            Vector3 _27 = GameObject.FindWithTag("Grapple").transform.position;
            _27.y = _26;
            GameObject.FindWithTag("Grapple").transform.position = _27;
        }
        this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        this.GetComponent<Rigidbody>().constraints = this.GetComponent<Rigidbody>().constraints & ~RigidbodyConstraints.FreezePositionY;
        this.player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
    }

    public virtual void LerpTrue()
    {
        this.lerp = true;
    }

    public IsLerpable()
    {
        this.stopDistance = 1.5f;
        this.lerpSpeed = 0.05f;
    }

}