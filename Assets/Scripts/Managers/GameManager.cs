using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
  public static GameManager Instance;
  public bool isPausing = false;

  private float _worldSpeed = GameData.InitialWorldSpeed;
  public float WorldSpeed => _worldSpeed;

  private float _score = GameData.InitialScore;
  public float Score => _score;

  private float _scoreMilestone = GameData.InitialScoreMilestone;

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
    Time.timeScale = GameData.NormalTimeScale;
    _score = GameData.InitialScore;
    _worldSpeed = GameData.InitialWorldSpeed;
    _scoreMilestone = GameData.InitialScoreMilestone;
  }

  void Update()
  {
    if (!isPausing)
    {
      _score += GameData.ScorePerSecond * Time.deltaTime;
      if (_score >= _scoreMilestone)
      {
        _worldSpeed = Mathf.Min(_worldSpeed + GameData.WorldSpeedIncrement, GameData.MaxWorldSpeed);
        _scoreMilestone = Mathf.Min(_scoreMilestone * GameData.ScoreMilestoneMultiplier, GameData.MaxScoreMilestone);
      }
    }
  }

  public void Pause()
  {
    if (!isPausing)
    {
      isPausing = true;
      Time.timeScale = GameData.PausedTimeScale;
      GameUIManager.Instance.ShowPausePanel();
    }
    else
    {
      isPausing = false;
      GameUIManager.Instance.HidePausePanel();
      Time.timeScale = GameData.NormalTimeScale;
    }
  }

  public void GameOver()
  {
    isPausing = true;

    Time.timeScale = GameData.PausedTimeScale;
    GameUIManager.Instance.ShowLostPanel();
    AudioManager.Instance.PlaySFX(AudioSFXEnum.GameOver);

    DataManager.Instance.BestScore = Mathf.RoundToInt(Score);
  }

  public void Restart()
  {
    Time.timeScale = GameData.NormalTimeScale;
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