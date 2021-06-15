using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    public Button buttonPlay;

    void InsertToTable()
    {
      DatabaseInsertion.InsertTotblTeam("Team 2", null, null, 300000);
      DatabaseInsertion.InsertTotblTeam("Team 3", null, null, 300000);

      DatabaseInsertion.InsertPilotsTotblPilots("Faruk", 24, 77, 88, 5000, null);
      DatabaseInsertion.InsertPilotsTotblPilots("Ali", 26, 66, 88, 10000, null);
      DatabaseInsertion.InsertPilotsTotblPilots("Muhammed", 33, 77, 88, 8888, null);
      DatabaseInsertion.InsertPilotsTotblPilots("Seca", 22, 66, 77, 1000, null);

      DatabaseInsertion.InsertSpecsTotblPilotSpecs(8, 8, 8, 8, 8, 1);
      DatabaseInsertion.InsertSpecsTotblPilotSpecs(5, 5, 5, 5, 5, 2);
      DatabaseInsertion.InsertSpecsTotblPilotSpecs(4, 4, 4, 4, 4, 3);
      DatabaseInsertion.InsertSpecsTotblPilotSpecs(4, 4, 4, 4, 4, 4);

      DatabaseInsertion.InsertPilotsTotblPilots("Hamit", 31, 88, 99, 11111, "2");
      DatabaseInsertion.InsertPilotsTotblPilots("Halil", 31, 88, 99, 11111, "2");

      DatabaseInsertion.InsertSpecsTotblPilotSpecs(4, 4, 4, 4, 4, 5);
      DatabaseInsertion.InsertSpecsTotblPilotSpecs(4, 4, 4, 4, 4, 6);

      DatabaseInsertion.InsertPilotsTotblPilots("Hamdi", 31, 88, 99, 11111, "3");
      DatabaseInsertion.InsertPilotsTotblPilots("Ahmet", 26, 48, 79, 11111, "3");

      DatabaseInsertion.InsertSpecsTotblPilotSpecs(4, 4, 4, 4, 4, 7);
      DatabaseInsertion.InsertSpecsTotblPilotSpecs(4, 4, 4, 4, 4, 8);

      DatabaseInsertion.InsertCarsTotblCar("Araba 1", 1200, 35, 100, 200000, null);
      DatabaseInsertion.InsertCarsTotblCar("Araba 2", 1200, 35, 100, 200000, "2");
      DatabaseInsertion.InsertCarsTotblCar("Araba 3", 1200, 35, 100, 200000, "3");

      DatabaseInsertion.InsertTotblFixture("Türkiye", "Etap 1", false);
      DatabaseInsertion.InsertTotblFixture("Monte Carlo", "Etap 2", false);
    }

    private void OnEnable()
    {
      buttonPlay.onClick.AddListener(() => InsertToTable());
    }
}
