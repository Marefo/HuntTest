using System;
using _CodeBase.Merge;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _CodeBase.UI
{
  [RequireComponent(typeof(Button))]
  public class BuyAnimalBtn : MonoBehaviour
  {
    private Button _button;
    private Field _field;

    [Inject]
    private void Construct(Field field)
    {
      _field = field;
    }
    
    private void Awake() => _button = GetComponent<Button>();

    private void OnEnable() => _button.onClick.AddListener(TryBuy);
    private void OnDisable() => _button.onClick.RemoveListener(TryBuy);

    private void TryBuy()
    {
      if(_field.IsAddAnimalPossible == false) return;
      _field.AddAnimal();
    }
  }
}