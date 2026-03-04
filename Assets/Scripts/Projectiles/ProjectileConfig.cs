using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileConfig", menuName = "ProjectileConfig")]
public class ProjectileConfig : ScriptableObject
{
  public float speed;
  public int damage;
}