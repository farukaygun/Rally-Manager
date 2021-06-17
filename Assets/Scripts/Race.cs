using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PilotTime
{
  public int pilotId { get; set; }
  public int teamId { get; set; }
  public TimeSpan time { get; set; }
  public int point { get; set; }
  public int budget { get; set; }
}

public class Race : MonoBehaviour
{
  public Dropdown dropdownDrivingType;
  public Button goToTeamScoreBoard;
  public Button backToLobby;
  public Text textGroundType;
  public Text textTurboHealth;
  public Text textTransmissionHealth;
  public Text textFuelHealth;
  public Text textEngineHealth;
  public Text wheelHealthLF;
  public Text wheelHealthLB;
  public Text wheelHealthRF;
  public Text wheelHealthRB;

  public GameObject prefab;
  public GameObject finishLine;
  public GameObject pilotScoreBoard;
  public GameObject teamScoreBoard;
  public GameObject rowPilots;
  public GameObject rowTeams;
  public Loading loading;

  private GameObject newRow;

  TimeSpan pilot1Score;
  TimeSpan pilot2Score;
  TimeSpan pilot3Score;
  TimeSpan userScore = new TimeSpan();

  static bool stopwatchActive = false;
  float currentTime;
  public int startMinutes;

  public Text textTime;

  List<PilotTime> pilotTimeArray = new List<PilotTime>();

  private void Start()
  {
    prefab = (GameObject)Instantiate(Resources.Load("Araba 1"));
    currentTime = startMinutes * 60;

    Global.fuelHealth = 100;
    Global.engineHealth = 100;
    Global.transmissionHealth = 100;
    Global.turboHealth = 100;
    Global.wheelHealthLB = 100;
    Global.wheelHealthLF = 100;
    Global.wheelHealthRB = 100;
    Global.wheelHealthRF = 100;
  }

  private void FixedUpdate()
  {
    textGroundType.text = Global.groundType;
    textFuelHealth.text = "Yakýt: " + Math.Floor(Global.fuelHealth).ToString();
    textEngineHealth.text = "Motor: " + Math.Floor(Global.engineHealth).ToString();
    textTransmissionHealth.text = "Vites: " + Math.Floor(Global.transmissionHealth).ToString();
    textTurboHealth.text = "Turbo: " + Math.Floor(Global.turboHealth).ToString();
    wheelHealthLB.text = "Sol Arka: " + Math.Floor(Global.wheelHealthLB).ToString();
    wheelHealthLF.text = "Sol Ön: " + Math.Floor(Global.wheelHealthLF).ToString();
    wheelHealthRB.text = "Sað Arka: " + Math.Floor(Global.wheelHealthRB).ToString();
    wheelHealthRF.text = "Sað Ön: " + Math.Floor(Global.wheelHealthRF).ToString();

    if (stopwatchActive == true)
    {
      currentTime = currentTime + Time.deltaTime;
    }

    TimeSpan time = TimeSpan.FromSeconds(currentTime);
    textTime.text = time.ToString(@"mm\:ss\:fff");
  }

  public static void StartStopwatch()
  {
    stopwatchActive = true;
  }

  public static void StopStopwatch()
  {
    stopwatchActive = false;
  }

  void PredictTime()
  {
    userScore = TimeSpan.ParseExact(textTime.text, @"mm\:ss\:fff", System.Globalization.CultureInfo.InvariantCulture);
    
    Debug.Log("userScore: " + userScore);

    System.Random randomTime = new System.Random();

    var lowerLimit = new TimeSpan(0, 0, userScore.Minutes, userScore.Seconds - 5, 0);
    var upperLimit = new TimeSpan(0, 0, userScore.Minutes, userScore.Seconds + 5, 0);

    Debug.Log("lower: " + lowerLimit);
    Debug.Log("upper: " + upperLimit);

    pilot1Score = new TimeSpan(0, 0, userScore.Minutes, 
                                randomTime.Next(lowerLimit.Seconds, upperLimit.Seconds), 
                                randomTime.Next(0, 999)
                              );
    pilot2Score = new TimeSpan(0, 0, userScore.Minutes,
                                randomTime.Next(lowerLimit.Seconds, upperLimit.Seconds),
                                randomTime.Next(0, 999)
                              );
    pilot3Score = new TimeSpan(0, 0, userScore.Minutes,
                                randomTime.Next(lowerLimit.Seconds, upperLimit.Seconds),
                                randomTime.Next(0, 999)
                              );
    Debug.Log("pilot1: " + pilot1Score);
    Debug.Log("pilot2: " + pilot2Score);
    Debug.Log("pilot3: " + pilot3Score);
  }

  // bubble sort
  void CalculatePoint(List<PilotTime> pilotTimeArray)
  {
    int i;
    int j;
    PilotTime TempValue;

    for (i = (pilotTimeArray.Count - 1); i >= 0; i--)
    {
      for (j = 1; j <= i; j++)
      {
        if (pilotTimeArray[j - 1].time > pilotTimeArray[j].time)
        {
          TempValue = pilotTimeArray[j - 1];
          pilotTimeArray[j - 1] = pilotTimeArray[j];
          pilotTimeArray[j] = TempValue;
        }
      }
    }
  }

  void SetPilotScoreBoard()
  {
    pilotTimeArray[0].point = 25;
    pilotTimeArray[1].point = 18;
    pilotTimeArray[2].point = 15;
    pilotTimeArray[3].point = 12;

    pilotTimeArray[0].budget = 15000;
    pilotTimeArray[1].budget = 12500;
    pilotTimeArray[2].budget = 10000;
    pilotTimeArray[3].budget = 7500;

    foreach (var item in pilotTimeArray)
    {
      if (item.pilotId == int.Parse(Global.currentPilotId))
      {
        DatabaseInsertion.InsertTotblScoreBoard(item.point, userScore.ToString(), item.pilotId, int.Parse(Global.currentPilotTeamId), int.Parse(Global.currentFixture.id), int.Parse(Global.currentLeague.id), int.Parse(Global.currentSeason));
        var budget = int.Parse(Global.budget) + item.budget;
        Global.budget = budget.ToString();
        DatabaseUpdate.UpdateManagerTeamBudget(budget.ToString());
      }
      else
      {
        DatabaseInsertion.InsertTotblScoreBoard(item.point, item.time.ToString(), item.pilotId, item.teamId, int.Parse(Global.currentFixture.id), int.Parse(Global.currentLeague.id), int.Parse(Global.currentSeason));
      }
    }

    rowPilots.SetActive(true);

    int i = 1;
    var scoreBoard = DatabaseSelection.SelectCurrentPilotScoreBoardFromtblScoreBoard(int.Parse(Global.currentFixture.id));
    foreach (var item in scoreBoard)
    {
      newRow = Instantiate(rowPilots);
      newRow.tag = "rowClone";
      newRow.transform.SetParent(GameObject.Find("PilotTableScoreboard").transform, false);

      newRow.transform.Find("TextLine").GetComponent<Text>().text = i.ToString();
      newRow.transform.Find("TextName").GetComponent<Text>().text = item.pilotName.ToString();
      newRow.transform.Find("TextTeam").GetComponent<Text>().text = item.teamName.ToString();
      newRow.transform.Find("TextTime").GetComponent<Text>().text = item.time.ToString();
      newRow.transform.Find("TextPoint").GetComponent<Text>().text = item.point.ToString();

      i++;
    }
    rowPilots.SetActive(false);
  }

  void SetTeamScoreBoard()
  {
    pilotScoreBoard.SetActive(false);
    teamScoreBoard.SetActive(true);

    rowTeams.SetActive(true);

    int i = 1;
    var scoreBoard = DatabaseSelection.SelectCurrentTeamScoreBoardFromtblScoreBoard(int.Parse(Global.currentFixture.id));
    foreach (var item in scoreBoard)
    {
      newRow = Instantiate(rowTeams);
      newRow.tag = "rowClone";
      newRow.transform.SetParent(GameObject.Find("TeamTableScoreboard").transform, false);

      newRow.transform.Find("TextLine").GetComponent<Text>().text = i.ToString();
      newRow.transform.Find("TextName").GetComponent<Text>().text = item.teamName.ToString();
      newRow.transform.Find("TextPoint").GetComponent<Text>().text = item.point.ToString();

      i++;
    }
    rowTeams.SetActive(false);
  }

  void DestroyRow()
  {
    var clones = GameObject.FindGameObjectsWithTag("rowClone");
    foreach (var clone in clones)
    {
      Destroy(clone);
    }
  }

  void Finish()
  {
    StopStopwatch();
    PredictTime();
    Time.timeScale = 0;

    //DatabaseInsertion.InsertTotblScoreBoard(10, userScore.ToString(), int.Parse(Global.currentPilotId), int.Parse(Global.currentPilotTeamId), int.Parse(Global.currentFixture.id));
    pilotTimeArray.Add(new PilotTime { pilotId = int.Parse(Global.currentPilotId), teamId = int.Parse(Global.currentPilotTeamId), time = userScore, point = 0 });
    pilotTimeArray.Add(new PilotTime { pilotId = 5, teamId = 1, time = pilot1Score, point = 0 });
    pilotTimeArray.Add(new PilotTime { pilotId = 6, teamId = 1, time = pilot2Score, point = 0});
    pilotTimeArray.Add(new PilotTime { pilotId = 7, teamId = 2, time = pilot3Score, point = 0});

    CalculatePoint(pilotTimeArray);

    textTime.gameObject.SetActive(false);
    dropdownDrivingType.gameObject.SetActive(false);
    textGroundType.gameObject.SetActive(false);
    wheelHealthLF.gameObject.SetActive(false);
    wheelHealthLB.gameObject.SetActive(false);
    wheelHealthRB.gameObject.SetActive(false);
    wheelHealthRF.gameObject.SetActive(false);
    textEngineHealth.gameObject.SetActive(false);
    textFuelHealth.gameObject.SetActive(false);
    textTransmissionHealth.gameObject.SetActive(false);
    textTurboHealth.gameObject.SetActive(false);
    pilotScoreBoard.gameObject.SetActive(true);

    SetPilotScoreBoard();
  }

  private void OnCollisionEnter(Collision collision)
  {
    if (collision.gameObject.name == prefab.name)
    {
      Finish();
    }
  }

  void GoToLobby()
  {
    DatabaseUpdate.UpdateFixtureStatus(Global.currentFixture.id, true);
    DestroyRow();

    Time.timeScale = 1;

    pilotScoreBoard.SetActive(false);
    teamScoreBoard.SetActive(false);
    textTime.gameObject.SetActive(false);
    dropdownDrivingType.gameObject.SetActive(false);
    pilotScoreBoard.gameObject.SetActive(false);

    loading.gameObject.SetActive(true);
    loading.LoadLevel(3);
  }

  private void OnEnable()
  {
    goToTeamScoreBoard.onClick.AddListener(() => SetTeamScoreBoard());
    backToLobby.onClick.AddListener(() => GoToLobby());
  }
}
