using System.Collections.Generic;
using UnityEngine;

public class ProjectileSystem
{
  private List<ProjectileFactory> _factories;

  public ProjectileSystem()
  {
    _factories = new List<ProjectileFactory>();
  }

  public void AddProjectile(ProjectileFactory factory)
  {
    if (_factories.Contains(factory)) return;
    _factories.Add(factory);
  }

  public void RemoveProjectile(ProjectileFactory factory)
  {
    if (!_factories.Contains(factory)) return;
    _factories.Remove(factory);
  }

  public void Update()
  {
    foreach (var factory in _factories)
    {
      factory.ReduceCooldown(Time.deltaTime);
      if (factory.IsReady) factory.Spawn();
    }
  }
}