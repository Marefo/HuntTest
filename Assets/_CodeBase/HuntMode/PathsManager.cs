using System;
using System.Collections.Generic;
using System.Linq;
using _CodeBase.Extensions;
using PathCreation;
using UnityEngine;

namespace _CodeBase.HuntMode
{
  public class PathsManager : MonoBehaviour
  {
    private readonly List<Path> _paths = new List<Path>();
    private List<Path> _freePaths => _paths.Where(path => path.IsFree).ToList();

    private void Awake() => CollectPaths();

    public Path GetRandomFreePath() => _freePaths.GetRandomValue();

    public Path GetMiddlePath()
    {
      int index = Mathf.RoundToInt(_paths.Count / 2f);
      return _paths[index];
    }

    private void CollectPaths()
    {
      Path[] paths = transform.GetComponentsInChildren<Path>();

      foreach (Path path in paths)
        _paths.Add(path);
    }
  }
}