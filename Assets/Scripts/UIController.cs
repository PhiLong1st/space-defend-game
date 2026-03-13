using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
  public static UIController Instance;
  [SerializeField] private GameObject _losePanel;
  [SerializeField] private GameObject _winPanel;
  [SerializeField] private GameObject _pausePanel;

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

  public void ShowLosePanel()
  {
    _losePanel.SetActive(true);
  }

  public void ShowWinPanel()
  {
    _winPanel.SetActive(true);
  }

  public void ShowPausePanel()
  {
    _pausePanel.SetActive(true);
  }

  public void HidePausePanel()
  {
    _pausePanel.SetActive(false);
  }

  public void HideLosePanel()
  {
    _losePanel.SetActive(false);
  }

  public void HideWinPanel()
  {
    _winPanel.SetActive(false);
  }
}