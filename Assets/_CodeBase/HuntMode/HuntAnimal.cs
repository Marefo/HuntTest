using System;
using UnityEngine;

namespace _CodeBase.HuntMode
{
  public class HuntAnimal : MonoBehaviour
  {
    public event Action<HuntAnimal> Jumped;
    
    [SerializeField] private AnimalPathFollower _pathFollower;
    
    public void Initialize(Path path, float moveSpeed)
    {
      _pathFollower.SetMoveSpeed(moveSpeed);
      _pathFollower.SetPath(path.PathCreator);
      path.Take();
    }
  }
}