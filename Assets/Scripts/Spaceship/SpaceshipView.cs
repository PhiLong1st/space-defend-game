using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpaceshipView : MonoBehaviour
{
  private Animator _animator;
  [SerializeField] private Spaceship _spaceship;

  [SerializeField] private GameObject[] _shipViewLevelPrefabs;
  
  private GameObject CurrentShipView;

  private void Awake()
  {
    _animator = GetComponent<Animator>();
  }

  private void Start()
  {
    UpdateShipView();
  }

  public void PlayLevelUpAnimation()
  {
    UpdateShipView();
  }

  private void UpdateShipView()
  {
    int level = Mathf.Min(_spaceship.CurrentLevel, _shipViewLevelPrefabs.Length);

    if (CurrentShipView != null)
    {
      Destroy(CurrentShipView);
    }

    GameObject shipView = Instantiate(_shipViewLevelPrefabs[level - 1], transform.position, transform.rotation, transform);
    shipView.SetActive(true);

    CurrentShipView = shipView;
  }
}