using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class DropBirdPoo : MonoBehaviour
{
    public float randomMin;
    public float randomMax;
    public GameObject birdPoo;
    public Transform birdPooSpawnPoint;
    public virtual void Start()
    {
        this.StartCoroutine(this.DropPoo());
    }

    public virtual void Update()
    {
    }

    public virtual IEnumerator DropPoo()
    {
        Debug.Log("running");
        float randomNumber = Random.Range(this.randomMin, this.randomMax);
        yield return new WaitForSeconds(randomNumber);
        UnityEngine.Object.Instantiate(this.birdPoo, this.birdPooSpawnPoint.position, Quaternion.identity);
        this.StartCoroutine(this.DropPoo());
    }

    public DropBirdPoo()
    {
        this.randomMin = 1f;
        this.randomMax = 5f;
    }

}