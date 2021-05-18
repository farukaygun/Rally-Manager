using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrivingTypeController : MonoBehaviour
{
    public Dropdown drivingTypeDropdown;
    public static float drivingTypeBrakeDistance;
    public static float drivingTypeBrakeSpeedLimit;
    
    public void SetDropdownValue()
    {
        // Sakin
        if (drivingTypeDropdown.value == 0)
        {
           drivingTypeBrakeDistance = 35f;
        }
        // Normal
        else if (drivingTypeDropdown.value == 1)
        {
            drivingTypeBrakeDistance = 30f;
        }
        // Agresif
        else if (drivingTypeDropdown.value == 2)
        {
            drivingTypeBrakeDistance = 25f;
        }
    }
}
