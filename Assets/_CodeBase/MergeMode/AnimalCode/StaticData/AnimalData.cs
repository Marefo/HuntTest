using System;
using _CodeBase.HuntMode;
using UnityEngine;

namespace _CodeBase.MergeMode.AnimalCode.StaticData
{
  [Serializable]
  public class AnimalData
  {
    [field: SerializeField] public int Lvl { get; private set; }
    [field: SerializeField] public Vector3 Offset { get; private set; }
    [field: SerializeField] public int Damage { get; private set; }
    [field: SerializeField] public MergeAnimal MergePrefab { get; private set; }
    [field: SerializeField] public HuntAnimal HuntPrefab { get; private set; }
    
    public AnimalData(int lvl, Vector3 offset, MergeAnimal mergePrefab, HuntAnimal huntPrefab, int damage)
    {
      Lvl = lvl;
      Offset = offset;
      MergePrefab = mergePrefab;
      HuntPrefab = huntPrefab;
      Damage = damage;
    }
  }
}