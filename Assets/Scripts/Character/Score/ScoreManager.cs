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
        private ScoreModel _scoreModel;
        private readonly IShowRecord _showRecord;

        public ScoreSaver(JsonDataContext jsonDataContext, ScoreModel scoreModel, IShowRecord showRecord)
        {
            _jsonDataContext = jsonDataContext;
            _scoreModel = scoreModel;
            _showRecord = showRecord;
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
            _scoreModel.Score.Value += score;
        }
        
        private async void PLayerDie()
        {
            await _jsonDataContext.Load();

            if (_scoreModel.Score.Value > _jsonDataContext.Score)
            {
                _jsonDataContext.Score = _scoreModel.Score.Value;
                _showRecord.OnOnNewRecordScore();
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