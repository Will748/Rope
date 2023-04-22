using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class CameraRotation : MonoBehaviour
{
    public float ZPosition;
    public Transform player;
    public virtual void LateUpdate()
    {
        this.GetComponent<Camera>().transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));

        {
            float _6 = this.ZPosition;
            Vector3 _7 = this.transform.position;
            _7.z = _6;
            this.transform.position = _7;
        }

        {
            float _8 = this.player.position.x;
            Vector3 _9 = this.transform.position;
            _9.x = _8;
            this.transform.position = _9;
        }
    }

}