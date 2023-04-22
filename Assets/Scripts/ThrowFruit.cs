using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class ThrowFruit : MonoBehaviour
{
    public GameObject Fruit;
    public float throwFruitForceH1;
    public float throwFruitForceV1;
    public float throwFruitForceH2;
    public float throwFruitForceV2;
    public float throwFruitForceH3;
    public float throwFruitForceV3;
    public int throwFruitBehindForceH;
    public float throwFruitBehindForceV;
    public float distance1;
    public float distance2;
    public float distance3;
    public int distanceFromCamera;
    public Transform fruitSpawnPoint;
    public virtual void Awake()
    {
        this.fruitSpawnPoint = GameObject.Find("FruitSpawnPoint").transform;
        if (!this.Fruit)
        {
            this.Fruit = GameObject.FindWithTag("Fruit");
        }
    }

    public virtual void Update()
    {
        if (Input.GetButtonDown("Fire2") && (Drum.rotated == false))
        {
            GameObject newFruit = UnityEngine.Object.Instantiate(this.Fruit, this.fruitSpawnPoint.position, Quaternion.identity);
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = this.distanceFromCamera;
            Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            Vector3 shootDirection = worldMousePosition - this.transform.position;
            shootDirection = shootDirection.normalized;
            float distance = Vector3.Distance(worldMousePosition, this.fruitSpawnPoint.position);
            float dot = Vector3.Dot(this.transform.right, this.transform.InverseTransformPoint(worldMousePosition).normalized);
            Vector3 lworldMousePosition = this.transform.InverseTransformPoint(worldMousePosition);
            if (((distance > this.distance1) && (distance < this.distance2)) && (lworldMousePosition.x > 0))
            {
                newFruit.GetComponent<Rigidbody>().AddForce(shootDirection * this.throwFruitForceH1);
                newFruit.GetComponent<Rigidbody>().AddForce((Vector3.up * this.throwFruitForceV2) + this.GetComponent<Rigidbody>().velocity);
            }
            else
            {
                if (((distance > this.distance2) && (distance < this.distance3)) && (lworldMousePosition.x > 0))
                {
                    newFruit.GetComponent<Rigidbody>().AddForce(shootDirection * this.throwFruitForceH2);
                    newFruit.GetComponent<Rigidbody>().AddForce((Vector3.up * this.throwFruitForceV2) + this.GetComponent<Rigidbody>().velocity);
                }
                else
                {
                    if ((distance > this.distance3) && (lworldMousePosition.x > 0))
                    {
                        newFruit.GetComponent<Rigidbody>().AddForce(shootDirection * this.throwFruitForceH3);
                        newFruit.GetComponent<Rigidbody>().AddForce((Vector3.up * this.throwFruitForceV3) + this.GetComponent<Rigidbody>().velocity);
                    }
                    else
                    {
                        if (lworldMousePosition.x < 0)
                        {
                            newFruit.GetComponent<Rigidbody>().AddForce(shootDirection * this.throwFruitBehindForceH);
                            newFruit.GetComponent<Rigidbody>().AddForce((Vector3.up * this.throwFruitBehindForceV) + this.GetComponent<Rigidbody>().velocity);
                        }
                    }
                }
            }
        }
    }

    public ThrowFruit()
    {
        this.throwFruitForceH1 = 400f;
        this.throwFruitForceV1 = 200f;
        this.throwFruitForceH2 = 400f;
        this.throwFruitForceV2 = 200f;
        this.throwFruitForceH3 = 400f;
        this.throwFruitForceV3 = 200f;
        this.throwFruitBehindForceH = 20;
        this.throwFruitBehindForceV = 200f;
        this.distance1 = 1f;
        this.distance2 = 3f;
        this.distance3 = 9f;
        this.distanceFromCamera = 20;
    }

}