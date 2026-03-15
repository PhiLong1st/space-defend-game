using UnityEngine;

public class CollectorTrigger : MonoBehaviour
{
  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.CompareTag("Player"))
    {
      return;
    }

    other.gameObject.SetActive(false);
  }
}