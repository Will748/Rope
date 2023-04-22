using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class resource : MonoBehaviour
{
    public virtual void Start()
    {
    }

    public virtual void Update()
    {
    }

    public virtual void OnCollisonEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            UnityEngine.Object.Destroy(this.gameObject);
        }
    }

}