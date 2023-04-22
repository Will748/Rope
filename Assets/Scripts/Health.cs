using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class Health : MonoBehaviour
{
    public float health;
    public virtual void Start()
    {
    }

    public virtual void Update()
    {
    }

    public virtual void ApplyDamage(float damage)
    {
        this.health = this.health - damage;
        if (this.health == 0)
        {
            Application.LoadLevel(0);
        }
    }

    public Health()
    {
        this.health = 10f;
    }

}