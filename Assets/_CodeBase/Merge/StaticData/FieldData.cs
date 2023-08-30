using System;
using System.Collections.Generic;
using UnityEngine;

namespace _CodeBase.Merge.StaticData
{
  [Serializable]
  public class FieldData
  {
    public List<CellData> Cells;
    
    public FieldData(List<CellData> cells)
    {
      Cells = cells;
    }
  }
}