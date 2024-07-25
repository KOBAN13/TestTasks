using System.IO;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace SaveSystem
{
    public class JsonDataContext : DataContext
    {
        private const string Path = "GameData";

        public async UniTask Load()
        {
            if (!File.Exists(FilePath)) return;
            using var reader = new StreamReader(FilePath);
            var json = await reader.ReadToEndAsync();
            GameDataCurrent = JsonUtility.FromJson<GameData>(json);
        }

        public async UniTask Save()
        {
            var json = JsonUtility.ToJson(GameDataCurrent);
            await using var writer = new StreamWriter(FilePath);
            await writer.WriteAsync(json);
        }
        
        private string FilePath => $"{Application.persistentDataPath}/{Path}.json";
    }
}