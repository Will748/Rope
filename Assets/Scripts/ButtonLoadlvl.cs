using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class ButtonLoadlvl : MonoBehaviour
{
    public int lvlNumber;
    public virtual void Start()
    {
    }

    public virtual void Update()
    {
    }

    public virtual void OnClick()
    {
        Application.LoadLevel(this.lvlNumber);
    }

    public ButtonLoadlvl()
    {
        this.lvlNumber = 1;
    }

}