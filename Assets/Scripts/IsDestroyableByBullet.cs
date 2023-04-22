using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class IsDestroyableByBullet : MonoBehaviour
{
    public int health;
    public virtual void OnCollisionEnter(Collision other)
    {
        if (other.transform.gameObject.tag == "GunBullet")
        {
            this.health = this.health - 1;
            if (this.health == 0)
            {
                UnityEngine.Object.Destroy(this.gameObject);
                GameObject[] Enemys = GameObject.FindGameObjectsWithTag("Enemy2");
                foreach (GameObject enemy in Enemys)
                {
                    Debug.Log("t");
                    //enemy.SendMessage("NearestTarget");
                    enemy.SendMessage("LookAtNearestTarget");
                }
            }
        }
    }

    public IsDestroyableByBullet()
    {
        this.health = 5;
    }

}