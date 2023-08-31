using System;
using System.Collections.Generic;
using _CodeBase.HuntMode.HuntCamerasCode;
using _CodeBase.HuntMode.Launcher;
using _CodeBase.HuntMode.PreyCode;
using _CodeBase.HuntMode.Settings;
using _CodeBase.Infrastructure;
using _CodeBase.MeatCode;
using _CodeBase.MeatCode.StaticData;
using UnityEngine;
using Zenject;

namespace _CodeBase.HuntMode
{
  public class HuntAnimalsSwitcher : MonoBehaviour
  {
    [SerializeField] private HuntCamerasSwitcher _camerasSwitcher;
    [SerializeField] private AnimalsLauncher _animalsLauncher;
    [Space(10)]
    [SerializeField] private HuntGlobalAnimalSettings _settings;

    private List<HuntAnimal> _huntAnimals;
    private PathsManager _pathsManager;
    private MeatData _meatData;
    private HuntModeGameState _gameState;
    
    [Inject]
    private void Construct(PathsManager pathsManager, MeatData meatData, HuntModeGameState gameState)
    {
      _pathsManager = pathsManager;
      _meatData = meatData;
      _gameState = gameState;
    }

    private void OnEnable() => _gameState.LevelFinished += OnLevelFinish;
    private void OnDisable() => _gameState.LevelFinished -= OnLevelFinish;

    private void Start()
    {
      InitializeAnimals();
      SelectAnimal(0);
    }

    public void SetAnimals(List<HuntAnimal> huntAnimals) => _huntAnimals = huntAnimals;

    private void InitializeAnimals()
    {
      foreach (HuntAnimal huntAnimal in _huntAnimals)
      {
        huntAnimal.SetOnPath(_pathsManager.GetRandomFreePath(), _settings.MoveSpeed);
      }
    }

    private void OnLevelFinish(int earnedMeatAmount)
    {
      foreach (HuntAnimal huntAnimal in _huntAnimals)
      {
        if(huntAnimal == null) continue;
        huntAnimal.OnLevelFinish();
      }
    }

    private void SelectAnimal(int animalIndex)
    {
      HuntAnimal animal = _huntAnimals[animalIndex];
      _camerasSwitcher.SetTarget(animal.transform);
      _animalsLauncher.SetLaunchingAnimal(animal);
      animal.Landed += OnAnimalLand;
    }

    private void OnAnimalLand(HuntAnimal animal, int receivedMeatAmount)
    {
      animal.Landed -= OnAnimalLand;
      
      if(receivedMeatAmount > 0)
        _meatData.Add(receivedMeatAmount);
      
      int animalIndex = _huntAnimals.IndexOf(animal);
      bool isLastAnimal = animalIndex == _huntAnimals.Count - 1;

      if (isLastAnimal == false)
        SelectAnimal(animalIndex + 1);
      else
        _gameState.Finish();
    }
  }
}