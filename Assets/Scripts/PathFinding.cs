using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class PathFinding : MonoBehaviour
{
    public float x;
    public float y;
    public float z;
    public virtual void Start()
    {
    }

    public virtual void Update()
    {
    }

    public virtual void OnTriggerEnter(Collider Other)
    {
        if (Other.GetComponent<Collider>().tag == "Enemy")
        {
            Other.transform.Rotate(this.x, this.y, this.z);
        }
    }

}