using _CodeBase.Infrastructure.Services;
using Zenject;

namespace _CodeBase.UI.Buttons
{
  public class HuntModeNextLevelBtn : ButtonUI
  {
    private SceneService _sceneService;

    [Inject]
    private void Construct(SceneService sceneService)
    {
      _sceneService = sceneService;
    }
    
    protected override void OnClick() => _sceneService.LoadScene(_sceneService.MergeSceneName);
  }
}