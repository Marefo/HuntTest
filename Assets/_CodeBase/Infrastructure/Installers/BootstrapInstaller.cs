using _CodeBase.Infrastructure.Services;
using _CodeBase.MeatCode;
using _CodeBase.MeatCode.StaticData;
using UnityEngine;
using Zenject;

namespace _CodeBase.Infrastructure.Installers
{
  public class BootstrapInstaller : MonoInstaller
  {
    public override void InstallBindings()
    {
      Container.Bind<SceneService>().AsSingle().NonLazy();
      Container.Bind<SavesService>().AsSingle();
      Container.Bind<AssetProvider>().AsSingle();
      Container.BindInterfacesAndSelfTo<MeatData>().AsSingle().NonLazy();
      Container.BindInterfacesAndSelfTo<InputService>().AsSingle().NonLazy();
    }
  }
}