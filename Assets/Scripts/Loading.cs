using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
  public Slider progressBar;

  public void LoadLevel(int index)
  {
    StartCoroutine(StartLoading(index));
  }

  IEnumerator StartLoading(int index)
  {
    AsyncOperation async = SceneManager.LoadSceneAsync(index);

    while (!async.isDone)
    {
      progressBar.value = async.progress;

      yield return null;
    }
  }
}
