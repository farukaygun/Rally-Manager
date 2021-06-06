using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    public Button buttonPlay;

    void InsertToTable()
    {
      DatabaseInsertion.InsertPilotsTotblPilots("Faruk", 24, 77, 88, 5000, null);
      DatabaseInsertion.InsertPilotsTotblPilots("Ali", 26, 66, 88, 10000, null);
      DatabaseInsertion.InsertPilotsTotblPilots("Muhammed", 33, 77, 88, 8888, "1");
      DatabaseInsertion.InsertPilotsTotblPilots("Seca", 22, 66, 77, 1000, null);
      DatabaseInsertion.InsertPilotsTotblPilots("Hamit", 31, 88, 99, 11111, null);

      DatabaseInsertion.InsertCarsTotblCar("Araba 1", 1200, 35, 100, 200000, null);
    }

    private void OnEnable()
    {
      buttonPlay.onClick.AddListener(() => InsertToTable());
    }
}
