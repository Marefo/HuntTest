using UnityEngine;
using UnityEngine.SceneManagement;

namespace _CodeBase.Infrastructure.Services
{
  public class SceneService
  {
    public readonly string BootstrapSceneName = "Bootstrap";
    public readonly string MergeSceneName = "Merge";
    public readonly string HuntSceneName = "Hunt";
    
    public void ReloadCurrentScene() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    public void LoadScene(string sceneName) => SceneManager.LoadScene(sceneName);
  }
}