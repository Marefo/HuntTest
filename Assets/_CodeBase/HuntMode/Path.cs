using PathCreation;
using UnityEngine;

namespace _CodeBase.HuntMode
{
  public class Path : MonoBehaviour
  {
    public bool IsFree { get; private set; } = true;
    
    [field: SerializeField] public PathCreator PathCreator;

    public void Take() => IsFree = false;
    public void Release() => IsFree = true;
  }
}