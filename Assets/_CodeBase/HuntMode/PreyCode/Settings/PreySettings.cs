using UnityEngine;

namespace _CodeBase.HuntMode.PreyCode.Settings
{
  [CreateAssetMenu(fileName = "PreySettings", menuName = "Settings/Prey")]
  public class PreySettings : ScriptableObject
  {
    public int StartDistance;
    public int MeatPerHit;
  }
}