using Cinemachine;
using UnityEngine;

namespace _CodeBase.HuntMode.HuntCamerasCode
{
  public class HuntCamera : MonoBehaviour
  {
    [field: SerializeField] public bool Active { get; private set; }
    [SerializeField] private CinemachineVirtualCamera _camera;

    public void SetFollowTarget(Transform target) => _camera.Follow = target;
    public void SetLookAtTarget(Transform lookAt) => _camera.LookAt = lookAt;

    public void SetActive()
    {
      Active = true;
      _camera.Priority = 100;
    }

    public void ResetActive()
    {
      Active = false;
      _camera.Priority = 0;
    }
  }
}