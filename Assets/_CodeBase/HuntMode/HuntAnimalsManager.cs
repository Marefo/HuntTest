using System;
using System.Collections.Generic;
using _CodeBase.HuntMode.Settings;
using UnityEngine;
using Zenject;

namespace _CodeBase.HuntMode
{
  public class HuntAnimalsManager : MonoBehaviour
  {
    [SerializeField] private HuntCamera _camera;
    [SerializeField] private List<HuntAnimal> _huntAnimals;
    [Space(10)]
    [SerializeField] private HuntGlobalAnimalSettings _settings;

    private PathsManager _pathsManager;
    private Prey _prey;
    
    [Inject]
    private void Construct(PathsManager pathsManager, Prey prey)
    {
      _pathsManager = pathsManager;
      _prey = prey;
    }

    private void Start()
    {
      InitializeAnimals();
      SelectAnimal(0);
    }

    private void InitializeAnimals()
    {
      foreach (HuntAnimal huntAnimal in _huntAnimals)
      {
        huntAnimal.Initialize(_pathsManager.GetRandomFreePath(), _settings.MoveSpeed);
      }
    }

    private void SelectAnimal(int animalIndex)
    {
      HuntAnimal animal = _huntAnimals[animalIndex];
      _camera.SetTarget(animal.transform, _prey.transform);
      animal.Jumped += OnAnimalJump;
    }

    private void OnAnimalJump(HuntAnimal animal)
    {
      animal.Jumped -= OnAnimalJump;
      int animalIndex = _huntAnimals.IndexOf(animal);
      bool isLastAnimal = animalIndex == _huntAnimals.Count - 1;

      if (isLastAnimal == false)
        SelectAnimal(animalIndex + 1);
      else
      {
        // Finish Level (player lose)
      }
    }
  }
}