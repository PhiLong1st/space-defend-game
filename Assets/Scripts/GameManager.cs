using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
  public static GameManager Instance;
  public bool isPausing = false;

  private float _worldSpeed = 1f;
  public float WorldSpeed => _worldSpeed;

  void Awake()
  {
    if (Instance != null)
    {
      Destroy(gameObject);
    }
    else
    {
      Instance = this;
    }
  }

  void Update()
  {
    if (Input.GetKeyDown(KeyCode.Escape))
    {
      Pause();
    }
  }

  public void Pause()
  {
    if (!isPausing)
    {
      isPausing = true;
      Time.timeScale = 0f;
      UIController.Instance.ShowPausePanel();
      // AudioManager.Instance.PlaySound(AudioManager.Instance.pause);
    }
    else
    {
      isPausing = false;
      UIController.Instance.HidePausePanel();
      Time.timeScale = 1f;
      //  AudioManager.Instance.PlaySound(AudioManager.Instance.unpause);
    }
  }

  public void QuitGame()
  {
    Application.Quit();
  }

  public void GoToMainMenu()
  {
    SceneManager.LoadScene("MainMenu");
  }
}