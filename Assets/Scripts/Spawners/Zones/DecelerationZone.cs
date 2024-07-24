using Character;
using Character.Buff;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;

namespace Spawners.Zones
{
    public class DecelerationZone : MonoBehaviour
    {
        private PlayerBaff _playerBaff;

        [Inject]
        private void Construct(PlayerBaff playerBaff) => _playerBaff = playerBaff;
        
        private void OnEnable()
        {
            var collider = GetComponent<Collider>();
            var buff = new SpeedBuff(_playerBaff.BaseStats.Speed * 0.6f);
            
            collider
                .OnTriggerEnterAsObservable()
                .Subscribe(_ => _playerBaff.AddBuff(buff))
                .AddTo(this);

            collider.
                OnTriggerExitAsObservable()
                .Subscribe(_ => _playerBaff.RemoveBuff(buff))
                .AddTo(this);
        }
    }
}