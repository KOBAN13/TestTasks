using System;
using UniRx;
using UnityEngine;

namespace Spawners.BonusSpawn
{
    public class Timer
    {
        private readonly float _time;
        private readonly Action _delegateWithTimerStop;

        public Timer(float time, Action delegateWithTimerStop)
        {
            _time = time;
            _delegateWithTimerStop = delegateWithTimerStop;
        }

        public void Initialize()
        {
            Observable
                .Timer(TimeSpan.FromSeconds(_time))
                .Subscribe(_ =>
                {
                    _delegateWithTimerStop();
                    Debug.Log($"конец бафа");
                });
        }
    }
}