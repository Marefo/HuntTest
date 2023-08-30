using System;

namespace _CodeBase.Merge.StaticData
{
  [Serializable]
  public class CellData
  {
    public bool IsHuntingGroup;
    public bool HasAnimal;
    public int AnimalLvl;

    public CellData(bool isHuntingGroup, bool hasAnimal, int animalLvl)
    {
      IsHuntingGroup = isHuntingGroup;
      HasAnimal = hasAnimal;
      AnimalLvl = animalLvl;
    }
  }
}