using System;
using UnityEngine;
using Zenject;

namespace _CodeBase.Infrastructure.Services
{
  public class InputService : ITickable
  {
    public event Action TouchEntered;
    public event Action TouchCanceled;

    public float TouchDelta { get; private set; }

    private float _lastTouchPosition;
    private float _currentTouchPosition;

    public void Tick()
    {
      if (Input.GetMouseButtonDown(0))
      {
        TouchEntered?.Invoke();
      }
      else if (Input.GetMouseButton(0))
      {
      }
      else if (Input.GetMouseButtonUp(0))
        TouchCanceled?.Invoke();
    }
  }
}