using UnityEngine;

[CreateAssetMenu(fileName = "KamikazeTrap_", menuName = "SpaceDefend/KamikazeTrapConfig")]
public class KamikazeTrapConfig : ScriptableObject
{

  [Header("Kamikaze Trap Identity")]
  [Tooltip("Display name")]
  public string DisplayName;

  [Header("Kamikaze Trap Stats")]
  [Tooltip("Damage dealt to the spaceship upon explosion")]
  public int Damage = 10;

  [Tooltip("Health of the kamikaze trap")]
  public int Health = 50;

  [Tooltip("Speed at which the kamikaze trap moves")]
  public float Speed = 2f;

  [Tooltip("Warning Speed at which the kamikaze trap moves")]
  public float WarningSpeed = 1f;
}