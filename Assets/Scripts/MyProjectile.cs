using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class MyProjectile : MonoBehaviour
{
    public int range;
    public int speed;
    public virtual void Start()
    {
        UnityEngine.Object.Destroy(this.gameObject, 3);
    }

    public virtual void Update()
    {
        this.transform.Translate(0, this.speed * Time.deltaTime, 0);
        float distanceTravelled = Vector3.Distance(this.transform.position, GameObject.FindWithTag("projectileSpawnPoint").transform.position);
        if (distanceTravelled > this.range)
        {
            UnityEngine.Object.Destroy(this.gameObject);
        }
    }

    public virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.transform.tag != "Turret")
        {
            UnityEngine.Object.Destroy(this.gameObject);
        }
    }

    public MyProjectile()
    {
        this.range = 8;
        this.speed = 200;
    }

}