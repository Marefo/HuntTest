using Cinemachine;
using UnityEngine;

namespace _CodeBase.HuntMode
{
  public class HuntCamera : MonoBehaviour
  {
    [SerializeField] private CinemachineVirtualCamera _camera;

    public void SetTarget(Transform target, Transform lookAt)
    {
      _camera.Follow = target;
      _camera.LookAt = lookAt;
    }
  }
}