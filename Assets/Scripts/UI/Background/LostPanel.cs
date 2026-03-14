using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LostPanel : MonoBehaviour
{
  [SerializeField] private TextMeshProUGUI _scoreText;
  [SerializeField] private TextMeshProUGUI _bestScoreText;

  private void Start()
  {
    _scoreText.text = $"SCORE: {GameManager.Instance.Score:00000}";
    _bestScoreText.text = $"BEST: {PlayerPrefs.GetInt("BestScore", 0):00000}";
  }
}