using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class AttackButton : MonoBehaviour
{
    private GameObject[] enemys;
    private GameObject[] enemys2;
    private GameObject[] guns;
    private GameObject[] turrets;
    public virtual void Start()
    {
    }

    public virtual void Update()
    {
    }

    public virtual void OnClick()
    {
        ControlUnit.allowObjectPlacement = true;
        this.enemys = GameObject.FindGameObjectsWithTag("Enemy");
        this.enemys2 = GameObject.FindGameObjectsWithTag("Enemy2");
        this.guns = GameObject.FindGameObjectsWithTag("Gun");
        this.turrets = GameObject.FindGameObjectsWithTag("Turret2");
        foreach (GameObject enemy in this.enemys)
        {
            enemy.SendMessage("setAllowMove", true);
            enemy.SendMessage("LookAtNearestTarget");
        }
        foreach (GameObject enemy in this.enemys2)
        {
            enemy.SendMessage("setAllowMove", true);
            enemy.SendMessage("LookAtNearestTarget");
        }
        foreach (GameObject gun in this.guns)
        {
            gun.SendMessage("FireMagazine");
        }
        foreach (GameObject turret in this.turrets)
        {
            turret.SendMessage("SetAllowShoot", true);
        }
        UnityEngine.Object.Destroy(GameObject.FindWithTag("GUI"));
    }

}