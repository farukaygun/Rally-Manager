using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour
{
  public static string dbPath;
  public static string dbName = "rallyManagerData.db";

  public static string budget;
  public static Fixture currentFixture;

  public static string currentPilotId;
  public static string currentCarId;
  public static int pilotScore;
  public static int point;
}
