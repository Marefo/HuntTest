using UnityEngine;

namespace _CodeBase.Infrastructure
{
  public class AssetProvider
  {
    public readonly string PreyPrefab = "Prey";

    public T Load<T>(string path) where T : Object => Resources.Load<T>(path);
  }
}