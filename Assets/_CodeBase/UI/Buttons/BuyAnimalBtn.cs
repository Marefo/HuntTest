using System;
using _CodeBase.MeatCode;
using _CodeBase.MergeMode;
using _CodeBase.MergeMode.StaticData;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _CodeBase.UI.Buttons
{
  [RequireComponent(typeof(Button))]
  public class BuyAnimalBtn : ButtonUI
  {
    [SerializeField] private TextMeshProUGUI _costField;
    [SerializeField] private EconomicData _economicData;
    
    private Field _field;
    private MeatData _meatData;

    [Inject]
    private void Construct(Field field, MeatData meatData)
    {
      _field = field;
      _meatData = meatData;
    }

    protected override void OnEnable()
    {
      base.OnEnable();
      _meatData.AmountChanged += OnMeatAmountChange;
    }

    protected override void OnDisable()
    {
      base.OnDisable();
      _meatData.AmountChanged -= OnMeatAmountChange;
    }

    private void Start()
    {
      UpdateButtonAvailability();
      _costField.text = _economicData.AnimalCost.ToString();
    }

    protected override void OnClick()
    {
      _meatData.Remove(_economicData.AnimalCost);
      _field.AddAnimal();
    }

    private void OnMeatAmountChange(int obj) => UpdateButtonAvailability();

    private void UpdateButtonAvailability() => _button.interactable = _field.IsAddAnimalPossible && 
                                                                 _meatData.Amount >= _economicData.AnimalCost;
  }
}