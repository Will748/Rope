using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class ropeScript : MonoBehaviour
{
    public int distanceFromCamera;
    public Transform grappleSpawnPoint;
    public GameObject player;
    public int range;
    public GameObject ropeGrapple;
    public GameObject capsule;
    private GameObject instanceRopeGrapple;
    private Vector3 capsSpawnPoint;
    private bool hasShot;
    private System.Collections.Generic.List<GameObject> instanceCapsL;
    public virtual void Start()
    {
    }

    public virtual void Awake()
    {
        if (!this.player)
        {
            this.player = GameObject.FindWithTag("Player");
        }
        if (!this.grappleSpawnPoint)
        {
            this.grappleSpawnPoint = GameObject.Find("gunNull").transform;
        }
    }

    public virtual void Update()
    {
        RaycastHit hit = default(RaycastHit);
        if (Input.GetButtonDown("Fire1"))
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = this.distanceFromCamera;
            Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            if (Physics.Raycast(this.transform.position, worldMousePosition - this.transform.position, out hit, this.range))
            {
                this.instanceRopeGrapple = UnityEngine.Object.Instantiate(this.ropeGrapple, this.grappleSpawnPoint.position, Quaternion.identity);
                float distance = Vector3.Distance(this.grappleSpawnPoint.position, hit.point);
                this.instanceRopeGrapple.transform.position = hit.point;
                this.instanceCapsL.Add(this.instanceRopeGrapple);
                this.capsSpawnPoint = this.instanceRopeGrapple.transform.position;
                float noOfCapsules = Mathf.Abs(distance);
                //createCapsules(noOfCapsules);
                Quaternion setRotAmount = Quaternion.LookRotation(this.grappleSpawnPoint.position - this.instanceRopeGrapple.transform.position);
                this.StartCoroutine(this.createCapsules((int) (distance * 4.5f)));
            }
        }
    }

    public virtual IEnumerator createCapsules(int capsuleNumber)
    {
        int createdCapsules = 0;
        Quaternion setRotAmount = Quaternion.LookRotation(this.grappleSpawnPoint.position - this.instanceRopeGrapple.transform.position);
        while (capsuleNumber > createdCapsules)
        {
            GameObject instanceCaps = UnityEngine.Object.Instantiate(this.capsule, this.capsSpawnPoint, setRotAmount);
            instanceCaps.transform.position = instanceCaps.transform.position + instanceCaps.transform.TransformDirection(0, 0, 0.25f);
            this.instanceCapsL.Add(instanceCaps);
            this.capsSpawnPoint = instanceCaps.transform.position;

            {
                float _38 = instanceCaps.transform.eulerAngles.x + 90;
                Vector3 _39 = instanceCaps.transform.eulerAngles;
                _39.x = _38;
                instanceCaps.transform.eulerAngles = _39;
            }
            createdCapsules++;
            if (createdCapsules == capsuleNumber)
            {
                this.StartCoroutine(this.setConnectedBodies());
            }
            yield return null;
        }
    }

    public virtual IEnumerator setConnectedBodies()
    {
        int i = this.instanceCapsL.Count;
        i = i - 1;
        while (i > 0)
        {
            Debug.Log("done");
            ((CharacterJoint) this.instanceCapsL[i].GetComponent(typeof(CharacterJoint))).connectedBody = (Rigidbody) this.instanceCapsL[i - 1].GetComponent(typeof(Rigidbody));
            i--;
            yield return null;
        }
    }

    public ropeScript()
    {
        this.distanceFromCamera = 20;
        this.range = 100;
        this.instanceCapsL = new System.Collections.Generic.List<GameObject>();
    }

}