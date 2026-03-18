using UnityEngine;

public class SaveManager : AbstractSingleton<SaveManager>
{
  const string BEST_SCORE = "BestScore";

  public int BestScore
  {
    get => PlayerPrefs.GetInt(BEST_SCORE, 0);
    set
    {
      if (value > BestScore)
      {
        PlayerPrefs.SetInt(BEST_SCORE, value);
        PlayerPrefs.Save();
      }
    }
  }
}