using System;
using _CodeBase.HuntMode.PreyCode;
using _CodeBase.Infrastructure;
using DG.Tweening;
using UnityEngine;

namespace _CodeBase.HuntMode
{
  public class HuntAnimal : MonoBehaviour
  {
    public event Action<HuntAnimal, int> Landed;

    [SerializeField] private ParticleSystem _hitPreyVfx;
    [SerializeField] private ParticleSystem _hitGroundVfx;
    [Space(10)]
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private SphereCollider _sphereCollider;
    [SerializeField] private Animator _animator;
    [SerializeField] private AnimalPathFollower _pathFollower;

    private const float CollisionSaveTime = 0.05f;
    
    private Vector3 _launchDirection;
    private bool _isLaunched;
    private bool _isCollisionSaveTimePassed;
    private Tween _collisionSaveTimeTween;
    private int _damage;

    private void OnCollisionEnter(Collision other)
    {
      if(_isCollisionSaveTimePassed == false) return;
      
      Vector3 contactPoint = other.GetContact(0).point;

      if (other.gameObject.TryGetComponent(out Prey prey))
        OnCollisionWithPrey(contactPoint, prey);
      else
        OnCollisionWithGround(contactPoint);
    }

    public void SetOnPath(Path path, float moveSpeed)
    {
      _pathFollower.SetMoveSpeed(moveSpeed);
      _pathFollower.SetPath(path.PathCreator);
      path.Take();
    }

    public void SetDamage(int damage) => _damage = damage;

    public void Launch(Vector3 initialVelocity)
    {
      _launchDirection = initialVelocity.normalized;
      transform.rotation = Quaternion.LookRotation(_launchDirection);
      _pathFollower.enabled = false;
      _rigidbody.isKinematic = false;
      _rigidbody.velocity = initialVelocity;

      _collisionSaveTimeTween?.Kill();
      _isCollisionSaveTimePassed = false;
      _collisionSaveTimeTween = DOVirtual.DelayedCall(CollisionSaveTime, OnCollisionSaveTimePassed).SetLink(gameObject);
    }

    public void OnLevelFinish()
    {
      _animator.enabled = false;
      _pathFollower.enabled = false;
    }

    private void OnCollisionSaveTimePassed()
    {
      _isCollisionSaveTimePassed = true;
      bool isHit = Physics.SphereCast(transform.position + _sphereCollider.center, _sphereCollider.radius, 
        transform.forward, out RaycastHit hit);
      
      if(isHit == false) return;
      OnCollisionWithGround(hit.point);
    }

    private void OnCollisionWithPrey(Vector3 contactPoint, Prey prey)
    {
      Instantiate(_hitPreyVfx, contactPoint, Quaternion.identity);
      int receivedMeatAmount = prey.OnHuntAnimalBite(_damage);
      
      Landed?.Invoke(this, receivedMeatAmount);
      Destroy(gameObject);
    }

    private void OnCollisionWithGround(Vector3 contactPoint)
    {
      Landed?.Invoke(this, 0);
      Instantiate(_hitGroundVfx, contactPoint, Quaternion.identity);
      Destroy(gameObject);
    }
  }
}