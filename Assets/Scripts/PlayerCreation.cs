using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCreation : MonoBehaviour
{
  public Button btnCreateManager;

  // azalt
  public Button btnDcPrsnMngmnt;
  public Button btnDcMotivation;
  public Button btnDcAdaptation;
  public Button btnDcDecisiveness;

  // arttır
  public Button btnIncPrsnMngmnt;
  public Button btnIncMotivation;
  public Button btnIncAdaptation;
  public Button btnIncDecisiveness;

  // textbox
  public InputField inputFieldName;
  public InputField inputFieldSurname;
  public InputField inputFieldAge;

  // label
  public Text textSkillPoints;
  public Text textPersonManagement;
  public Text textMotivation;
  public Text textAdaptation;
  public Text textDecisiveness;

  int avaliableSkillPoints = 20;
  int personManagement = 5;
  int motivation = 5;
  int adaptation = 5;
  int decisiveness = 5;

  string playerName = "";
  string playerSurname = "";
  int playerAge = 0;

  public void IncreaseSkillPoint(Text name, ref int skill)
  {
    if (avaliableSkillPoints > 0)
    {
      Debug.Log("skill: " + skill);
      skill += 1;
      Debug.Log("skill: " + skill);
      avaliableSkillPoints -= 1;

      name.text = skill.ToString();

      textSkillPoints.text = avaliableSkillPoints.ToString();
      name.text = skill.ToString();
    }
  }

  public void DecreaseSkillPoint(Text name, ref int skill)
  {
    if (skill > 0)
    {
      skill--;
      avaliableSkillPoints += 1;

      textSkillPoints.text = avaliableSkillPoints.ToString();
      name.text = skill.ToString();
    }
  }

  public void OnChangeInputField(InputField inputFieldName)
  {
    if (inputFieldName.name == "InputFieldName")
    {
      playerName = inputFieldName.text;
      Debug.Log(playerName);
    }
    else if (inputFieldName.name == "InputFieldSurname")
    {
      playerSurname = inputFieldName.text;
      Debug.Log(playerSurname);
    }
    else if (inputFieldName.name == "InputFieldAge")
    {
      playerAge = int.Parse(inputFieldName.text);
      Debug.Log(playerAge);
    }
  }

  public void CreateManager()
  {
    if (playerName != null || playerSurname != null || playerAge > 0)
    {
      Database.InsertTotblManager(playerName, playerSurname, playerAge);
    }
    else Debug.Log("hata 1!");
    if (avaliableSkillPoints == 0)
    {
      Database.InsertTotblManagerSkills(avaliableSkillPoints, personManagement, motivation, adaptation, decisiveness);
    }
    else Debug.Log("hata 2!");
  }

  public void OnEnable()
  {
    btnDcPrsnMngmnt.onClick.AddListener(() => DecreaseSkillPoint(textPersonManagement, ref personManagement));
    btnIncPrsnMngmnt.onClick.AddListener(() => IncreaseSkillPoint(textPersonManagement, ref personManagement));

    btnDcAdaptation.onClick.AddListener(() => DecreaseSkillPoint(textAdaptation, ref adaptation));
    btnIncAdaptation.onClick.AddListener(() => IncreaseSkillPoint(textAdaptation, ref adaptation));

    btnDcMotivation.onClick.AddListener(() => DecreaseSkillPoint(textMotivation, ref motivation));
    btnIncMotivation.onClick.AddListener(() => IncreaseSkillPoint(textMotivation, ref motivation));

    btnDcDecisiveness.onClick.AddListener(() => DecreaseSkillPoint(textDecisiveness, ref decisiveness));
    btnIncDecisiveness.onClick.AddListener(() => IncreaseSkillPoint(textDecisiveness, ref decisiveness));

    btnCreateManager.onClick.AddListener(() => CreateManager());
  }
}