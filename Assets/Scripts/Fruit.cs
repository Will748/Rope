using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class Fruit : MonoBehaviour
{
    public float destroyTime;
    public static float damage;
    public virtual void Start()
    {
        UnityEngine.Object.Destroy(this.gameObject, this.destroyTime);
    }

    /*function OnCollisionEnter(collision : Collision) : IEnumerator{
	
	if(!collision.collider.gameObject.tag == "Player" 
		&& !collision.collider.gameObject.tag == "Drum")Destroy(gameObject);
}*/
    public Fruit()
    {
        this.destroyTime = 5f;
    }

    static Fruit()
    {
        Fruit.damage = 1f;
    }

}