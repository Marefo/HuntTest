using System;
using _CodeBase.MeatCode;
using UnityEngine;
using Zenject;

namespace _CodeBase.Infrastructure
{
  public class HuntModeGameState
  {
    public event Action<int> LevelFinished;

    private readonly MeatData _meatData;

    private int _meatAmountOnStart;

    public HuntModeGameState(MeatData meatData)
    {
      _meatData = meatData;
    }
    
    public void UpdateMeatAmountOnStart() => _meatAmountOnStart = _meatData.Amount;

    public void Finish()
    {
      int earnedMeatAmountPerLevel = _meatData.Amount - _meatAmountOnStart;
      LevelFinished?.Invoke(earnedMeatAmountPerLevel);
    }
  }
}