using System;
using UnityEngine;
using Zenject;

namespace _CodeBase.Infrastructure.Services
{
  public class InputService : ITickable
  {
    public event Action TouchEntered;
    public event Action TouchCanceled;
    public event Action Disabled;

    public bool Enabled { get; private set; } = true;
    public Vector3 TouchPosition => Input.mousePosition;

    private float _lastTouchPosition;
    private float _currentTouchPosition;

    public void Tick()
    {
      if(Enabled == false) return;

      if (Input.GetMouseButtonDown(0))
        TouchEntered?.Invoke();
      else if (Input.GetMouseButton(0))
      {
      }
      else if (Input.GetMouseButtonUp(0))
        TouchCanceled?.Invoke();
    }
    
    public void Enable() => Enabled = true;

    public void Disable()
    {
      Enabled = false;
      Disabled?.Invoke();
    }
  }
}