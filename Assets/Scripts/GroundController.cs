using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GroundController : MonoBehaviour
{
  public Dropdown groundTypeDropdown;
  public GameObject road;

  private void Awake()
  {
    road = GameObject.Find("Ground");
    groundTypeDropdown = GameObject.Find("DropdownGroundType").GetComponent<Dropdown>();
  }

  public void SetDropdownValue()
  {
    // Çakýl
    if (groundTypeDropdown.value == 0)
    {
      return;
    }
    // Asfalt
    else if (groundTypeDropdown.value == 1)
    {
      SetLayerRecursively(road, 9);
    }
    // Karlý
    else if (groundTypeDropdown.value == 2)
    {
      SetLayerRecursively(road, 8);
    }
    // Toprak
    else if (groundTypeDropdown.value == 3)
    {
      return;
    }
  }

  void SetLayerRecursively(GameObject obj, int newLayer)
  {
    if (null == obj)
    {
      return;
    }

    obj.layer = newLayer;

    foreach (Transform child in obj.transform)
    {
      if (null == child)
      {
        continue;
      }
      SetLayerRecursively(child.gameObject, newLayer);
    }
  }
}
