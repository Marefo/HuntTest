using _CodeBase.Infrastructure.Services;
using Zenject;

namespace _CodeBase.Infrastructure.Installers
{
  public class BootstrapInstaller : MonoInstaller
  {
    public override void InstallBindings()
    {
      Container.Bind<SceneService>().AsSingle().NonLazy();
      Container.Bind<SavesService>().AsSingle().NonLazy();
      Container.BindInterfacesAndSelfTo<InputService>().AsSingle().NonLazy();
    }
  }
}