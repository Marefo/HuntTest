using System;
using System.Collections.Generic;

namespace _CodeBase.MergeMode.StaticData
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