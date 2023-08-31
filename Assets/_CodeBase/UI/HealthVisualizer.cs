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
    [SerializeField] private Image _image;

    private void OnEnable() => _health.HealthAmountChanged += OnHealthAmountChange;
    private void OnDisable() => _health.HealthAmountChanged -= OnHealthAmountChange;

    private void OnHealthAmountChange(int currentValue)
    {
      float newFillAmount = Mathf.InverseLerp(0, _health.MaxValue, currentValue);
      _image.DOKill();
      _image.DOFillAmount(newFillAmount, 0.2f).SetLink(gameObject);
    }
  }
}