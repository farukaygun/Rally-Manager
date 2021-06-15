using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
  public float timeRemaining = 3;
  public bool timerIsRunning = false;
  public Text textCountDown;

  private void Start()
  {
    // Starts the timer automatically
    timerIsRunning = true;
    CarEngine.maxSpeed = 0;
  }

  void Update()
  {
    if (timerIsRunning)
    {
      if (timeRemaining > 0)
      {
        timeRemaining -= Time.deltaTime;
        DisplayTime(timeRemaining);
      }
      else
      {
        timeRemaining = 0;
        timerIsRunning = false;
        CarEngine.maxSpeed = 100;
        textCountDown.gameObject.SetActive(false);
      }
    }
  }

  void DisplayTime(float timeToDisplay)
  {
    timeToDisplay += 1;

    float seconds = Mathf.FloorToInt(timeToDisplay % 60);

    textCountDown.text = seconds.ToString();
  }
}