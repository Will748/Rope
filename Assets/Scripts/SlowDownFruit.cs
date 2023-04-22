using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class SlowDownFruit : MonoBehaviour
{
    public float slowDownTo;
    public float time;
    public float destroyTime;
    public virtual void Start()
    {
        UnityEngine.Object.Destroy(this.gameObject, this.destroyTime);
    }

    public virtual IEnumerator OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            float origSpeed = ((Enemy) collision.gameObject.GetComponent(typeof(Enemy))).speed;
            //var speed2 = origSpeed/slowDownTo;
            EnemyMovement.speed = this.slowDownTo;
            Debug.Log(EnemyMovement.speed);
            yield return new WaitForSeconds(this.time);
            EnemyMovement.speed = origSpeed;
        }
    }

    public SlowDownFruit()
    {
        this.slowDownTo = 2f;
        this.time = 3f;
        this.destroyTime = 3f;
    }

}