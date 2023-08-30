using System;
using NaughtyAttributes;
using PathCreation;
using UnityEngine;

namespace _CodeBase.HuntMode
{
  public class AnimalPathFollower : MonoBehaviour
  {
    private readonly EndOfPathInstruction _modeAfterFinish = EndOfPathInstruction.Loop;
    private PathCreator _path;
    private float _moveSpeed;
    private float _travelledDistance;

    private void Update() => Follow();
    
    public void SetPath(PathCreator pathCreator) => _path = pathCreator;
    
    public void SetStartDistance(int startDistance) => _travelledDistance = startDistance;
    
    public void SetMoveSpeed(float moveSpeed) => _moveSpeed = moveSpeed;

    private void Follow()
    {
      _travelledDistance += _moveSpeed * Time.deltaTime;
      transform.position = _path.path.GetPointAtDistance(_travelledDistance, _modeAfterFinish);
      
      Vector3 rotationAlongPath = _path.path.GetRotationAtDistance(_travelledDistance, _modeAfterFinish).eulerAngles;
      Vector3 targetRotation = transform.rotation.eulerAngles;
      targetRotation.y = rotationAlongPath.y;
      transform.rotation = Quaternion.Euler(targetRotation);
    }
  }
}