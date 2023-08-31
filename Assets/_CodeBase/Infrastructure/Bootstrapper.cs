using System.Collections.Generic;
using _CodeBase.Infrastructure.Services;
using UnityEditor;
using UnityEngine;
using Zenject;

namespace _CodeBase.Infrastructure
{
  public class Bootstrapper : MonoBehaviour
  {
    [SerializeField] private List<GameObject> _indestructibleObjects;

    private const string WasBootstrappedKey = "WasBootstrapped";
    private SceneService _sceneService;

    [Inject]
    private void Construct(SceneService sceneService)
    {
      _sceneService = sceneService;
    }

    private void Awake() => Initialize();

#if UNITY_EDITOR
    private void OnEnable() => EditorApplication.playModeStateChanged += LogPlayModeState;
    private void OnDisable() => EditorApplication.playModeStateChanged -= LogPlayModeState;

    private void LogPlayModeState(PlayModeStateChange obj)
    {
      if(obj != PlayModeStateChange.ExitingPlayMode) return;
      MarkAsUnBootstrapped();
    }
#endif

    private void Initialize()
    {
      if (gameObject.scene.name == _sceneService.BootstrapSceneName)
      {
        Application.targetFrameRate = 60;
        MarkAsBootstrapped();
        _indestructibleObjects.ForEach(DontDestroyOnLoad);
        //_sceneService.LoadScene(_sceneService.MergeSceneName);
        _sceneService.LoadScene(_sceneService.HuntSceneName);
      }
      else if (IsBootstrapped() == false) 
        _sceneService.LoadScene(_sceneService.BootstrapSceneName);
    }

    private bool IsBootstrapped()
    {
      bool bootstrapped = false;
      
      if(PlayerPrefs.HasKey(WasBootstrappedKey))
        bootstrapped = PlayerPrefs.GetInt(WasBootstrappedKey) == 1;

      return bootstrapped;
    }

    private void MarkAsBootstrapped() => PlayerPrefs.SetInt(WasBootstrappedKey, 1);
    private void MarkAsUnBootstrapped() => PlayerPrefs.SetInt(WasBootstrappedKey, 0);
  }
}