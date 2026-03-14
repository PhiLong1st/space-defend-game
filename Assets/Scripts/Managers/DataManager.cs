using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataManager
{
  private static DataManager _instance;
  public static DataManager Instance
  {
    get
    {
      if (_instance == null)
      {
        _instance = new DataManager();
      }
      return _instance;
    }
  }

  public int BestScore
  {
    get => PlayerPrefs.GetInt("BestScore", 0);
    set
    {
      if (value > BestScore)
      {
        PlayerPrefs.SetInt("BestScore", value);
        PlayerPrefs.Save();
      }
    }
  }
}