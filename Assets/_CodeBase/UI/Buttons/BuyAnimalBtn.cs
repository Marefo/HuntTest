using _CodeBase.MergeMode;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _CodeBase.UI.Buttons
{
  [RequireComponent(typeof(Button))]
  public class BuyAnimalBtn : ButtonUI
  {
    private Field _field;

    [Inject]
    private void Construct(Field field)
    {
      _field = field;
    }

    protected override void OnClick()
    {
      if(_field.IsAddAnimalPossible == false) return;
      _field.AddAnimal();
    }
  }
}