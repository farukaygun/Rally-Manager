using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lobby : MonoBehaviour
{
  public GameObject mainLobby;
  public GameObject pilotTable;
  public GameObject carTable;
  public GameObject carFix;
  public GameObject beforeRace;
  public GameObject beforeRaceTablePilots;
  public GameObject beforeRaceTableCars;
  public GameObject scoreBoard;
  public GameObject pilotScoreBoard;
  public GameObject teamScoreBoard;
  public GameObject leagueFinished;

  public GameObject rowCars;
  public GameObject rowPilots;
  public GameObject beforeRaceRowPilots;
  public GameObject beforeRaceRowCars;
  public GameObject rowTeamsScoreBoard;
  public GameObject rowPilotsScoreBoard;

  private GameObject newRow;

  private List<Pilot> pilots;
  private List<Car> cars;

  public Button buttonMyCars;
  public Button buttonMyPilots;
  public Button backToLobbyFromCars;
  public Button backToLobbyFromPilots;
  public Button backToLobbyFromCarFix;
  public Button playButton;
  public Button buttonRepair;
  public Button buttonStartRace;
  public Button buttonExit;
  public Button buttonScoreBoard;
  public Button buttonShowPilotScoreBoard;
  public Button buttonShowTeamScoreBoard;
  public Button buttonGoToLobbyFromScoreBoard;

  public Button buttonYes;
  public Button buttonNo;

  public Text textFixtureId;
  public Text textFixtureName;
  public Text textFixtureDate;
  public Text textLeagueName;
  public Text textBudget;

  public Dropdown dropdownWheelType;

  public Loading loading;

  private void Start()
  {
    pilots = DatabaseSelection.SelectFromtblTeamPilots();
    cars = DatabaseSelection.SelectFromtblTeamCars();
    Global.currentLeague = DatabaseSelection.SelectFromTblLeagues();
    Global.currentSeason = DatabaseSelection.SelectFromTblSeason();
    Global.budget = DatabaseSelection.SelectBudgetFromtblTeam();
    textBudget.text = "Bütçe: $" + Global.budget;
  }

  void SetPilotsToTable(GameObject rowPilots)
  {
    rowPilots.SetActive(true);
    foreach (var item in pilots)
    {
      newRow = Instantiate(rowPilots);
      newRow.tag = "rowClone";
      newRow.transform.SetParent(GameObject.Find("TablePilots").transform, false);

      if (rowPilots == beforeRaceRowPilots)
      {
        newRow.transform.GetComponentInChildren<Button>().onClick.AddListener(() => SelectPilotForRace(item.id, item.teamID));
      }

      newRow.transform.Find("TextName").GetComponent<Text>().text = item.name;
      newRow.transform.Find("TextAge").GetComponent<Text>().text = item.age;
      newRow.transform.Find("TextAbility").GetComponent<Text>().text = item.abilityPoint;
      newRow.transform.Find("TextPotantial").GetComponent<Text>().text = item.potantial;
      if (this.rowPilots == rowPilots)
      {
        newRow.transform.Find("TextSalary").GetComponent<Text>().text = "$" + item.salary;
      }
    }
    rowPilots.SetActive(false);
  }

  void SetCarsToTable(GameObject rowCars)
  {
    rowCars.SetActive(true);
    foreach (var item in cars)
    {
      newRow = Instantiate(rowCars);
      newRow.tag = "rowClone";
      newRow.transform.SetParent(GameObject.Find("TableCars").transform, false);
      if (this.rowCars == rowCars)
      {
        newRow.transform.GetComponentInChildren<Button>().onClick.AddListener(() => CarFixShow());
      } 
      else
      {
        newRow.transform.GetComponentInChildren<Button>().onClick.AddListener(() => SelectCarForRace(item.id));
      }

      newRow.transform.Find("TextName").GetComponent<Text>().text = item.name;
      newRow.transform.Find("TextMass").GetComponent<Text>().text = item.mass;
      newRow.transform.Find("TextSuspansionDistance").GetComponent<Text>().text = item.suspansionDistance;
      newRow.transform.Find("TextHorsePower").GetComponent<Text>().text = item.horsePower;
      // newRow.transform.Find("TextPrice").GetComponent<Text>().text = "$" + item.price;
    }
    rowCars.SetActive(false);
  }

  void DestroyRow()
  {
    var clones = GameObject.FindGameObjectsWithTag("rowClone");
    foreach (var clone in clones){
      Destroy(clone);
    }
  }

  void BackToLobbyFromCar()
  {
    DestroyRow();
    carTable.SetActive(false);
    mainLobby.SetActive(true);
  }

  void BackToLobbyFromPilot()
  {
    DestroyRow();
    pilotTable.SetActive(false);
    mainLobby.SetActive(true);
  }

  void BackToLobbyFromCarFix()
  {    
    carFix.SetActive(false);
    carTable.SetActive(true);
    
    SetCarsToTable(rowCars);
  }

  void BackToLobbyFromScoreBoard()
  {
    pilotScoreBoard.SetActive(true);
    teamScoreBoard.SetActive(false);
    scoreBoard.SetActive(false);
    mainLobby.SetActive(true);
  }

  void PlayMatch()
  {
    Global.currentFixture = DatabaseSelection.SelectFromFixture(false);

    if (Global.currentFixture.name != "Sezon Sonu")
    {
      mainLobby.SetActive(false);
      beforeRace.SetActive(true);

      SetRaceInfo();
      SetPilotsToTable(beforeRaceRowPilots);
    }
    else
    {
      mainLobby.SetActive(false);
      leagueFinished.SetActive(true);
    }
  }

  void CarFixShow()
  {
    DestroyRow();
    carFix.SetActive(true);
    carTable.SetActive(false);
  }

  void SetRaceInfo()
  {
    textLeagueName.text = Global.currentLeague.name;
    textFixtureId.text = Global.currentFixture.id;
    textFixtureName.text = Global.currentFixture.name;
    textFixtureDate.text = Global.currentFixture.date;
  }

  void SelectPilotForRace(string pilotId, string teamId)
  {
    Global.currentPilotId = pilotId;
    Global.currentPilotTeamId = teamId;

    beforeRaceTablePilots.SetActive(false);
    beforeRaceTableCars.SetActive(true);
    dropdownWheelType.gameObject.SetActive(true);

    SetCarsToTable(beforeRaceRowCars);
  }

  void SelectCarForRace(string carId)
  {
    Global.currentCarId = carId;

    beforeRaceTableCars.SetActive(false);
    dropdownWheelType.gameObject.SetActive(false);
    buttonStartRace.gameObject.SetActive(true);
  }

  void StartRace()
  {
    buttonStartRace.gameObject.SetActive(false);
    textFixtureId.gameObject.SetActive(false);
    textFixtureName.gameObject.SetActive(false);
    textFixtureDate.gameObject.SetActive(false);

    loading.gameObject.SetActive(true);
    loading.LoadLevel(4);
  }

  void GetWheelType()
  {
    Global.wheelType = dropdownWheelType.value;
  }

  void ShowScoreBoard()
  {
    //teamScoreboard.SetActive(true);
    mainLobby.SetActive(false);
    scoreBoard.SetActive(true);
    teamScoreBoard.SetActive(true);
    pilotScoreBoard.SetActive(false);

    SetTeamScoreBoard();
  }

  void ShowPilotScoreBoard()
  {
    pilotScoreBoard.SetActive(true);
    teamScoreBoard.SetActive(false);

    SetPilotScoreBoard();
  }

  void ShowTeamScoreBoard()
  {
    teamScoreBoard.SetActive(true);
    pilotScoreBoard.SetActive(false);

    SetTeamScoreBoard();
  }

  void SetPilotScoreBoard()
  {
    teamScoreBoard.SetActive(false);
    pilotScoreBoard.SetActive(true);
    
    rowPilotsScoreBoard.SetActive(true);

    DestroyRow();

    int i = 1;
    var scoreBoard = DatabaseSelection.SelectPilotScoreBoardFromtblScoreBoard(int.Parse(Global.currentSeason));
    foreach (var item in scoreBoard)
    {
      newRow = Instantiate(rowPilotsScoreBoard);
      newRow.tag = "rowClone";
      newRow.transform.SetParent(GameObject.Find("PilotTableScoreboard").transform, false);

      newRow.transform.Find("TextLine").GetComponent<Text>().text = i.ToString();
      newRow.transform.Find("TextName").GetComponent<Text>().text = item.pilotName.ToString();
      newRow.transform.Find("TextTeam").GetComponent<Text>().text = item.teamName.ToString();
      newRow.transform.Find("TextPoint").GetComponent<Text>().text = item.point.ToString();

      i++;
    }
    rowPilotsScoreBoard.SetActive(false);
  }

  void SetTeamScoreBoard()
  {
    pilotScoreBoard.SetActive(false);
    teamScoreBoard.SetActive(true);

    rowTeamsScoreBoard.SetActive(true);

    DestroyRow();

    int i = 1;
    var scoreBoard = DatabaseSelection.SelectTeamScoreBoardFromtblScoreBoard(int.Parse(Global.currentSeason));
    foreach (var item in scoreBoard)
    {
      newRow = Instantiate(rowTeamsScoreBoard);
      newRow.tag = "rowClone";
      newRow.transform.SetParent(GameObject.Find("TeamTableScoreboard").transform, false);

      newRow.transform.Find("TextLine").GetComponent<Text>().text = i.ToString();
      newRow.transform.Find("TextName").GetComponent<Text>().text = item.teamName.ToString();
      newRow.transform.Find("TextPoint").GetComponent<Text>().text = item.point.ToString();

      i++;
    }
    rowTeamsScoreBoard.SetActive(false);
  }

  void QuitApp()
  {
    Application.Quit();
  }

  void GoToUpLeague()
  {
    DatabaseInsertion.InsertTotblFixture("Türkiye", "Etap 1", false);
    DatabaseInsertion.InsertTotblFixture("Monte Carlo", "Etap 2", false);
    DatabaseInsertion.InsertTotblFixture("Sezon Sonu", "Sezon Sonu", false);

    DatabaseUpdate.UpdateFixtureStatus(Global.currentFixture.id, true);

    DatabaseUpdate.UpdateLeagueStatus(Global.currentLeague.id, false);
    var leagueId = int.Parse(Global.currentLeague.id) + 1;
 
    DatabaseUpdate.UpdateLeagueStatus(leagueId.ToString(), true);
    Global.currentLeague.id = leagueId.ToString();

    textLeagueName.text = Global.currentLeague.name;

    var budget = int.Parse(Global.budget) - 50000;
    DatabaseUpdate.UpdateManagerTeamBudget(budget.ToString());
    textBudget.text = budget.ToString();

    DatabaseInsertion.InsertTotblSeason("season");
    Global.currentSeason = DatabaseSelection.SelectFromTblSeason();

    leagueFinished.SetActive(false);
    mainLobby.SetActive(true);
  }

  void GoWithSameLeague()
  {
    DatabaseInsertion.InsertTotblFixture("Türkiye", "Etap 1", false);
    DatabaseInsertion.InsertTotblFixture("Monte Carlo", "Etap 2", false);
    DatabaseInsertion.InsertTotblFixture("Sezon Sonu", "Sezon Sonu", false);

    DatabaseUpdate.UpdateFixtureStatus(Global.currentFixture.id, true);

    DatabaseInsertion.InsertTotblSeason("season");
    Global.currentSeason = DatabaseSelection.SelectFromTblSeason();

    leagueFinished.SetActive(false);
    mainLobby.SetActive(true);
  }

  private void OnEnable()
  {
    buttonMyCars.onClick.AddListener(() => SetCarsToTable(rowCars));
    buttonMyPilots.onClick.AddListener(() => SetPilotsToTable(rowPilots));
    backToLobbyFromCars.onClick.AddListener(() => BackToLobbyFromCar());
    backToLobbyFromPilots.onClick.AddListener(() => BackToLobbyFromPilot());
    backToLobbyFromCarFix.onClick.AddListener(() => BackToLobbyFromCarFix());
    playButton.onClick.AddListener(() => PlayMatch());
    buttonRepair.onClick.AddListener(() => CarFixShow());
    buttonStartRace.onClick.AddListener(() => StartRace());
    dropdownWheelType.onValueChanged.AddListener(delegate { GetWheelType(); });
    buttonScoreBoard.onClick.AddListener(() => ShowScoreBoard());
    buttonShowPilotScoreBoard.onClick.AddListener(() => ShowPilotScoreBoard());
    buttonShowTeamScoreBoard.onClick.AddListener(() => ShowTeamScoreBoard());
    buttonGoToLobbyFromScoreBoard.onClick.AddListener(() => BackToLobbyFromScoreBoard());
    buttonYes.onClick.AddListener(() => GoToUpLeague());
    buttonNo.onClick.AddListener(() => GoWithSameLeague());
    buttonExit.onClick.AddListener(() => QuitApp());
  }
}
