using System;
using Character;
using UniRx;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Enemy.Walk
{
    public class EnemyWalk : IEnemyMove, IDisposable
    {
        private readonly ITarget _target;
        private readonly float _speed;
        private NavMeshAgent _navMeshAgent;
        private CompositeDisposable _compositeDisposable = new();
        
        
        public EnemyWalk(ITarget target, float speed)
        {
            _target = target;
            _speed = speed;

            SubscribeUpdateTarget();
        }

        private void SubscribeUpdateTarget()
        {
            Observable
                .EveryUpdate()
                .Subscribe(_ => _navMeshAgent.SetDestination(_target.GetTarget()))
                .AddTo(_compositeDisposable);
        }

        public void InitMove(NavMeshAgent agent)
        {
            _navMeshAgent = agent;
            _navMeshAgent.speed = _speed;
        }

        public void Dispose()
        {
            _compositeDisposable.Clear();
            _compositeDisposable.Dispose();
        }
    }
}