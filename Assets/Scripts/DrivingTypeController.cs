using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrivingTypeController : MonoBehaviour
{
  public Dropdown drivingTypeDropdown;

  private void Start()
  {
    drivingTypeDropdown = GameObject.Find("DropdownDrivingType").GetComponent<Dropdown>();
  }

  public void SetDropdownValue()
  {
    // Sakin
    if (drivingTypeDropdown.value == 0)
    {
      CarEngine.maxBrakeTorque = 65f;
    }
    // Normal
    else if (drivingTypeDropdown.value == 1)
    {
      CarEngine.maxBrakeTorque = 60f;
    }
    // Agresif
    else if (drivingTypeDropdown.value == 2)
    {
      CarEngine.maxBrakeTorque = 55f;
    }
  }
}
