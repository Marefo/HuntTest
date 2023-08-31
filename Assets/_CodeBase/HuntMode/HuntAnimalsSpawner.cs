using System;
using System.Collections.Generic;
using _CodeBase.Infrastructure.Services;
using _CodeBase.MergeMode.AnimalCode.StaticData;
using _CodeBase.MergeMode.StaticData;
using UnityEngine;
using Zenject;

namespace _CodeBase.HuntMode
{
  public class HuntAnimalsSpawner : MonoBehaviour
  {
    [SerializeField] private HuntAnimalsSwitcher _huntAnimalsSwitcher;
    [SerializeField] private AnimalsData _animalsData;
    
    private SavesService _savesService;
    private readonly List<HuntAnimal> _huntAnimals = new List<HuntAnimal>();

    [Inject]
    private void Construct(SavesService savesService)
    {
      _savesService = savesService;
    }
    
    private void Awake()
    {
      SpawnAnimalsFromSaves();
      _huntAnimalsSwitcher.SetAnimals(_huntAnimals);
    }

    private void SpawnAnimalsFromSaves()
    {
      FieldData fieldData = _savesService.Load<FieldData>(_savesService.MergeFieldDataFileName);

      foreach (CellData cellData in fieldData.Cells)
      {
        if(cellData.HasAnimal == false || cellData.IsHuntingGroup == false) continue;

        AnimalData animalData = _animalsData.GetAnimalByLvl(cellData.AnimalLvl);

        HuntAnimal huntAnimal = Instantiate(animalData.HuntPrefab);
        huntAnimal.SetDamage(animalData.Damage);
        _huntAnimals.Add(huntAnimal);
      }
    }
  }
}