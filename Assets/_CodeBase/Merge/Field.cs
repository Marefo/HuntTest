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

    [SerializeField] private AnimalSpawner _animalSpawner;
    [Space(10)]
    [SerializeField] private List<Cell> _cells;

    private List<Cell> _cellsWithoutAnimal => _cells.Where(cell => cell.HasAnimal == false).ToList();

    public void AddAnimal()
    {
      if(IsAddAnimalPossible == false) return;
      Cell cell = GetCellWithoutAnimal();
      _animalSpawner.SpawnAnimal(cell, 1);
    }

    public void MergeAnimals(Animal draggingAnimal, Cell targetCell)
    {
      int targetLvl = targetCell.Animal.Lvl + 1;
      Destroy(draggingAnimal.gameObject);
      Destroy(targetCell.Animal.gameObject);
      targetCell.ResetAnimal();
      _animalSpawner.SpawnAnimal(targetCell, targetLvl);
    }

    private Cell GetCellWithoutAnimal() => _cellsWithoutAnimal.First();
  }
}