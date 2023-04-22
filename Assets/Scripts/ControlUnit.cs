using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class ControlUnit : MonoBehaviour
{
    public static bool allowObjectPlacement;
    static ControlUnit()
    {
        ControlUnit.allowObjectPlacement = true;
    }

}