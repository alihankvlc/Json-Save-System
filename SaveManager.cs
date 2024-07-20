using System.IO;
using UnityEngine;

namespace Assets.KVLC
{
    public interface ISaveable { }

    public class SaveManager
    {
        public void Save<T>(T data) where T : ISaveable
        {
            string json = JsonUtility.ToJson(data);
            string filePath = GetFilePath<T>();

            File.WriteAllText(filePath, json);
        }

        public T Load<T>() where T : class, ISaveable
        {
            string filePath = GetFilePath<T>();

            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                return JsonUtility.FromJson<T>(json);
            }

            Debug.LogWarning($"File not found -> {filePath}");
            return null;
        }

        public bool ContainsKey<T>() where T : ISaveable
        {
            string filePath = GetFilePath<T>();
            return File.Exists(filePath);
        }

        private string GetFilePath<T>() where T : ISaveable
        {
            return Path.Combine(Application.persistentDataPath, $"{typeof(T).Name}.json");
        }
    }
}
