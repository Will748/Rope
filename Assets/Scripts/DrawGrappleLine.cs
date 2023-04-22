using System.Collections.Generic;
using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class DrawGrappleLine : MonoBehaviour
{
    // Line start width
    public static float startWidth;
    // Line end width
    public static float endWidth;
    public static int i;
    public static int n;
    //or you could use...
    //var aMaterial : Material;
    // get line going...
    public static LineRenderer line;
    public virtual void Awake()//meshCollider.sharedMesh;
    {
        DrawGrappleLine.line = (LineRenderer) this.gameObject.AddComponent(typeof(LineRenderer));
        DrawGrappleLine.line.SetWidth(DrawGrappleLine.startWidth, DrawGrappleLine.endWidth);
        DrawGrappleLine.line.SetVertexCount(2);
        DrawGrappleLine.line.material.color = Color.red;
        //we need to see the line... 
        DrawGrappleLine.line.GetComponent<Renderer>().enabled = true;
    }

    /*static function createLine(startingPoint : Vector3, endingPoint : Vector3){
	//var line = this.gameObject.AddComponent(LineRenderer);
   	line.SetWidth(startWidth, endWidth);
   	line.SetVertexCount(2);
  	line.material.color = Color.red;
   	line.renderer.enabled = true;
}*/
    public virtual void Update()
    {
        //get the shooter object...
        //set starting point of line to this object, in this case the grappling hook prefab
        DrawGrappleLine.line.SetPosition(1, this.gameObject.transform.position);
        //set the ending point of the line to the shooter object
        DrawGrappleLine.line.SetPosition(0, GameObject.Find("gunNull").transform.position);
    }

    public void getLinePositions()
    {
        DrawGrappleLine.i = 2;
        //line.renderer.enabled =false;
        DrawGrappleLine.line.SetVertexCount(DrawGrappleLine.i);
        int s = ShootGrapple2.grapplePositions.Count;
        while (s > 0)
        {
            DrawGrappleLine.line.SetVertexCount(DrawGrappleLine.i + 1);
            DrawGrappleLine.line.SetPosition(DrawGrappleLine.i, ShootGrapple2.grapplePositions[s - 1]);
            DrawGrappleLine.i++;
            s--;
        }
        //line.SetPosition(1, this.gameObject.transform.position);
        DrawGrappleLine.line.SetPosition(0, GameObject.Find("gunNull").transform.position);
        line.SetPosition(1, gameObject.transform.position);
        DrawGrappleLine.line.GetComponent<Renderer>().enabled = true;
    }

    /*static function addLinePosition(){
	i++
	line.SetVertexCount(i);
	line.SetPosition(i, 
	/*line.SetVertexCount(i+1);
	Debug.Log(ShootGrapple.grapplePositions[n]);
	line.SetPosition(i,ShootGrapple.grapplePositions[n]);
	i++;
	n++;
}*/
    public DrawGrappleLine()
    {
        DrawGrappleLine.startWidth = 0.05f;
        DrawGrappleLine.endWidth = 0.05f;
        DrawGrappleLine.i = 2;
    }

}