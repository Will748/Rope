using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class Waypoint : MonoBehaviour
{
    public void OnTriggerEnter(Collider hit)
    {
        if ((hit.gameObject.tag == "Enemy") && (EnemyMovement.right == false))
        {
            EnemyMovement.right = true;
            EnemyMovement.left = false;
        }
        else
        {
            if ((hit.gameObject.tag == "Enemy") && (EnemyMovement.left == false))
            {
                EnemyMovement.left = true;
                EnemyMovement.right = false;
            }
        }
    }

}