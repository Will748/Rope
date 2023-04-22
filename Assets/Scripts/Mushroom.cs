using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class Mushroom : MonoBehaviour
{
    public int force;
    public virtual void Start()
    {
    }

    public virtual void Update()
    {
    }

    public virtual void OnCollisionEnter(Collision collision)
    {
        Debug.Log("force");
        collision.rigidbody.AddForce(Vector3.up * this.force);
    }

    public Mushroom()
    {
        this.force = 20;
    }

}