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
  public GameObject rowCars;
  public GameObject rowPilots;
  private GameObject newRow;

  private List<Pilot> pilots;
  private List<Car> cars;

  public Button buttonMyCars;
  public Button buttonMyPilots;
  public Button backToLobbyFromCars;
  public Button backToLobbyFromPilots;
  public Button backToLobbyFromCarFix;

  private void Start()
  {
    pilots = DatabaseSelection.SelectFromtblTeamPilots();
    cars = DatabaseSelection.SelectFromtblTeamCars();
  }

  void SetPilotsToTable()
  {
    rowPilots.SetActive(true);
    foreach (var item in pilots)
    {
      newRow = Instantiate(rowPilots);
      newRow.tag = "rowClone";
      newRow.transform.SetParent(GameObject.Find("TablePilots").transform, false);
      

      newRow.transform.Find("TextName").GetComponent<Text>().text = item.name;
      newRow.transform.Find("TextAge").GetComponent<Text>().text = item.age;
      newRow.transform.Find("TextAbility").GetComponent<Text>().text = item.abilityPoint;
      newRow.transform.Find("TextPotantial").GetComponent<Text>().text = item.potantial;
      newRow.transform.Find("TextSalary").GetComponent<Text>().text = "$" + item.salary;
    }
    rowPilots.SetActive(false);
  }

  void SetCarsToTable()
  {
    rowCars.SetActive(true);
    foreach (var item in cars)
    {
      newRow = Instantiate(rowCars);
      newRow.tag = "rowClone";
      newRow.transform.SetParent(GameObject.Find("TableCars").transform, false);

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
  }

  private void OnEnable()
  {
    buttonMyCars.onClick.AddListener(() => SetCarsToTable());
    buttonMyPilots.onClick.AddListener(() => SetPilotsToTable());
    backToLobbyFromCars.onClick.AddListener(() => BackToLobbyFromCar());
    backToLobbyFromPilots.onClick.AddListener(() => BackToLobbyFromPilot());
    backToLobbyFromCarFix.onClick.AddListener(() => BackToLobbyFromCarFix());
  }
}
