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

  public GameObject rowCars;
  public GameObject rowPilots;
  public GameObject beforeRaceRowPilots;
  public GameObject beforeRaceRowCars;

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

  public Text textFixtureId;
  public Text textFixtureName;
  public Text textFixtureDate;

  public Dropdown dropdownWheelType;

  public Loading loading;

  private void Start()
  {
    pilots = DatabaseSelection.SelectFromtblTeamPilots();
    cars = DatabaseSelection.SelectFromtblTeamCars();
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
        newRow.transform.GetComponentInChildren<Button>().onClick.AddListener(() => SelectPilotForRace(item.id));
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

  void PlayMatch()
  {
    Global.currentFixture = DatabaseSelection.SelectFromFixture(false);

    mainLobby.SetActive(false);
    beforeRace.SetActive(true);

    SetRaceInfo();
    SetPilotsToTable(beforeRaceRowPilots);
  }

  void CarFixShow()
  {
    DestroyRow();
    carFix.SetActive(true);
    carTable.SetActive(false);
  }

  void SetRaceInfo()
  {
    textFixtureId.text = Global.currentFixture.id;
    textFixtureName.text = Global.currentFixture.name;
    textFixtureDate.text = Global.currentFixture.date;
  }

  void SelectPilotForRace(string pilotId)
  {
    Global.currentPilotId = pilotId;

    beforeRaceTablePilots.SetActive(false);
    beforeRaceTableCars.SetActive(true);
    dropdownWheelType.gameObject.SetActive(true);

    SetCarsToTable(beforeRaceRowCars);
  }

  void SelectCarForRace(string carId)
  {
    Global.currentCarId = carId;

    beforeRaceTableCars.SetActive(false);
    buttonStartRace.gameObject.SetActive(true);
  }

  void StartRace()
  {
    buttonStartRace.gameObject.SetActive(false);
    dropdownWheelType.gameObject.SetActive(false);
    textFixtureId.gameObject.SetActive(false);
    textFixtureName.gameObject.SetActive(false);
    textFixtureDate.gameObject.SetActive(false);

    loading.gameObject.SetActive(true);
    loading.LoadLevel();
  }

  void GetWheelType()
  {
    Global.wheelType = dropdownWheelType.value;
    Debug.Log(Global.wheelType);
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
  }
}
