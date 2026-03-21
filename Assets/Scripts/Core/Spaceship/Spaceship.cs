using UnityEngine;
using System.Collections.Generic;

public class Spaceship
{
  private readonly SpaceshipConfig _config;

  public int CurrentHealth { get; private set; }
  public int CurrentStamina { get; private set; }
  public float CurrentSpeed { get; private set; }
  public int CurrentLevel { get; private set; }
  public int CurrentExperience { get; private set; }
  public int MaxHealth { get; private set; }
  public int MaxStamina { get; private set; }
  public int ExperienceToNextLevel => Mathf.Max(1, Mathf.FloorToInt(CurrentLevel * _config.ExperienceMultiplier));
  public float Speed => CurrentSpeed * GameManager.Instance.WorldSpeed;

  public bool IsDestroyed => CurrentHealth <= 0;

  public Spaceship(SpaceshipConfig config)
  {
    _config = config;
    CurrentSpeed = config.MovementSpeed;
    CurrentHealth = config.Health;
    CurrentStamina = config.Stamina;
    MaxHealth = config.Health;
    MaxStamina = config.Stamina;
    CurrentLevel = 1;
    CurrentExperience = 0;
  }

  public void Heal(int amount)
  {
    if (amount <= 0 || IsDestroyed) return;
    CurrentHealth = Mathf.Min(CurrentHealth + amount, MaxHealth);
  }

  public void TakeDamage(int amount)
  {
    if (amount <= 0 || IsDestroyed) return;
    CurrentHealth = Mathf.Max(0, CurrentHealth - amount);
  }

  public bool UseStamina(int amount)
  {
    if (amount <= 0 || IsDestroyed) return false;
    if (CurrentStamina < amount) return false;

    CurrentStamina -= amount;
    return true;
  }

  public void RecoverStamina(int amount)
  {
    if (amount <= 0 || IsDestroyed) return;
    CurrentStamina = Mathf.Min(CurrentStamina + amount, MaxStamina);
  }

  public void GainExperience(int amount)
  {
    if (amount <= 0 || IsDestroyed) return;
    CurrentExperience = Mathf.Min(CurrentExperience + amount, ExperienceToNextLevel);
  }
  public bool IsAtMaxLevel() => CurrentLevel >= _config.MaxLevel;

  public bool CanLevelUp() => CurrentExperience >= ExperienceToNextLevel && !IsAtMaxLevel();

  public void LevelUp()
  {
    CurrentLevel++;
    CurrentExperience = 0;
    MaxHealth += _config.HealthIncrementPerLevel;
    MaxStamina += _config.StaminaIncrementPerLevel;
    Heal(MaxHealth);
    RecoverStamina(MaxStamina);
  }
}