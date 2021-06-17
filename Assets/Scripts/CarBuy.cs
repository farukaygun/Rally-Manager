using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarBuy : MonoBehaviour
{
  private List<Car> cars;
  public GameObject carRow1;
  public Text textBudget;


  void Start()
  {
    cars = DatabaseSelection.SelectFromtblCar();
    Global.budget = DatabaseSelection.SelectBudgetFromtblTeam();
    textBudget.text = "Bütçe: $" + Global.budget;
    SetToTable();
  }

  void SetToTable()
  {
    carRow1.transform.Find("TextName").GetComponent<Text>().text = cars[0].name;
    carRow1.transform.Find("TextMass").GetComponent<Text>().text = cars[0].mass;
    carRow1.transform.Find("TextSuspansionDistance").GetComponent<Text>().text = cars[0].suspansionDistance;
    carRow1.transform.Find("TextHorsePower").GetComponent<Text>().text = cars[0].horsePower;
    carRow1.transform.Find("TextPrice").GetComponent<Text>().text = "$" + cars[0].price;
  }

  void BuyCar(int id, int price, GameObject row)
  {
    Global.budget = (int.Parse(Global.budget) - price).ToString();

    DatabaseUpdate.UpdateManagerTeamBudget(Global.budget);
    DatabaseUpdate.UpdateManagerTeamCar("3", id);

    textBudget.text = "Bütçe: $" + Global.budget;
    carRow1.transform.Find("Button (1)").GetComponent<Button>().enabled = false;
  }

  private void OnEnable()
  {
    carRow1.transform.Find("Button (1)").GetComponent<Button>().onClick.AddListener(() => BuyCar(int.Parse(cars[0].id), int.Parse(cars[0].price), carRow1));  
  }
}
