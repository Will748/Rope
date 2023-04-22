using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class Drum : MonoBehaviour
{
    public Transform player;
    public float rotateSpeed;
    public float zRotateSpeed;
    public static bool rotated;
    private bool rotating;
    //private var rotateOut =false;
    //private var rotateIn = false;
    public virtual void Awake()
    {
        if (!this.player)
        {
            this.player = GameObject.FindWithTag("Player").transform;
        }
    }

    public virtual void Update()//if(Input.GetKeyDown("h") && rotated && !rotating)ShootVibe();
    {
        if ((Input.GetKeyDown("f") && !Drum.rotated) && !this.rotating)
        {
            this.StartCoroutine(this.RotateDrum(this.player.up, 180, this.rotateSpeed, 1));
        }
        if ((Input.GetKeyDown("g") && Drum.rotated) && !this.rotating)
        {
            this.StartCoroutine(this.RotateZ(-90, 1));
        }
    }

    public virtual IEnumerator RotateDrum(/*point : Vector3,*/Vector3 axis, float rotateAmount, float rotateTime, int i)
    {
        Drum.rotated = true;
        this.rotating = true;
        float step = 0f; //non-smoothed
        float rate = 1f / rotateTime; //amount to increase non-smooth step by
        float smoothStep = 0f; //smooth step this time
        float lastStep = 0f; //smooth step last time
        while (step < 1f) // until we're done
        {
            step = step + (Time.deltaTime * rate); //increase the step
            smoothStep = Mathf.SmoothStep(0f, 1f, step); //get the smooth step
            this.transform.RotateAround(this.player.position, axis, rotateAmount * (smoothStep - lastStep));
            lastStep = smoothStep; //store the smooth step
            yield return null;
        }
        //finish any left-over
        //if(step > 1.0) transform.RotateAround(point, axis, rotateAmount * (1.0 - lastStep)); 
        if (i == 1)
        {
            this.StartCoroutine(this.RotateZ(90, 0));
        }
        else
        {
            this.rotating = false;
            Drum.rotated = false;
        }
    }

    public virtual IEnumerator RotateZ(int rotate, int i)//if(t < 1.0) transform.RotateZ(point, axis, rotateAmount * (1.0 - lastStep));	
    {
        if (i == 1)
        {
            this.rotating = true;
        }
        Quaternion oldRotation = this.transform.rotation;
        this.transform.Rotate(0, 0, rotate);
        Quaternion newRotation = this.transform.rotation;
        float t = 0f;
        while (t < 1f)
        {
            Debug.Log(t);
            this.transform.rotation = Quaternion.Slerp(oldRotation, newRotation, t);
            yield return null;
            t = t + (Time.deltaTime * this.zRotateSpeed);
        }
        if (i == 1)
        {

            {
                int _14 = 90;
                Vector3 _15 = this.transform.eulerAngles;
                _15.z = _14;
                this.transform.eulerAngles = _15;
            }
            this.StartCoroutine(this.RotateDrum(this.player.up, 180, this.rotateSpeed, 0));
        }
        else
        {

            {
                int _16 = 180;
                Vector3 _17 = this.transform.eulerAngles;
                _17.z = _16;
                this.transform.eulerAngles = _17;
            }
            Drum.rotated = true;
            this.rotating = false;
        }
    }

    public Drum()//function ShootVibe(){
    {
        this.rotateSpeed = 2f;
        this.zRotateSpeed = 1f;
    }

}