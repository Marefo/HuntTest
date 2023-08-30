using System;
using _CodeBase.Merge;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace _CodeBase.UI
{
  public class EmptyHuntingGroupMessage : MonoBehaviour
  {
    [SerializeField] private Transform _visual;

    private Field _field;

    [Inject]
    private void Construct(Field field)
    {
      _field = field;
    }
    
    private void OnEnable()
    {
      _field.HuntingGroupBecomeEmpty += Show;
      _field.HuntingGroupFilled += Hide;
    }

    private void OnDisable()
    {
      _field.HuntingGroupBecomeEmpty -= Show;
      _field.HuntingGroupFilled -= Hide;
    }

    private void Show()
    {
      _visual.DOKill();
      _visual.localScale = Vector3.one;
      _visual.DOPunchScale(Vector3.one * 0.15f, 0.25f, 1).SetEase(Ease.InOutBack).SetLoops(-1, LoopType.Yoyo)
        .SetLink(_visual.gameObject);
    }

    private void Hide()
    {
      _visual.DOKill();
      _visual.localScale = Vector3.zero;
    }
  }
}