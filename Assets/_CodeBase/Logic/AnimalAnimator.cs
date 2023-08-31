using System.Collections;
using UnityEngine;

namespace _CodeBase.Logic
{
  public class AnimalAnimator : MonoBehaviour
  {
    [SerializeField] private Animator _animator;
    
    private readonly int _idleRandomHash = Animator.StringToHash("Idle Random");
    private readonly int _deathHash = Animator.StringToHash("Death");

    private Coroutine _idleAnimationRandomizeCoroutine;

    public void PlayDeath() => _animator.SetTrigger(_deathHash);

    public void StartIdleAnimationRandomization()
    {
      _idleAnimationRandomizeCoroutine = StartCoroutine(RandomizeAnimationCoroutine());
    }

    public void StopIdleAnimationRandomization()
    {
      if(_idleAnimationRandomizeCoroutine == null) return;
      StopCoroutine(_idleAnimationRandomizeCoroutine);
    }

    private IEnumerator RandomizeAnimationCoroutine()
    {
      while (true)
      {
        _animator.SetInteger(_idleRandomHash, Random.Range(0, 3));
        yield return new WaitForSeconds(Random.value);
      }
    }
  }
}