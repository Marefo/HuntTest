using System;
using _CodeBase.HuntMode.Settings;
using _CodeBase.IndicatorCode;
using UnityEngine;
using Zenject;

namespace _CodeBase.HuntMode.PreyCode
{
  public class Prey : MonoBehaviour
  {
    [SerializeField] private int _startDistance;
    [SerializeField] private AnimalPathFollower _follower;
    [SerializeField] private Health _health; 
    [Space(10)]
    [SerializeField] private HuntGlobalAnimalSettings _settings;

    private PathsManager _pathsManager;
    
    [Inject]
    private void Construct(PathsManager pathsManager)
    {
      _pathsManager = pathsManager;
    }
    
    private void Awake() => SetUpPathFollower();

    private void OnEnable() => _health.ValueCameToZero += Die;

    private void OnDisable() => _health.ValueCameToZero -= Die;

    public void ReceiveDamage(int damage) => _health.Decrease(damage);

    private void SetUpPathFollower()
    {
      Path path = _pathsManager.GetMiddlePath();
      _follower.SetPath(path.PathCreator);
      _follower.SetStartDistance(_startDistance);
      _follower.SetMoveSpeed(_settings.MoveSpeed);
      path.Take();
    }

    private void Die()
    {
      Destroy(gameObject);
    }
  }
}