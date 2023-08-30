using System;
using System.Collections.Generic;
using System.Linq;
using _CodeBase.Infrastructure.Services;
using _CodeBase.MergeMode.AnimalCode;
using _CodeBase.MergeMode.StaticData;
using NaughtyAttributes;
using UnityEngine;
using Zenject;

namespace _CodeBase.MergeMode
{
  public class Field : MonoBehaviour
  {
    public event Action HuntingGroupBecomeEmpty;
    public event Action HuntingGroupFilled;
    
    public bool IsAddAnimalPossible => _cellsWithoutAnimal.Count > 0;

    [SerializeField] private ParticleSystem _mergeVfx;
    [Space(10)]
    [SerializeField] private MergeAnimalSpawner _animalSpawner;
    [Space(10)]
    [SerializeField] private List<Cell> _cells;

    private List<Cell> _cellsWithoutAnimal => _cells.Where(cell => cell.HasAnimal == false).ToList();
    private List<Cell> _huntingGroupCells => _cells.Where(cell => cell.IsHuntingGroup).ToList();

    private SavesService _savesService;

    [Inject]
    private void Construct(SavesService savesService)
    {
      _savesService = savesService;
    }
    
    private void Awake() => _huntingGroupCells.ForEach(SubscribeToCellEvents);

    private void Start()
    {
      bool hasSaves = LoadSaves();
      
      if(hasSaves == false) 
        _animalSpawner.SpawnAnimal(GetCellWithoutAnimal(), 1);
    }

    private void OnApplicationFocus(bool hasFocus)
    {
      if(hasFocus) return;
      Save();
    }

    private void OnDestroy() => _huntingGroupCells.ForEach(UnSubscribeFromCellEvents);

    public void AddAnimal()
    {
      if(IsAddAnimalPossible == false) return;
      Cell cell = GetCellWithoutAnimal();
      _animalSpawner.SpawnAnimal(cell, 1);
    }

    public void MergeAnimals(MergeAnimal draggingAnimal, Cell targetCell)
    {
      int targetLvl = targetCell.Animal.Lvl + 1;
      Destroy(draggingAnimal.gameObject);
      Destroy(targetCell.Animal.gameObject);
      targetCell.ResetAnimal();
      Instantiate(_mergeVfx, targetCell.AnimalPoint.position + _mergeVfx.transform.position, Quaternion.identity);
      _animalSpawner.SpawnAnimal(targetCell, targetLvl);
    }

    public List<Cell> GetCellsByLvl(int lvl) => _cells.Where(cell => cell.HasAnimal && cell.Animal.Lvl == lvl).ToList();

    private Cell GetCellWithoutAnimal() => _cellsWithoutAnimal.First();

    private void SubscribeToCellEvents(Cell cell)
    {
      cell.AddedAnimal += CheckForHuntingGroupState;
      cell.RemovedAnimal += CheckForHuntingGroupState;
    }
    
    private void UnSubscribeFromCellEvents(Cell cell)
    {
      cell.AddedAnimal -= CheckForHuntingGroupState;
      cell.RemovedAnimal -= CheckForHuntingGroupState;
    }
    
    private void CheckForHuntingGroupState()
    {
      bool isAllHuntingGroupCellsEmpty = _huntingGroupCells.Count(cell => cell.IsHuntingGroup && cell.HasAnimal) == 0;

      if (isAllHuntingGroupCellsEmpty == false) 
        HuntingGroupFilled?.Invoke();
      else
        HuntingGroupBecomeEmpty?.Invoke();
    }

    [Button()]
    public void Save()
    {
      List<CellData> cellsData = new List<CellData>();

      foreach (Cell cell in _cells)
      {
        cellsData.Add(new CellData(cell.IsHuntingGroup, cell.HasAnimal, cell.HasAnimal ? cell.Animal.Lvl : 0));
      }

      FieldData fieldData = new FieldData(cellsData);
      _savesService.Save(fieldData, _savesService.MergeFieldDataFileName);
    }

    private bool LoadSaves()
    {
      FieldData fieldData = _savesService.Load<FieldData>(_savesService.MergeFieldDataFileName);
      
      if (fieldData == null) return false;

      for (int i = 0; i < _cells.Count; i++)
      {
        Cell currentCell = _cells[i];
        CellData savedCell = fieldData.Cells[i];
        
        if(savedCell.HasAnimal)
          _animalSpawner.SpawnAnimal(currentCell, savedCell.AnimalLvl);
      }

      return true;
    }
  }
}