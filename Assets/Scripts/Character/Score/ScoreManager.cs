using System;
using SaveSystem;
using UnityEngine;
using Zenject;

namespace Character.Score
{
    public class ScoreSaver : IInitializable, IDisposable, IScore
    {
        private readonly JsonDataContext _jsonDataContext;
        private event Action<float> OnEnemyDie;
        private event Action OnPlayerDie;
        private GameData _gameData = new();

        public ScoreSaver(JsonDataContext jsonDataContext)
        {
            _jsonDataContext = jsonDataContext;
        }

        public void Dispose()
        {
            OnEnemyDie -= EnemyDie;
            OnPlayerDie -= PLayerDie;
        }

        public async void Initialize()
        {
            OnEnemyDie += EnemyDie;
            OnPlayerDie += PLayerDie;
            await _jsonDataContext.Load();
        }

        public void OnOnEnemyDie(float score)
        {
            OnEnemyDie?.Invoke(score);
        }

        public void OnOnPlayerDie()
        {
            OnPlayerDie?.Invoke();
        }
        
        private void EnemyDie(float score)
        {
            _gameData.score += score;
            Debug.LogWarning(_gameData.score);
        }
        
        private async void PLayerDie()
        {
            await _jsonDataContext.Load();

            if (_gameData.score > _jsonDataContext.Score)
            {
                _jsonDataContext.Score = _gameData.score;
                await _jsonDataContext.Save();
            }
        }
    }

    public interface IScore
    {
        void OnOnEnemyDie(float score);
        void OnOnPlayerDie();
    }
}