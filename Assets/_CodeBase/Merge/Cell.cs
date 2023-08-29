using _CodeBase.Merge.AnimalCode;
using UnityEngine;

namespace _CodeBase.Merge
{
  public class Cell : MonoBehaviour
  {
    public bool HasAnimal => Animal != null;
    public Animal Animal { get; private set; }
    
    [field: SerializeField] public Transform AnimalPoint { get; private set; }

    public void SetAnimal(Animal animal)
    {
      Animal = animal;
      ResetAnimalPosition();
    }

    public void ResetAnimal()
    {
      Animal = null;
    }

    private void ResetAnimalPosition()
    {
      Animal.transform.SetParent(AnimalPoint);
      Animal.transform.localPosition = Vector3.zero + Animal.Offset;
    }
  }
}