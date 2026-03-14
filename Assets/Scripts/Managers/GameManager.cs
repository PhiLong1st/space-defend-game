using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
  public static GameManager Instance;
  public bool isPausing = false;

  private float _worldSpeed = 1f;
  public float WorldSpeed => _worldSpeed;

  private int _score = 0;
  public int Score => _score;

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
      _score += 1;
      if (_score % 500 == 0 && _score > 0)
      {
        _worldSpeed = Mathf.Max(_worldSpeed, 2f);
      }
    }

    if (Input.GetKeyDown(KeyCode.Escape))
    {
      Pause();
    }

    if (Input.GetKeyDown(KeyCode.L))
    {
      GameOver();
    }

    if (Input.GetKeyDown(KeyCode.R))
    {
      Restart();
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

    DataManager.Instance.BestScore = Score;
  }

  public void Restart()
  {
    Time.timeScale = 1f;
    isPausing = false;
    SceneLoader.LoadScene(SceneManager.GetActiveScene().name);
  }

  public void QuitGame()
  {
    Application.Quit();
  }

  public void GoToMainMenu()
  {
    // 
  }

  public void GoToSettings()
  {
    // 
  }
}