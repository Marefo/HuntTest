using System;
using UnityEngine;

namespace _CodeBase.Merge.AnimalCode.StaticData
{
  [Serializable]
  public class AnimalData
  {
    [field: SerializeField] public int Lvl { get; private set; }
    [field: SerializeField] public Vector3 Offset { get; private set; }
    [field: SerializeField] public MergeAnimal Prefab { get; private set; }
    
    public AnimalData(Vector3 offset, MergeAnimal prefab, int lvl)
    {
      Offset = offset;
      Prefab = prefab;
      Lvl = lvl;
    }
  }
}