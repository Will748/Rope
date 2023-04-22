using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class Enemy : MonoBehaviour
{
    public float speed;
    public int distanceToTravel;
    public float rotateSpeed;
    private float distanceTravelled;
    private object startingPosition;
    private bool allowMovement;
    private int y;
    public virtual void Start()
    {
        this.startingPosition = this.transform.position;
    }

    public virtual void Update()
    {
        this.distanceTravelled = Vector3.Distance(this.transform.position, (Vector3) this.startingPosition);
        if (this.distanceTravelled > this.distanceToTravel)
        {
            this.StartCoroutine("Rotate");
        }
        if (this.allowMovement)
        {
            this.transform.Translate(0, 0, this.speed * Time.deltaTime);
        }
    }

    public virtual void SetSpeed(float newSpeed)
    {
        this.speed = newSpeed;
    }

    public virtual void SetAllowMovement(bool movement)
    {
        this.allowMovement = movement;
    }

    public virtual IEnumerator Rotate()
    {
        this.allowMovement = false;
        this.startingPosition = this.transform.position;
        float t = this.rotateSpeed;
        float incAmount = this.rotateSpeed * Time.deltaTime;
        while (incAmount < 1)
        {
            this.transform.eulerAngles = Vector3.Slerp(this.transform.eulerAngles, new Vector3(0, this.y, 0), incAmount);
            incAmount = incAmount + (this.rotateSpeed * Time.deltaTime);
            yield return null;
        }
        this.allowMovement = true;
        if (this.y == 270)
        {
            this.y = 90;
        }
        else
        {
            if (this.y == 90)
            {
                this.y = 270;
            }
        }
    }

    public Enemy()
    {
        this.speed = 4f;
        this.distanceToTravel = 10;
        this.rotateSpeed = 0.1f;
        this.allowMovement = true;
        this.y = 270;
    }

}