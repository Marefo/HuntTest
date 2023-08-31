using _CodeBase.HuntMode.PreyCode;
using UnityEngine;
using Zenject;

namespace _CodeBase.HuntMode.HuntCamerasCode
{
  public class HuntCamerasSwitcher : MonoBehaviour
  {
    [SerializeField] private HuntCamera _camera1;
    [SerializeField] private HuntCamera _camera2;
    
    private Prey _prey;

    [Inject]
    private void Construct(Prey prey)
    {
      _prey = prey;
    }
    
    public void SetTarget(Transform target)
    {
      if (_camera1.Active)
        ActivateNewCameraForTarget(_camera1, _camera2, target);
      else
        ActivateNewCameraForTarget(_camera2, _camera1, target);
    }

    public void ChangeActiveCameraLookAtTarget(Transform target)
    {
      if(_camera1.Active)
        _camera1.SetLookAtTarget(target);
      else
        _camera2.SetLookAtTarget(target);
    }

    private void ActivateNewCameraForTarget(HuntCamera oldCamera, HuntCamera newCamera, Transform target)
    {
      newCamera.SetFollowTarget(target);
      newCamera.SetLookAtTarget(_prey.transform);

      oldCamera.ResetActive();
      newCamera.SetActive();
    }
  }
}