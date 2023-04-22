using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class Floor : MonoBehaviour
{
    public virtual void Start()
    {
        this.tag = "Untaken";
    }

    public virtual void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
        }
    }

}