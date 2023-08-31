using System.Collections;
using _CodeBase.Logic;
using DG.Tweening;
using UnityEngine;

namespace _CodeBase.MergeMode.AnimalCode
{
  public class MergeAnimal : MonoBehaviour
  {
    public int Lvl { get; private set; }
    public Vector3 Offset { get; private set; }

    [SerializeField] private AnimalAnimator _animator;
    [SerializeField] private SkinnedMeshRenderer _mesh;
    [SerializeField] private Material _highlightMaterial;
    
    private Material _defaultMaterial;

    public void Initialize(int lvl, Vector3 offset)
    {
      Lvl = lvl;
      Offset = offset;
      _defaultMaterial = _mesh.material;

      _animator.StartIdleAnimationRandomization();
    }

    public void OnStartDragging()
    {
      EnableHighlight();
      transform.DOKill();
      transform.DOScale(Vector3.one * 1.25f, 0.15f).SetLink(gameObject);
    }

    public void OnFinishDragging()
    {
      DisableHighlight();
      transform.DOKill();
      transform.DOScale(Vector3.one, 0.15f).SetLink(gameObject);
    }

    private void EnableHighlight() => _mesh.material = _highlightMaterial;
    private void DisableHighlight() => _mesh.material = _defaultMaterial;
  }
}