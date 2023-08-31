using UnityEngine;

namespace _CodeBase.Logic
{
  public class RotatorToCamera : MonoBehaviour
  {
    private Camera _camera;

    private void Start() => _camera = Camera.main;

    private void Update() => RotateToCamera();

    private void RotateToCamera()
    {
      Quaternion targetRotation =
        Quaternion.LookRotation(new Vector3(_camera.transform.forward.x, 0, _camera.transform.forward.z));

      transform.rotation = targetRotation;
    }
  }
}