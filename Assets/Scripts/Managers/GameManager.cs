using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
  public static GameManager Instance;
  public bool isPausing = false;

  private float _worldSpeed = 1f;
  public float WorldSpeed => _worldSpeed;

  private float _score = 0f;
  public float Score => _score;

  private float _scoreMilestone = 200f;

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

  private void Start()
  {
    Time.timeScale = 1f;
    _score = 0;
    _worldSpeed = 1f;
  }

  void Update()
  {
    if (!isPausing)
    {
      _score += 10 * Time.deltaTime;
      if (_score >= _scoreMilestone)
      {
        _worldSpeed = Mathf.Min(_worldSpeed + 0.5f, 2f);
        _scoreMilestone = Mathf.Min(_scoreMilestone * 2f, 10000f);
      }
    }
  }

  public void Pause()
  {
    if (!isPausing)
    {
      isPausing = true;
      Time.timeScale = 0f;
      GameUIManager.Instance.ShowPausePanel();
    }
    else
    {
      isPausing = false;
      GameUIManager.Instance.HidePausePanel();
      Time.timeScale = 1f;
    }
  }

  public void GameOver()
  {
    isPausing = true;

    Time.timeScale = 0f;
    GameUIManager.Instance.ShowLostPanel();
    AudioManager.Instance.PlaySFX(AudioSFXEnum.GameOver);

    DataManager.Instance.BestScore = Mathf.RoundToInt(Score);
  }

  public void Restart()
  {
    Time.timeScale = 1f;
    isPausing = false;
    SceneLoader.LoadScene(SceneManager.GetActiveScene().name);

    GameUIManager.Instance.HidePausePanel();
    GameUIManager.Instance.HideLostPanel();
  }

  public void QuitGame()
  {
    Application.Quit();
  }

  public void GoToMainMenu()
  {
    SceneLoader.LoadScene("Main_Menu");
  }

  public void GoToSettings()
  {
    // Implementation for going to settings
  }
}