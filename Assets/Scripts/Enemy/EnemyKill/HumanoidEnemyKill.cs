using Character;
using Enemy.Die;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Enemy.EnemyKill
{
    public class HumanoidEnemyKill : IKill
    {
        private PlayerBaff _playerBaff;
        private Die<PlayerComponents> _player;
        
        private CompositeDisposable _compositeDisposable = new();

        public HumanoidEnemyKill(Die<PlayerComponents> player, PlayerBaff playerBaff)
        {
            _player = player;
            _playerBaff = playerBaff;
        }

        public void OnTriggerEnemy(Collider collider)
        {
            PlayerComponents playerComponents = null;
            
            collider
                .OnTriggerEnterAsObservable()
                .Where(_ => _playerBaff.CurrentStats.IsImmortal == false && _. TryGetComponent(out playerComponents))
                .Subscribe(_ => _player.Died(playerComponents))
                .AddTo(_compositeDisposable);
        }
    }

    public interface IKill
    {
        void OnTriggerEnemy(Collider collider);
    }
}