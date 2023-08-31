using UnityEngine;

namespace _CodeBase.MergeMode.StaticData
{
  [CreateAssetMenu(fileName = "EconomicData", menuName = "StaticData/Economic")]
  public class EconomicData : ScriptableObject
  {
    public int AnimalCost;
  }
}