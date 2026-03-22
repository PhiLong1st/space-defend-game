using UnityEngine;

[CreateAssetMenu(fileName = "MeteorConfig", menuName = "SpaceDefend/Meteor Config")]
public class MeteorConfig : ScriptableObject
{
  [Header("Meteor Properties")]
  [Tooltip("Unique identifier for this meteor type")]
  public string MeteorId;

  [Tooltip("Display name of the meteor")]
  public string DisplayName;

  [Tooltip("Damage dealt by the meteor upon impact")]
  public int Damage = 10;

  [Tooltip("Health points of the meteor")]
  public int Health = 100;

  [Tooltip("Minimum speed of the meteor")]
  public float MinSpeed = 1f;

  [Tooltip("Maximum speed of the meteor")]
  public float MaxSpeed = 3f;

  [Tooltip("Minimum scale of the meteor")]
  public float MinScale = 2f;

  [Tooltip("Maximum scale of the meteor")]
  public float MaxScale = 3f;
}