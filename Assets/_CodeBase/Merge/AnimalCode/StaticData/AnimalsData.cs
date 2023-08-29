using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _CodeBase.Merge.AnimalCode.StaticData
{
  [CreateAssetMenu(fileName = "AnimalsData", menuName = "StaticData/AnimalsData")]
  public class AnimalsData : ScriptableObject
  {
    public int MaxAnimalLvl => _animalsData.Max(animalData => animalData.Lvl);
      
    [SerializeField] private List<AnimalData> _animalsData;

    public AnimalData GetAnimalByLvl(int lvl) => _animalsData.First(animal => animal.Lvl == lvl);
  }
}