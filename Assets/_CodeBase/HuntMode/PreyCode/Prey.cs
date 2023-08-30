using System;
using _CodeBase.HuntMode.Settings;
using UnityEngine;
using Zenject;

namespace _CodeBase.HuntMode
{
  public class Prey : MonoBehaviour
  {
    [SerializeField] private int _startDistance;
    [SerializeField] private AnimalPathFollower _follower;
    [Space(10)]
    [SerializeField] private HuntGlobalAnimalSettings _settings;

    private PathsManager _pathsManager;
    
    [Inject]
    private void Construct(PathsManager pathsManager)
    {
      _pathsManager = pathsManager;
    }
    
    private void Awake() => SetUpPathFollower();

    private void SetUpPathFollower()
    {
      Path path = _pathsManager.GetMiddlePath();
      _follower.SetPath(path.PathCreator);
      _follower.SetStartDistance(_startDistance);
      _follower.SetMoveSpeed(_settings.MoveSpeed);
      path.Take();
    }
  }
}