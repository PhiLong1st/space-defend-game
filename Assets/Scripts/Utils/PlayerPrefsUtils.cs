using UnityEngine;

public static class PlayerPrefsUtils
{
  public static T Read<T>(string key) where T : new()
  {
    return PlayerPrefs.HasKey(key)
            ? JsonUtility.FromJson<T>(PlayerPrefs.GetString(key))
            : new T();
  }

  public static void Write<T>(string key, T data) where T : new()
  {
    PlayerPrefs.SetString(key, JsonUtility.ToJson(data));
  }

  public static void Clear(string key)
  {
    PlayerPrefs.DeleteKey(key);
  }
}