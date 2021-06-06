using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
  public Slider progressBar;

  public void LoadLevel()
  {
    StartCoroutine(StartLoading());
  }

  IEnumerator StartLoading()
  {
    AsyncOperation async = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);

    while (!async.isDone)
    {
      progressBar.value = async.progress;

      yield return null;
    }
  }
}
