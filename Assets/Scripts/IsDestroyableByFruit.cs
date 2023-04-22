using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class IsDestroyableByFruit : MonoBehaviour
{
    public float health;
    public Fruit script;
    public virtual void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Fruit")
        {
            Debug.Log("hit");
            this.script = (Fruit) collider.gameObject.GetComponent(typeof(Fruit));
            this.ApplyDamage();
        }
    }

    public virtual void ApplyDamage()
    {
        this.health = this.health - Fruit.damage;
        if (this.health == 0)
        {
            UnityEngine.Object.Destroy(this.gameObject);
        }
    }

    public IsDestroyableByFruit()
    {
        this.health = 3f;
    }

}