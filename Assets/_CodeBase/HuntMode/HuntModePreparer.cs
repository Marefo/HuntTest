using System;
using _CodeBase.Infrastructure;
using _CodeBase.MeatCode;
using UnityEngine;
using Zenject;

namespace _CodeBase.HuntMode
{
  public class HuntModePreparer : MonoBehaviour
  {
    private HuntModeGameState _gameState;

    [Inject]
    private void Construct(HuntModeGameState gameState, MeatData meatData)
    {
      _gameState = gameState;
    }

    private void Start() => _gameState.UpdateMeatAmountOnStart();
  }
}