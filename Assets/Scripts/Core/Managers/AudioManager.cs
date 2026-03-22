using System;
using System.Collections.Generic;
using UnityEngine;

public enum AudioSFXEnum
{
  ButtonClick,
  Explosion,
  GameOver,
  KamikazeEngine,
  SparkLaunch,
  MeteorHit,
  MeteorExplosion,
}

public enum AudioMusicEnum
{
  MainMenu,
  InGame,
}

[Serializable]
public struct SoundSFXData
{
  [SerializeField]
  public AudioSFXEnum key;

  [SerializeField]
  public AudioClip clip;
}

[Serializable]
public struct SoundMusicData
{
  [SerializeField]
  public AudioMusicEnum key;

  [SerializeField]
  public AudioClip clip;
}

public class AudioManager : AbstractSingleton<AudioManager>
{
  [SerializeField] private AudioSource musicSource;
  [SerializeField] private AudioSource sfxSource;

  [Header("Audio Data")]
  [SerializeField] private SoundSFXData[] audioSFXClips;

  [SerializeField] private SoundMusicData[] audioMusicClips;

  [Header("Volume Settings")]
  [SerializeField] private float musicVolume = 1f;
  [SerializeField] private float sfxVolume = 1f;

  private Dictionary<AudioSFXEnum, SoundSFXData> audioSFXDictionary;
  private Dictionary<AudioMusicEnum, SoundMusicData> audioMusicDictionary;

  private void Start()
  {
    audioSFXDictionary = new Dictionary<AudioSFXEnum, SoundSFXData>();
    audioMusicDictionary = new Dictionary<AudioMusicEnum, SoundMusicData>();

    foreach (var soundData in audioSFXClips)
    {
      if (soundData.clip == null) continue;
      audioSFXDictionary[soundData.key] = soundData;
    }

    foreach (var soundData in audioMusicClips)
    {
      if (soundData.clip == null) continue;
      audioMusicDictionary[soundData.key] = soundData;
    }


    SetMusicVolume(musicVolume);
    SetSFXVolume(sfxVolume);
  }

  public void PlayMusic(AudioMusicEnum key)
  {
    if (!audioMusicDictionary.ContainsKey(key))
    {
      Debug.LogWarning($"Music key '{key}' not found in AudioManager");
      return;
    }
    var soundData = audioMusicDictionary[key];
    if (musicSource.isPlaying)
    {
      musicSource.Stop();
    }
    musicSource.clip = soundData.clip;
    musicSource.Play();
  }

  public void PlaySFX(AudioSFXEnum key)
  {
    if (!audioSFXDictionary.ContainsKey(key))
    {
      Debug.LogWarning($"SFX key '{key}' not found in AudioManager");
      return;
    }
    var soundData = audioSFXDictionary[key];
    sfxSource.PlayOneShot(soundData.clip, sfxVolume);
  }

  public void PlaySFX(AudioSFXEnum key, float customVolume)
  {
    if (!audioSFXDictionary.ContainsKey(key))
    {
      Debug.LogWarning($"SFX key '{key}' not found in AudioManager");
      return;
    }
    var soundData = audioSFXDictionary[key];
    sfxSource.PlayOneShot(soundData.clip, customVolume);
  }

  public void StopMusic()
  {
    if (musicSource != null && musicSource.isPlaying)
    {
      musicSource.Stop();
    }
  }

  public void StopSFX()
  {
    if (sfxSource != null && sfxSource.isPlaying)
    {
      sfxSource.Stop();
    }
  }

  public void PauseMusic()
  {
    if (musicSource != null && musicSource.isPlaying)
    {
      musicSource.Pause();
    }
  }

  public void ResumeMusic()
  {
    if (musicSource != null && !musicSource.isPlaying)
    {
      musicSource.UnPause();
    }
  }

  public void SetMusicVolume(float volume) => musicSource.volume = Mathf.Clamp01(volume);

  public void SetSFXVolume(float volume) => sfxSource.volume = Mathf.Clamp01(volume);

  public float GetMusicVolume() => musicSource.volume;

  public float GetSFXVolume() => sfxSource.volume;

  public void ToggleMusic() => musicSource.mute = !musicSource.mute;

  public void ToggleSFX() => sfxSource.mute = !sfxSource.mute;
}