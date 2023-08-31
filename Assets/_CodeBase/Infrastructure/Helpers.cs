using UnityEngine;

namespace _CodeBase.Infrastructure
{
  public static class Helpers
  {
    public static Vector3 MultiplyVectors(Vector3 firstVector, Vector3 secondVector)
    {
      Vector3 multipliedVectors = Vector3.one;
      multipliedVectors.x = firstVector.x * secondVector.x;
      multipliedVectors.y = firstVector.y * secondVector.y;
      multipliedVectors.z = firstVector.z * secondVector.z;

      return multipliedVectors;
    }
    
    public static bool CompareLayers(int layer1, LayerMask layer2) => 
      layer2 == (layer2 | (1 << layer1));
  }
}