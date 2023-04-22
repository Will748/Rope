using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class BirdPoo : MonoBehaviour
{
    public float damage;
    public bool landed;
    public float time;
    public float SetSpeedTo;
    public virtual void Start()
    {
    }

    public virtual void Update()
    {
    }

    public virtual IEnumerator OnCollisionEnter(Collision collision)
    {
        Debug.Log("landing");
        if ((collision.gameObject.tag == "Player") && !this.landed)
        {
            collision.gameObject.SendMessage("ApplyDamage", this.damage);
        }
        if ((collision.gameObject.tag == "Player") && this.landed)
        {
            collision.gameObject.SendMessage("SetSpeed", this.SetSpeedTo);
            yield return new WaitForSeconds(this.time);
            collision.gameObject.SendMessage("SetSpeed", 4f);
        }
        else
        {
            this.landed = true;
        }
    }

    public BirdPoo()
    {
        this.damage = 1f;
        this.time = 5f;
        this.SetSpeedTo = 2f;
    }

}