using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour
{
  public static string dbPath = "Assets/StreamingAssets/rallyManagerData.db";
  public static string dbName = "rallyManagerData.db";

  public static string budget;
  public static Fixture currentFixture;
  public static League currentLeague;
  public static string currentSeason;

  public static string currentPilotId = "0";
  public static string currentPilotTeamId = "0";
  public static string currentCarId;
  public static int pilotScore;
  public static int point;

  public static int wheelType;
  public static string groundType;

  public static float turboHealth = 100;
  public static float transmissionHealth = 100;
  public static float fuelHealth = 100;
  public static float engineHealth = 100;
  public static float wheelHealthLF = 100;
  public static float wheelHealthLB = 100;
  public static float wheelHealthRF = 100;
  public static float wheelHealthRB = 100;

}
