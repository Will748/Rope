using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class DoneButton : MonoBehaviour
{
    public int lvlAdditive;
    public virtual void Start()
    {
    }

    public virtual void Update()
    {
    }

    public virtual void OnClick()
    {
        GameObject.Find("GridGameObject").SendMessage("setInstanceObjectNull");
        ControlUnit.allowObjectPlacement = false;
        Debug.Log(ControlUnit.allowObjectPlacement);
        Application.LoadLevelAdditive(this.lvlAdditive);
    }

}