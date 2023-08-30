using System;
using System.Collections.Generic;
using System.Linq;
using _CodeBase.Merge.AnimalCode;
using UnityEngine;

namespace _CodeBase.Merge
{
  public class Field : MonoBehaviour
  {
    public bool IsAddAnimalPossible => _cellsWithoutAnimal.Count > 0;

    [SerializeField] private ParticleSystem _mergeVfx;
    [Space(10)]
    [SerializeField] private MergeAnimalSpawner _animalSpawner;
    [Space(10)]
    [SerializeField] private List<Cell> _cells;

    private List<Cell> _cellsWithoutAnimal => _cells.Where(cell => cell.HasAnimal == false).ToList();

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
  }
}