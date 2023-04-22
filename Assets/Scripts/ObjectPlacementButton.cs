using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class ObjectPlacementButton : MonoBehaviour
{
    public Transform @object;
    public virtual void Start()
    {
    }

    public virtual void Update()
    {
    }

    public virtual void OnClick()
    {
        if (ControlUnit.allowObjectPlacement)
        {
            GameObject.Find("GridGameObject").SendMessage("setInstanceObject", this.@object);
        }
    }

}