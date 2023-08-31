using System;
using _CodeBase.IndicatorCode;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _CodeBase.UI
{
  public class HealthVisualizer : MonoBehaviour
  {
    [SerializeField] private Health _health;
    [SerializeField] private GameObject _visual;
    [SerializeField] private Image _fill;

    private void OnEnable() => _health.HealthAmountChanged += OnHealthAmountChange;
    private void OnDisable() => _health.HealthAmountChanged -= OnHealthAmountChange;

    private void OnHealthAmountChange(int currentValue)
    {
      if (currentValue == 0)
      {
        _visual.SetActive(false);
        return;
      }
      
      float newFillAmount = Mathf.InverseLerp(0, _health.MaxValue, currentValue);
      _fill.DOKill();
      _fill.DOFillAmount(newFillAmount, 0.2f).SetLink(gameObject);
    }
  }
}