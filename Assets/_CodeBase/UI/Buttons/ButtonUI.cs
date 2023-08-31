using UnityEngine;
using UnityEngine.UI;

namespace _CodeBase.UI.Buttons
{
  public abstract class ButtonUI : MonoBehaviour
  {
    protected Button _button;
    
    private void Awake() => _button = GetComponent<Button>();

    protected virtual void OnEnable() => _button.onClick.AddListener(OnClick);
    protected virtual void OnDisable() => _button.onClick.RemoveListener(OnClick);
    
    protected abstract void OnClick();

    protected void Enable() => _button.interactable = true;
    protected void Disable() => _button.interactable = false;
  }
}