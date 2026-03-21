using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUIManager : AbstractSingleton<GameUIManager>
{
  [SerializeField] private GameObject _pausePanel;
  [SerializeField] private GameObject _lostPanel;

  [SerializeField] private TextMeshProUGUI _scoreText;
  [SerializeField] private TextMeshProUGUI _bestScoreText;

  private void Update()
  {
    int score = Mathf.RoundToInt(GameManager.Instance.Score);
    _scoreText.text = $"{score:00000}";
    _bestScoreText.text = $"BEST: {PlayerPrefs.GetInt("BestScore", 0):00000}";
  }

  public void ShowPausePanel()
  {
    _pausePanel.SetActive(true);
  }

  public void HidePausePanel()
  {
    _pausePanel.SetActive(false);
  }

  public void ShowLostPanel()
  {
    _lostPanel.SetActive(true);
  }

  public void HideLostPanel()
  {
    _lostPanel.SetActive(false);
  }
}