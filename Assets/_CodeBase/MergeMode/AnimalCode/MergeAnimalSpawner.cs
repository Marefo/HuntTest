using _CodeBase.MergeMode.AnimalCode.StaticData;
using DG.Tweening;
using UnityEngine;

namespace _CodeBase.MergeMode.AnimalCode
{
  public class MergeAnimalSpawner : MonoBehaviour
  {
    [SerializeField] private AnimalsData _animalsData;
    
    public MergeAnimal SpawnAnimal(Cell cell, int lvl)
    {
      AnimalData animalData = _animalsData.GetAnimalByLvl(lvl);
      MergeAnimal animal = Instantiate(animalData.MergePrefab);
      animal.Initialize(animalData.Lvl, animalData.Offset);
      cell.SetAnimal(animal);
      animal.transform.DOKill();
      animal.transform.DOPunchScale(Vector3.one * 0.25F, 0.15f).SetLink(animal.gameObject);
      return animal;
    }
  }
}