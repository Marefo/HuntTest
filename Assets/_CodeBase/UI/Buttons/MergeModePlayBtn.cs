using System;
using _CodeBase.Infrastructure.Services;
using _CodeBase.MergeMode;
using Zenject;

namespace _CodeBase.UI.Buttons
{
  public class MergeModePlayBtn : ButtonUI
  {
    private SceneService _sceneService;
    private Field _field;
    
    [Inject]
    private void Construct(SceneService sceneService, Field field)
    {
      _sceneService = sceneService;
      _field = field;
      SubscribeEvents();
    }

    private void OnDestroy() => UnSubscribeEvents();

    private void SubscribeEvents()
    {
      _field.HuntingGroupBecomeEmpty += Disable;
      _field.HuntingGroupFilled += Enable;
    }

    private void UnSubscribeEvents()
    {
      _field.HuntingGroupBecomeEmpty -= Disable;
      _field.HuntingGroupFilled -= Enable;
    }
    
    protected override void OnClick()
    {
      _field.Save();
      _sceneService.LoadScene(_sceneService.HuntSceneName);
    }
  }
}