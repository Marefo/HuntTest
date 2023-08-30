using System.IO;
using UnityEngine;

namespace _CodeBase.Infrastructure.Services
{
  public class SavesService
  {
    public readonly string MergeFieldDataFileName = "MergeFieldData.json";
    
    public void Save<T>(T saveObj, string fileName)
    {
      string objJson = JsonUtility.ToJson(saveObj);
      File.WriteAllText(GetSaveFilePath(fileName), objJson);
    }

    public T Load<T>(string fileName) where T : class
    {
      string file = GetSaveFilePath(fileName);
      Debug.Log($"{file}");
      
      if (File.Exists(file) == false) return null;
      
      string objDataJson = File.ReadAllText(file);
      return JsonUtility.FromJson<T>(objDataJson);
    }

    private string GetSaveFilePath(string fileName) => $"{Application.persistentDataPath}/{fileName}";
  }
}