using UnityEngine;
using UnityEngine.UI;

namespace _CodeBase.UI.Buttons
{
  public abstract class ButtonUI : MonoBehaviour
  {
    private Button _button;
    
    private void Awake() => _button = GetComponent<Button>();

    private void OnEnable() => _button.onClick.AddListener(OnClick);
    private void OnDisable() => _button.onClick.RemoveListener(OnClick);
    
    protected abstract void OnClick();

    protected void Enable() => _button.interactable = true;
    protected void Disable() => _button.interactable = false;
  }
}