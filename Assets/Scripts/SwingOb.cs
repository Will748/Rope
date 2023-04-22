using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class SwingOb : MonoBehaviour
{
    public virtual void OnTriggerEnter(Collider Other)
    {
        if (Other.GetComponent<Collider>().gameObject.tag == "gTrigger")
        {
            ShootGrapple2.grapplePositions.RemoveAt(ShootGrapple2.grapplePositions.Count - 1);
            //ShootGrapple2.moveGrapple(ShootGrapple2.grapplePositions[ShootGrapple2.grapplePositions.Count -1]);
            this.transform.position = Gtrigger.swingObs[Gtrigger.swingObs.Count - 1];
            Gtrigger.hitObs[Gtrigger.hitObs.Count - 1].tag = "Untagged";
            Gtrigger.hitObs.RemoveAt(Gtrigger.hitObs.Count - 1);
            GameObject.FindWithTag("GrapGun").GetComponent<DrawGrappleLine>().getLinePositions();
            UnityEngine.Object.Destroy(this.gameObject);
        }
    }

}