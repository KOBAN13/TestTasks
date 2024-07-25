using System;
using Character;
using Enemy.Die;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;

namespace Spawners.Zones
{
    public class DieZone : MonoBehaviour
    {
        private PlayerComponents _playerComponents;
        private Factory.Factory _factory;

        [Inject]
        private void Construct(PlayerComponents playerComponents, Factory.Factory factory)
        { 
            _playerComponents = playerComponents;
            _factory = factory;
        }
        
        private void OnEnable()
        {
            var collider = GetComponent<Collider>();
            collider
                .OnTriggerEnterAsObservable()
                .Subscribe(_ => _factory.CreateDieComponent<PlayerComponents>().Died(_playerComponents))
                .AddTo(this);
        }
    }
}