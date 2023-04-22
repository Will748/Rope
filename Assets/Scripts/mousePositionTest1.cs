using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class mousePositionTest1 : MonoBehaviour
{
    public virtual void OnGUI()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Event e = Event.current;
        Debug.Log(e.mousePosition);
    }

}