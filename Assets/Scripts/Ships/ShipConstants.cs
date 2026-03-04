public enum TargetingStrategy
{
  Closest,        // Target nearest enemy
  Strongest,      // Target enemy with most HP
  Weakest,        // Target enemy with least HP
  First,          // Target enemy furthest along path
  Priority        // Target based on enemy priority value
}
