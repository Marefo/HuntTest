using System;
using _CodeBase.HuntMode.Launcher.Settings;
using _CodeBase.Infrastructure.Services;
using UnityEngine;
using Zenject;

namespace _CodeBase.HuntMode.Launcher
{
  public class AnimalsLauncher : MonoBehaviour
  {
    [SerializeField] private Transform _aimTopPoint;
    [SerializeField] private Transform _aimTargetPoint;
    [Space(10)]
    [SerializeField] private LineRenderer _aimLine;
    [Space(10)]
    [SerializeField] private AnimalsLauncherSettings _settings;

    private float _gravity => Physics.gravity.y;

    private InputService _inputService;
    private Vector3 _inputForce;
    private bool _isTouching;
    private Vector3 _touchStartPosition;
    private Vector3 _shootDirection;
    private HuntAnimal _launchingAnimal;

    [Inject]
    private void Construct(InputService inputService)
    {
      _inputService = inputService;
    }
    
    private void Awake()
    {
      ResetAimLine();
      ChangeAimTargetVisibility(false);
    }

    private void OnEnable()
    {
      _inputService.TouchEntered += StartAiming;
      _inputService.TouchCanceled += FinishAiming;
      _inputService.Disabled += ResetAiming;
    }

    private void OnDisable()
    {
      _inputService.TouchEntered -= StartAiming;
      _inputService.TouchCanceled -= FinishAiming;
      _inputService.Disabled -= ResetAiming;
    }

    private void FixedUpdate() => TakeAim();

    public void SetLaunchingAnimal(HuntAnimal launchingAnimal)
    {
      ResetAiming();
      _launchingAnimal = launchingAnimal;
    }

    private void TakeAim()
    {
      if(_isTouching == false) return;
      HandleInput();
      UpdateAimPrediction();
      DrawPath();
    }

    private void ResetAiming()
    {
      _isTouching = false;
      ChangeAimTargetVisibility(false);
      ResetAimLine();
    }

    private void StartAiming()
    {
      _isTouching = true;
      ChangeAimTargetVisibility(true);
      _touchStartPosition = _inputService.TouchPosition;
    }

    private void FinishAiming()
    {
      if(_isTouching == false) return;
      
      ResetAiming();
      Vector3 initialVelocity = CalculateLaunchData().InitialVelocity;
      _launchingAnimal.Launch(initialVelocity);
    }

    private void HandleInput()
    {
      _inputForce.x = Mathf.InverseLerp(-_settings.MaxInputDistance.x / 2, _settings.MaxInputDistance.x / 2, 
        _inputService.TouchPosition.x - _touchStartPosition.x);
      _inputForce.z = Mathf.InverseLerp(0, _settings.MaxInputDistance.z,
        _touchStartPosition.y - _inputService.TouchPosition.y);
    }

    private void UpdateAimPrediction()
    {
      Vector3 aimTargetPointOffsetX = _launchingAnimal.transform.right *
                                      Mathf.Lerp(-_settings.JumpDistanceX, _settings.JumpDistanceX, _inputForce.x); 
      
      Vector3 aimTargetPointOffsetZ = _launchingAnimal.transform.forward * Mathf.Clamp(
        _settings.JumpDistanceZ * _inputForce.z, 0, _settings.JumpDistanceZ);

      _aimTargetPoint.position = _launchingAnimal.transform.position + aimTargetPointOffsetZ + aimTargetPointOffsetX;
      _aimTopPoint.position = new Vector3(_aimTargetPoint.position.x / 2, _settings.JumpHeight, 
        _aimTargetPoint.position.z / 2);
    }

    private void DrawPath() 
    {
      LaunchData launchData = CalculateLaunchData();
      _aimLine.positionCount = _settings.AimLineResolution;
      
      for (int i = 0; i <= _settings.AimLineResolution - 1; i++) 
      {
        float simulationTime = i / (float) _settings.AimLineResolution * launchData.TimeToTarget;
        Vector3 displacement = launchData.InitialVelocity * simulationTime + 
                               Vector3.up * _gravity * simulationTime * simulationTime / 2f;
        Vector3 drawPoint = _launchingAnimal.transform.position + displacement;
        _aimLine.SetPosition(i, drawPoint);
      }
    }

    private LaunchData CalculateLaunchData()
    {
      float height = _aimTopPoint.position.y;
      float displacementY = _aimTargetPoint.position.y - _launchingAnimal.transform.position.y;
      Vector3 displacementXZ = new Vector3 (_aimTargetPoint.position.x - _launchingAnimal.transform.position.x, 0, 
        _aimTargetPoint.position.z - _launchingAnimal.transform.position.z);
      float time = Mathf.Sqrt(-2 * height / _gravity) + Mathf.Sqrt(2 * (displacementY - height) / _gravity);
      Vector3 velocityY = Vector3.up * Mathf.Sqrt (-2 * _gravity * height);
      Vector3 velocityXZ = displacementXZ / time;

      return new LaunchData(velocityXZ + velocityY * -Mathf.Sign(_gravity), time);
    }
    
    private struct LaunchData 
    {
      public readonly Vector3 InitialVelocity;
      public readonly float TimeToTarget;

      public LaunchData (Vector3 initialVelocity, float timeToTarget)
      {
        InitialVelocity = initialVelocity;
        TimeToTarget = timeToTarget;
      }
    }

    private void ResetAimLine() => _aimLine.positionCount = 0;
    private void ChangeAimTargetVisibility(bool visible) => _aimTargetPoint.gameObject.SetActive(visible);
  }
}