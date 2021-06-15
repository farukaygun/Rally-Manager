using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Race : MonoBehaviour
{
  public GameObject prefab;
  private void Start()
  {
    prefab = (GameObject)Instantiate(Resources.Load("Araba 1"));
  }
}
