
/// <summary>
/// Pure data model for a ship instance.
/// No logic, just state. Can be serialized for save/load.
/// </summary>
public class ShipModel
{
  // Identity
  public int InstanceId;
  public string ShipTypeId;

  // Position (for save/load or network sync)
  public float PositionX;
  public float PositionY;

  // State
  public int CurrentLevel = 1;
  public bool IsActive = true;

  // Combat state
  public float TimeSinceLastShot = 0f;

  public ShipModel(int instanceId, string shipTypeId)
  {
    InstanceId = instanceId;
    ShipTypeId = shipTypeId;
  }
}