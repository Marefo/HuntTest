using System;
using System.Collections.Generic;
using _CodeBase.HuntMode.HuntCamerasCode;
using _CodeBase.HuntMode.Launcher;
using _CodeBase.HuntMode.PreyCode;
using _CodeBase.HuntMode.Settings;
using UnityEngine;
using Zenject;

namespace _CodeBase.HuntMode
{
  public class HuntAnimalsSwitcher : MonoBehaviour
  {
    [SerializeField] private HuntCamerasSwitcher _camerasSwitcher;
    [SerializeField] private AnimalsLauncher _animalsLauncher;
    [SerializeField] private List<HuntAnimal> _huntAnimals;
    [Space(10)]
    [SerializeField] private HuntGlobalAnimalSettings _settings;

    private PathsManager _pathsManager;
    
    [Inject]
    private void Construct(PathsManager pathsManager)
    {
      _pathsManager = pathsManager;
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
      _camerasSwitcher.SetTarget(animal.transform);
      _animalsLauncher.SetLaunchingAnimal(animal);
      animal.Landed += OnAnimalLand;
    }

    private void OnAnimalLand(HuntAnimal animal)
    {
      animal.Landed -= OnAnimalLand;
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