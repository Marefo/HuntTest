using UnityEngine;

namespace _CodeBase.HuntMode.Launcher.Settings
{
  [CreateAssetMenu(fileName = "AnimalsLauncherSettings", menuName = "Settings/AnimalsLauncher")]
  public class AnimalsLauncherSettings : ScriptableObject
  {
    public Vector3 MaxInputDistance;
    public float JumpDistanceX;
    public float JumpDistanceZ;
    public float JumpHeight;
    [Space(10)] 
    public int AimLineResolution;
  }
}