using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class OnCollisionStop3 : MonoBehaviour
{
    /*function OnCollisionEnter(collision : Collision) {
	rigidbody.useGravity = false;
	Debug.Log("Unity");
    if(collision.gameObject.tag == "Enemy") {
    	Debug.Log("HitEnem");
    	collision.transform.parent = transform;
    	rigidbody.constraints &= ~RigidbodyConstraints.FreezePositionX;	
    }
}
/*function OnCollisionExit (other : Collision) {
    if(other.gameObject.tag == "Player") {
        other.transform.parent = null;
    }
}*/
    public virtual void Start()//Physics.IgnoreCollision(GameObject.FindWithTag("Enemy").collider, collider);
    {
    }

}