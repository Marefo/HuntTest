using System.Collections.Generic;
using UnityEngine;

namespace _CodeBase.Extensions
{
  public static class Extensions
  {
    public static T GetRandomValue<T>(this List<T> list) => list[Random.Range(0, list.Count)];
  }
}