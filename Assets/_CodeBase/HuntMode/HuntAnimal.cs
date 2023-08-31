using System;
using _CodeBase.HuntMode.PreyCode;
using _CodeBase.Infrastructure;
using DG.Tweening;
using UnityEngine;

namespace _CodeBase.HuntMode
{
  public class HuntAnimal : MonoBehaviour
  {
    public event Action<HuntAnimal> Landed;

    [SerializeField] private ParticleSystem _hitPreyVfx;
    [SerializeField] private ParticleSystem _hitGroundVfx;
    [Space(10)]
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private AnimalPathFollower _pathFollower;

    private const float CollisionSaveTime = 0.15f;
    
    private Vector3 _launchDirection;
    private bool _isLaunched;
    private bool _isCollisionSaveTimePassed;
    private Tween _collisionSaveTimeTween;

    private void OnCollisionEnter(Collision other)
    {
      if(_isCollisionSaveTimePassed == false) return;
      
      Vector3 contactPoint = other.GetContact(0).point;
      Landed?.Invoke(this);

      if (other.gameObject.TryGetComponent(out Prey prey))
        OnCollisionWithPrey(contactPoint, prey);
      else
        OnCollisionWithGround(contactPoint);
    }

    public void Initialize(Path path, float moveSpeed)
    {
      _pathFollower.SetMoveSpeed(moveSpeed);
      _pathFollower.SetPath(path.PathCreator);
      path.Take();
    }

    public void Launch(Vector3 initialVelocity)
    {
      _launchDirection = initialVelocity.normalized;
      transform.rotation = Quaternion.LookRotation(_launchDirection);
      _pathFollower.enabled = false;
      _rigidbody.isKinematic = false;
      _rigidbody.velocity = initialVelocity;

      _collisionSaveTimeTween?.Kill();
      _isCollisionSaveTimePassed = false;
      _collisionSaveTimeTween = DOVirtual.DelayedCall(CollisionSaveTime, 
        () => _isCollisionSaveTimePassed = true).SetLink(gameObject);
    }

    private void OnCollisionWithPrey(Vector3 contactPoint, Prey prey)
    {
      Instantiate(_hitPreyVfx, contactPoint, Quaternion.identity);
      prey.ReceiveDamage(50);
      Destroy(gameObject);
    }

    private void OnCollisionWithGround(Vector3 contactPoint)
    {
      Instantiate(_hitGroundVfx, contactPoint, Quaternion.identity);
      Destroy(gameObject);
    }
  }
}