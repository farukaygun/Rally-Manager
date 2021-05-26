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
  float teamBudget;

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
      teamBudget = 750.000f;
      diffuculty = "kolay";
    }
    else if (dropdownGameDiffuculty.value == 1)
    {
      textTeamBudget.text = "600.000$";
      teamBudget = 600.000f;
      diffuculty = "orta";
    }
    else if (dropdownGameDiffuculty.value == 2)
    {
      textTeamBudget.text = "500.000$";
      teamBudget = 500.000f;
      diffuculty = "zor";
    }
  }

  public void CreateTeam()
  {
    if (teamName != null || teamBudget > 0f || diffuculty != null)
    {
      Database.InsertTotblTeam(teamName, null, null, teamBudget);
      Database.InsertTotblGameSettings(diffuculty);
    }
  }

  public void OnEnable()
  {
    btnCreateTeam.onClick.AddListener(() => CreateTeam());
  }
}
