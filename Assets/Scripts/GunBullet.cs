using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class GunBullet : MonoBehaviour
{
    public int range;
    public int speed;
    public GameObject target;
    public object startPosition;
    public virtual void Start()
    {
        //Destroy(gameObject, 3);
        this.startPosition = this.transform.position;
    }

    public virtual void Update()
    {
        this.transform.Translate(0, this.speed * Time.deltaTime, 0);
        float distanceTravelled = Vector3.Distance(this.transform.position, (Vector3) this.startPosition);
        if (distanceTravelled > this.range)
        {
            UnityEngine.Object.Destroy(this.gameObject);
        }
    }

    public virtual void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject);
        UnityEngine.Object.Destroy(this.gameObject);
    }

    public GunBullet()
    {
        this.range = 8;
        this.speed = 200;
    }

}