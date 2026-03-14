using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUIManager : MonoBehaviour
{
  public static GameUIManager Instance { get; private set; }

  private void Awake()
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

  [SerializeField] private GameObject _pausePanel;
  [SerializeField] private GameObject _lostPanel;

  [SerializeField] private TextMeshProUGUI _scoreText;
  [SerializeField] private TextMeshProUGUI _bestScoreText;

  private void Update()
  {
    _scoreText.text = $"{GameManager.Instance.Score:00000}";
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