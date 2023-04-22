using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class Gun : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletSpawnPoint;
    public float rof;
    public int magazine;
    public int magazineRefilTime;
    public virtual IEnumerator FireMagazine()
    {
        int shotsFired = 0;
        while (shotsFired < this.magazine)
        {
            this.FireBullet();
            yield return new WaitForSeconds(this.rof);
            shotsFired++;
        }
        yield return new WaitForSeconds(this.magazineRefilTime);
        this.StartCoroutine(this.FireMagazine());
    }

    public virtual void FireBullet()
    {
        GameObject instanceBullet = UnityEngine.Object.Instantiate(this.bullet, this.bulletSpawnPoint.position, this.bulletSpawnPoint.rotation);
    }

    public Gun()
    {
        this.rof = 0.5f;
        this.magazine = 10;
        this.magazineRefilTime = 2;
    }

}