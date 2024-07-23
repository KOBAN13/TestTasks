using System;
using UniRx;
using UnityEngine;
using Weapon.Configs;
using Zenject;

namespace Weapon
{
    public class Rifle : MonoBehaviour, IWeapon
    {
        [field: SerializeField] public WeaponConfig WeaponConfig { get; private set; }
        private IBulletSpawn _bulletSpawn;
        private IDisposable _fireSub;
        private Subject<Unit> _fireSubject = new();

        [Inject]
        public void Construct(IBulletSpawn bulletSpawn) => _bulletSpawn = bulletSpawn;

        private void Awake()
        {
            _bulletSpawn.Init(gameObject.GetComponentInChildren<BulletPoint>());
        }

        public GameObject GetWeaponGameObject()
        {
            return gameObject;
        }

        public void Fire()
        {
            _fireSubject.OnNext(Unit.Default);
        }

        private void OnEnable()
        {
            _fireSub = _fireSubject.Throttle(TimeSpan.FromSeconds(1 / WeaponConfig.SpeedFireInSecond))
                .Subscribe(_ => _bulletSpawn.BulletSpawnTask(WeaponConfig.Damage));
        }

        private void OnDisable()
        {
            _fireSub.Dispose();
        }
    }
}