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
        [Inject]
        private void Construct(PlayerComponents playerComponents) => _playerComponents = playerComponents;
        
        private void OnEnable()
        {
            var collider = GetComponent<Collider>();
            collider
                .OnTriggerEnterAsObservable()
                .Subscribe(_ => new Die<PlayerComponents>().Died(_playerComponents))
                .AddTo(this);
        }
    }
}