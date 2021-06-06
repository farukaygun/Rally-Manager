using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamCreation : MonoBehaviour
{
  public Button btnCreateTeam;
  public InputField inputFieldTeamName;
  public Dropdown dropdownGameDiffuculty;
  public Text textTeamBudget;

  string teamName;
  string diffuculty;
  int teamBudget;

  public void OnChangeInputField(InputField inputFieldName)
  {
    Debug.Log(inputFieldName.text);
    teamName = inputFieldTeamName.text;
    Debug.Log(teamName);
  }

  public void SetDropdownValue()
  {
    if (dropdownGameDiffuculty.value == 0)
    {
      textTeamBudget.text = "750.000$"; 
      teamBudget = 750000;
      diffuculty = "kolay";
    }
    else if (dropdownGameDiffuculty.value == 1)
    {
      textTeamBudget.text = "600.000$";
      teamBudget = 600000;
      diffuculty = "orta";
    }
    else if (dropdownGameDiffuculty.value == 2)
    {
      textTeamBudget.text = "500.000$";
      teamBudget = 500000;
      diffuculty = "zor";
    }
  }

  public void CreateTeam()
  {
    if (teamName != null || teamBudget > 0f || diffuculty != null)
    {
      DatabaseInsertion.InsertTotblTeam(teamName, null, null, teamBudget);
      DatabaseInsertion.InsertTotblGameSettings(diffuculty);
    }
  }

  public void OnEnable()
  {
    btnCreateTeam.onClick.AddListener(() => CreateTeam());
  }
}
