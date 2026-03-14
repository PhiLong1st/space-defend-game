using UnityEngine;

[CreateAssetMenu(fileName = "SpaceShip_", menuName = "SpaceDefend/SpaceShip Config")]
public class SpaceshipConfig : ScriptableObject
{
  [Header("Identity")]
  [Tooltip("Unique identifier for this ship type")]
  public string ShipId;

  [Tooltip("Display name")]
  public string DisplayName;

  [Tooltip("Movement speed of the ship")]
  public float MovementSpeed = 5f;
}