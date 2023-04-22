using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class InstatiateObjectatMouseTest : MonoBehaviour
{
    public GameObject prefab;
    public float distanceFromCamera;
    public virtual void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = this.distanceFromCamera;
        Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Debug.Log(worldMousePosition);
        if (Input.GetButtonDown("Fire1"))
        {
            UnityEngine.Object.Instantiate(this.prefab, worldMousePosition, Quaternion.identity);
        }
        if (Input.GetButtonDown("Fire2"))
        {
            UnityEngine.Object.Destroy(GameObject.FindWithTag("Test"));
        }
    }

}