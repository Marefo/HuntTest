using _CodeBase.Merge.AnimalCode.StaticData;
using UnityEngine;

namespace _CodeBase.Merge.AnimalCode
{
  public class AnimalSpawner : MonoBehaviour
  {
    [SerializeField] private AnimalsData _animalsData;
    
    public Animal SpawnAnimal(Cell cell, int lvl)
    {
      AnimalData animalData = _animalsData.GetAnimalByLvl(lvl);
      Animal animal = Instantiate(animalData.Prefab);
      animal.Initialize(animalData.Lvl, animalData.Offset);
      cell.SetAnimal(animal);
      return animal;
    }
  }
}