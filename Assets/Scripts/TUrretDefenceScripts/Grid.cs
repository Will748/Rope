using UnityEngine;
using System.Collections;
using System;

[System.Serializable]
public partial class Grid : MonoBehaviour
{
    public GameObject lastGrid;
    public Transform turr;
    public Transform ground;
    public Vector3 rotAmount;
    public LayerMask layerMask;
    public Transform instantiateObject;
    private int i;
    private object objectInstantiated;
    private Transform floatingObject;
    public virtual void Start()//instantiateObject = turr;
    {
    }

    public virtual void Update()
    {
        RaycastHit hit = default(RaycastHit);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if ((this.objectInstantiated == null) && (this.lastGrid != null))
        {
            this.objectInstantiated = true;
            Debug.Log("hello");
           try
            {
                this.floatingObject = UnityEngine.Object.Instantiate(this.instantiateObject, new Vector3(this.lastGrid.transform.position.x, this.lastGrid.transform.position.y + 5, this.lastGrid.transform.position.z), Quaternion.identity);
            }
           catch(Exception)
            {
            }
        }
        if ((this.floatingObject != null) && (this.lastGrid != null))
        {
            this.floatingObject.transform.position = new Vector3(this.lastGrid.transform.position.x, this.lastGrid.transform.position.y + 5, this.lastGrid.transform.position.z);
        }
        if (Physics.Raycast(ray, out hit, 10000, this.layerMask.value))
        {
            if (this.lastGrid)
            {
                this.lastGrid.GetComponent<Renderer>().material.color = Color.white;
            }
            //lastGrid.tag = "Untaken";
            this.lastGrid = hit.collider.gameObject;
            this.lastGrid.GetComponent<Renderer>().material.color = Color.red;
        }
        else
        {
            //lastGrid.tag = "Untaken";
            //Instantiate(GameObject.Find("Enemy"), hit.point, Quaternion.identity);
            if (this.lastGrid)
            {
                this.lastGrid.GetComponent<Renderer>().material.color = Color.white;
                this.lastGrid = null;
            }
        }
        if ((Input.GetMouseButtonDown(0) && (this.lastGrid != null)) && (this.lastGrid.tag == "Untaken"))
        {
            this.i = this.i + 1;
            Vector3 spawnPoint = new Vector3(this.lastGrid.transform.position.x, this.lastGrid.transform.position.y + 0.1f, this.lastGrid.transform.position.z);
            this.rotAmount = this.ground.eulerAngles;
            Transform ob = UnityEngine.Object.Instantiate(this.instantiateObject, spawnPoint, Quaternion.identity);
            ob.name = ob.name + this.i;
            //Ob.transform.eulerAngles = rotAmount;
            //turrOb.transform.eulerAngles.x -= 0;
            this.lastGrid.tag = "Taken";
            this.lastGrid = null;
        }
    }

    public virtual void setInstanceObject(Transform gO)
    {
        this.instantiateObject = gO;
        if (this.floatingObject != null)
        {
            UnityEngine.Object.Destroy(this.floatingObject.gameObject);
        }
        this.objectInstantiated = false;
    }

    public virtual void setInstanceObjectNull()
    {
        this.instantiateObject = null;
    }

}