using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class TestButton : MonoBehaviour
{
    public virtual void Start()
    {
    }

    public virtual void Update()
    {
    }

    /*Debug.Log("Click");
	var input : String = GameObject.Find("Input").GetComponent(UIInput).text;
	GameObject.Find("Player").GetComponent(ShootGrapple2).grapplePositions.RemoveAt(parseInt(input));
	GameObject.Find("Player").GetComponent(ShootGrapple2).swingDirs.RemoveAt(parseInt(input));
	GameObject.Find("Player").GetComponent(Gtrigger).compareAngles.RemoveAt(parseInt(input));
	Debug.Log(input);*/    public virtual void OnClick()
    {
    }

}