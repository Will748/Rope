using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class Dive : MonoBehaviour
{
    public float diveSpeed;
    public virtual void Start()
    {
    }

    public virtual void Update()
    {
    }

    public virtual void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            //transform.parent.transform.position = Vector3(0,0,0);
            this.transform.parent.SendMessage("SetAllowMovement", false);
            this.StartCoroutine(this.MoveTowardsPlayer());
        }
    }

    public virtual IEnumerator MoveTowardsPlayer()
    {
        while (Vector3.Distance(GameObject.FindWithTag("Player").transform.position, this.transform.parent.position) > 2)
        {
            this.transform.parent.transform.LookAt(GameObject.FindWithTag("Player").transform.position);
            this.transform.parent.Translate(0, 0, this.diveSpeed);
            yield return null;
        }
        this.StartCoroutine(this.FlyBack());
    }

    public virtual IEnumerator FlyBack()
    {
        Debug.Log("i");

        {
            int _10 = -40;
            Vector3 _11 = this.transform.parent.eulerAngles;
            _11.x = _10;
            this.transform.parent.eulerAngles = _11;
        }
        while (this.transform.parent.position.y < 16)
        {
            this.transform.parent.Translate(0, 0, this.diveSpeed);
            yield return null;
        }

        {
            int _12 = 0;
            Vector3 _13 = this.transform.parent.eulerAngles;
            _13.x = _12;
            this.transform.parent.eulerAngles = _13;
        }
        this.transform.parent.SendMessage("SetAllowMovement", true);
    }

    public Dive()
    {
        this.diveSpeed = 5f;
    }

}