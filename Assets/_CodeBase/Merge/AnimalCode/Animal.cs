using UnityEngine;

namespace _CodeBase.Merge.AnimalCode
{
  public class Animal : MonoBehaviour
  {
    public int Lvl { get; private set; }
    public Vector3 Offset { get; private set; }

    public void Initialize(int lvl, Vector3 offset)
    {
      Lvl = lvl;
      Offset = offset;
    }
  }
}