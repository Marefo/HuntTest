using _CodeBase.Infrastructure;
using UnityEngine;
using Zenject;

namespace _CodeBase.HuntMode.PreyCode
{
  public class PreyFactory : IFactory<Prey>
  {
    private Prey _preyPrefab;
    private DiContainer _container;

    public PreyFactory(AssetProvider assetProvider, DiContainer container)
    {
      _container = container;
      _preyPrefab = assetProvider.Load<Prey>(assetProvider.PreyPrefab);
    }

    public Prey Create() => _container.InstantiatePrefabForComponent<Prey>(_preyPrefab);
  }
}