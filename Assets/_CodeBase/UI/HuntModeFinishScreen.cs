using System;
using _CodeBase.Infrastructure;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Zenject;

namespace _CodeBase.UI
{
  public class HuntModeFinishScreen : MonoBehaviour
  {
    [SerializeField] private GameObject _visual;
    [SerializeField] private Transform _earnGroup;
    [SerializeField] private TextMeshProUGUI _earnedMeatField;
    
    private HuntModeGameState _gameState;

    [Inject]
    private void Construct(HuntModeGameState gameState)
    {
      _gameState = gameState;
    }

    private void OnEnable() => _gameState.LevelFinished += Show;
    private void OnDisable() => _gameState.LevelFinished -= Show;

    private void Show(int earnedMeatAmountPerLevel)
    {
      _visual.SetActive(true);
      _earnedMeatField.text = $"Earned: {earnedMeatAmountPerLevel}";
      _earnGroup.transform.DOPunchScale(Vector3.one * 0.15f, 0.1f, 1).SetLink(_earnGroup.gameObject);
    }
  }
}