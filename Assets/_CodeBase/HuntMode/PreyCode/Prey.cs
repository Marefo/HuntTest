using System;
using _CodeBase.HuntMode.PreyCode.Settings;
using _CodeBase.HuntMode.Settings;
using _CodeBase.IndicatorCode;
using _CodeBase.Infrastructure;
using _CodeBase.Logic;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;
using Zenject;

namespace _CodeBase.HuntMode.PreyCode
{
  public class Prey : MonoBehaviour
  {
    [SerializeField] private AnimalPathFollower _follower;
    [SerializeField] private Health _health; 
    [SerializeField] private AnimalAnimator _animalAnimator;
    [Space(10)]
    [SerializeField] private ParticleSystem _deathVfx;
    [Space(10)]
    [SerializeField] private HuntGlobalAnimalSettings _globalSettings;
    [SerializeField] private PreySettings _settings;

    private PathsManager _pathsManager;
    private HuntModeGameState _gameState;
    
    [Inject]
    private void Construct(PathsManager pathsManager, HuntModeGameState gameState)
    {
      _pathsManager = pathsManager;
      _gameState = gameState;
    }
    
    private void Awake() => SetUpPathFollower();

    private void OnEnable() => _health.ValueCameToZero += Die;

    private void OnDisable() => _health.ValueCameToZero -= Die;

    public int OnHuntAnimalBite(int damage)
    {
      _health.Decrease(damage);
      return _settings.MeatPerHit;
    }

    private void SetUpPathFollower()
    {
      Path path = _pathsManager.GetMiddlePath();
      _follower.SetPath(path.PathCreator);
      _follower.SetStartDistance(_settings.StartDistance);
      _follower.SetMoveSpeed(_globalSettings.MoveSpeed);
      path.Take();
    }

    [Button()]
    private void Die()
    {
      _animalAnimator.PlayDeath();
      DOVirtual.DelayedCall(0.2f, FinishDeath).SetLink(gameObject);
    }

    private void FinishDeath()
    {
      Instantiate(_deathVfx, transform.position, Quaternion.identity);
      _gameState.Finish();
      Destroy(gameObject);
    }
  }
}