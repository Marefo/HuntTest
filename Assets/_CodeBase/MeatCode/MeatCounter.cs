using System;
using _CodeBase.MeatCode.StaticData;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Zenject;

namespace _CodeBase.MeatCode
{
  public class MeatCounter : MonoBehaviour
  {
    [SerializeField] private TextMeshProUGUI _textField;
    
    private MeatData _meatData;

    [Inject]
    private void Construct(MeatData meatData)
    {
      _meatData = meatData;
    }
    
    private void OnEnable() => _meatData.AmountChanged += OnMeatAmountChange;
    private void OnDisable() => _meatData.AmountChanged -= OnMeatAmountChange;

    private void Start() => OnMeatAmountChange(_meatData.Amount);

    private void OnMeatAmountChange(int currentValue)
    {
      _textField.text = currentValue.ToString();
      _textField.transform.DOPunchScale(Vector3.one * 0.15f, 0.1f, 1).SetLink(_textField.gameObject);
    }
  }
}