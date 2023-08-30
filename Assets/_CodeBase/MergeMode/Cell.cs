using System;
using _CodeBase.MergeMode.AnimalCode;
using UnityEngine;

namespace _CodeBase.MergeMode
{
  public class Cell : MonoBehaviour
  {
    public event Action AddedAnimal;
    public event Action RemovedAnimal;
    
    public bool HasAnimal => Animal != null;
    public MergeAnimal Animal { get; private set; }
    
    [field: SerializeField] public bool IsHuntingGroup { get; private set; }
    [field: SerializeField] public Transform AnimalPoint { get; private set; }
    [field: Space(10)] 
    [SerializeField] private MeshRenderer _mesh;
    [SerializeField] private Material _highlightMaterial;

    private Material _defaultMaterial;

    private void Awake() => _defaultMaterial = _mesh.material;

    public void SetAnimal(MergeAnimal animal)
    {
      Animal = animal;
      ResetAnimalPosition();
      AddedAnimal?.Invoke();
    }

    public void ResetAnimal()
    {
      Animal = null;
      RemovedAnimal?.Invoke();
    }

    public void EnableHighlight() => _mesh.material = _highlightMaterial;
    public void DisableHighlight() => _mesh.material = _defaultMaterial;

    private void ResetAnimalPosition()
    {
      Animal.transform.SetParent(AnimalPoint);
      Animal.transform.localPosition = Vector3.zero + Animal.Offset;
    }
  }
}