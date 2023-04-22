using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class Bullet : MonoBehaviour
{
    public float speed;
    public float damage;
    public virtual void Start()
    {
        UnityEngine.Object.Destroy(this.gameObject, 5);
    }

    public virtual void Update()
    {
        this.transform.Translate(0, 0, this.speed);
    }

    public virtual void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            ((Health) collider.gameObject.GetComponent(typeof(Health))).ApplyDamage(this.damage);
        }
    }

    public Bullet()
    {
        this.speed = 10f;
        this.damage = 10f;
    }

}