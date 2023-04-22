using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class Turret : MonoBehaviour
{
    public Transform enemy;
    public Transform myProjectile;
    public Transform projectileSpawnPoint;
    public int turnSpeed;
    public float reloadTime;
    public int accuracy;
    public int nextFireTime;
    public int nextMoveTime;
    public object allowShoot;
    public virtual void Start()
    {
        if (!this.myProjectile)
        {
            this.myProjectile = GameObject.FindWithTag("Projectile").transform;
        }
        if (!this.projectileSpawnPoint)
        {
            this.projectileSpawnPoint = GameObject.FindWithTag("projectileSpawnPoint").transform;
        }
    }

    public virtual void Update()
    {
        if (this.enemy && (this.allowShoot != null))
        {
            if (Time.time >= this.nextMoveTime)
            {
                Quaternion lookAtPosition = Quaternion.LookRotation(this.enemy.position - this.transform.position);
                //transform.rotation = Quaternion.Lerp(transform.rotation, lookAtPosition, Time.deltaTime * turnSpeed);
                this.transform.rotation = lookAtPosition;
            }
            if (Time.time >= this.nextFireTime)
            {
                this.shoot();
            }
        }
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if ((other.tag == "Enemy") || (other.tag == "Enemy2"))
        {
            this.nextFireTime = (int) (Time.time + 0.5f);
            this.nextMoveTime = (int) (Time.time + 0.5f);
            this.enemy = other.gameObject.transform;
        }
    }

    public virtual void OnTriggerExit(Collider other)
    {
        if (other.gameObject.transform == this.enemy)
        {
            this.enemy = null;
        }
    }

    public virtual void shoot()
    {
        UnityEngine.Object.Instantiate(this.myProjectile, this.projectileSpawnPoint.position, this.projectileSpawnPoint.rotation);
        this.nextFireTime = (int) (Time.time + this.reloadTime);
        this.nextMoveTime = (int) (Time.time + 0.5f);
    }

    public virtual void SetAllowShoot(bool setting)
    {
        this.allowShoot = setting;
    }

    public Turret()
    {
        this.turnSpeed = 5;
        this.reloadTime = 1f;
        this.accuracy = 2;
        this.nextFireTime = 1;
    }

}