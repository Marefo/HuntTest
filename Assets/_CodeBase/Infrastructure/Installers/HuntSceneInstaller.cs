using _CodeBase.HuntMode;
using _CodeBase.HuntMode.PreyCode;
using UnityEngine;
using Zenject;

namespace _CodeBase.Infrastructure.Installers
{
  public class HuntSceneInstaller : MonoInstaller
  {
    [SerializeField] private Transform _pathsManagerSpawnPoint;
    [SerializeField] private PathsManager _pathsManagerPrefab;
    
    public override void InstallBindings()
    {
      Container.Bind<Prey>().FromFactory<PreyFactory>();
      BindPathsManager();
    }
    
    private void BindPathsManager()
    {
      PathsManager pathsManager = Container.InstantiatePrefabForComponent<PathsManager>(_pathsManagerPrefab, _pathsManagerSpawnPoint);
      Container.Bind<PathsManager>().FromInstance(pathsManager).AsSingle();
      pathsManager.transform.localPosition = Vector3.zero;
    }
  }
}