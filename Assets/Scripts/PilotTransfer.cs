using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using UnityEngine.UI;

public class PilotTransfer : MonoBehaviour
{
  private List<Pilot> pilots;
  public GameObject row1;
  public GameObject row2;
  public GameObject row3;
  public GameObject row4;

  public Text textBudget;
  
  string budget;


  void Start()
  {
    pilots = DatabaseSelection.SelectFromtblPilot();
    budget = DatabaseSelection.SelectBudgetFromtblTeam();

    textBudget.text = "Bütçe: $" + budget;
    
    SetToTable();
  }

  void SetToTable()
  {
    row1.transform.Find("TextName").GetComponent<Text>().text = pilots[0].name;
    row1.transform.Find("TextAge").GetComponent<Text>().text = pilots[0].age;
    row1.transform.Find("TextAbility").GetComponent<Text>().text = pilots[0].abilityPoint;
    row1.transform.Find("TextPotantial").GetComponent<Text>().text = pilots[0].potantial;
    row1.transform.Find("TextSalary").GetComponent<Text>().text = "$" + pilots[0].salary;

    row2.transform.Find("TextName").GetComponent<Text>().text = pilots[1].name;
    row2.transform.Find("TextAge").GetComponent<Text>().text = pilots[1].age;
    row2.transform.Find("TextAbility").GetComponent<Text>().text = pilots[1].abilityPoint;
    row2.transform.Find("TextPotantial").GetComponent<Text>().text = pilots[1].potantial;
    row2.transform.Find("TextSalary").GetComponent<Text>().text = "$" + pilots[1].salary;

    row3.transform.Find("TextName").GetComponent<Text>().text = pilots[2].name;
    row3.transform.Find("TextAge").GetComponent<Text>().text = pilots[2].age;
    row3.transform.Find("TextAbility").GetComponent<Text>().text = pilots[2].abilityPoint;
    row3.transform.Find("TextPotantial").GetComponent<Text>().text = pilots[2].potantial;
    row3.transform.Find("TextSalary").GetComponent<Text>().text = "$" + pilots[2].salary;

    row4.transform.Find("TextName").GetComponent<Text>().text = pilots[3].name;
    row4.transform.Find("TextAge").GetComponent<Text>().text = pilots[3].age;
    row4.transform.Find("TextAbility").GetComponent<Text>().text = pilots[3].abilityPoint;
    row4.transform.Find("TextPotantial").GetComponent<Text>().text = pilots[3].potantial;
    row4.transform.Find("TextSalary").GetComponent<Text>().text = "$" + pilots[3].salary;
  }

  void SignContractWithPilot(int id, int salary, GameObject row)
  {
    budget = (int.Parse(budget) - salary).ToString();

    DatabaseUpdate.UpdateManagerTeamBudget(budget); 
    DatabaseUpdate.UpdateManagerTeamPilot(1, id); 

    textBudget.text = "Bütçe: $" + budget;

    row.transform.Find("Button (1)").GetComponent<Button>().enabled = false;
  }

  private void OnEnable()
  {
    row1.transform.Find("Button (1)").GetComponent<Button>().onClick.AddListener(() => SignContractWithPilot(int.Parse(pilots[0].id), int.Parse(pilots[0].salary), row1));
    row2.transform.Find("Button (1)").GetComponent<Button>().onClick.AddListener(() => SignContractWithPilot(int.Parse(pilots[1].id), int.Parse(pilots[1].salary), row2));
    row3.transform.Find("Button (1)").GetComponent<Button>().onClick.AddListener(() => SignContractWithPilot(int.Parse(pilots[2].id), int.Parse(pilots[2].salary), row3));
    row4.transform.Find("Button (1)").GetComponent<Button>().onClick.AddListener(() => SignContractWithPilot(int.Parse(pilots[3].id), int.Parse(pilots[3].salary), row4));
  }
}
