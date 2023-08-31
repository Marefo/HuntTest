using System;
using _CodeBase.Infrastructure.Services;
using _CodeBase.MeatCode.StaticData;
using UnityEngine;
using Zenject;

namespace _CodeBase.MeatCode
{
  public class MeatData : IInitializable
  {
    public event Action<int> AmountChanged;
    
    public int Amount { get; private set; }

    private SavesService _savesService;
    
    public void Initialize() => Load();

    [Inject]
    public void Construct(SavesService savesService) => _savesService = savesService;

    public void Add(int toAdd)
    {
      Amount += Mathf.Abs(toAdd);
      UpdateValue();
    }

    public void Remove(int toRemove)
    {
      Amount -= Mathf.Abs(toRemove);
      UpdateValue();
    }
    
    private void UpdateValue()
    {
      AmountChanged?.Invoke(Amount);
      Save();
    }

    private void Save() => _savesService.Save(new MeatSaveData(Amount), _savesService.MeatDataFileName);

    private void Load()
    {
      MeatSaveData meatSaveData = _savesService.Load<MeatSaveData>(_savesService.MeatDataFileName);
      if(meatSaveData == null) return;
      Amount = meatSaveData.Amount;
      AmountChanged?.Invoke(Amount);
    }
  }
}