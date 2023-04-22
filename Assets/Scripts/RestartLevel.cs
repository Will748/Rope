using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class RestartLevel : MonoBehaviour
{
    public virtual void Update()
    {
        if (Input.GetKey("r"))
        {
            Application.LoadLevel(0);
        }
    }

}