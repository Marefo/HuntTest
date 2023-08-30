using _CodeBase.MergeMode;
using UnityEngine;
using Zenject;

namespace _CodeBase.Infrastructure.Installers
{
  public class MergeSceneInstaller : MonoInstaller
  {
    [SerializeField] private Transform _fieldSpawnPoint;
    [SerializeField] private Field _fieldPrefab;
    
    public override void InstallBindings()
    {
      BindField();
    }

    private void BindField()
    {
      Field field = Container.InstantiatePrefabForComponent<Field>(_fieldPrefab, _fieldSpawnPoint);
      Container.Bind<Field>().FromInstance(field).AsSingle();
      field.transform.localPosition = Vector3.zero;
    }
  }
}