using System;
using UniRx;
using UnityEngine;
using Weapon.Configs;
using Zenject;

namespace Weapon
{
    public class Shotgun : MonoBehaviour, IWeapon
    {
        [field: SerializeField] public WeaponConfig WeaponConfig { get; private set; }
        private IBulletSpawn _bulletSpawn;
        private bool _isFired;
        private BulletPoint _bulletPoint;

        [Inject]
        public void Construct(IBulletSpawn bulletSpawn) => _bulletSpawn = bulletSpawn;

        private void Awake()
        {
            _bulletPoint = gameObject.GetComponentInChildren<BulletPoint>();
        }

        public GameObject GetWeaponGameObject()
        {
            return gameObject;
        }

        public void Fire()
        {
            if(_isFired == false) return;
            _bulletSpawn.BulletSpawnTask(WeaponConfig.Damage, _bulletPoint.transform);
            _isFired = false;
        }

        private void OnEnable()
        {
            Observable
                .Timer(TimeSpan.FromSeconds(1 / WeaponConfig.SpeedFireInSecond), TimeSpan.FromSeconds(1 / WeaponConfig.SpeedFireInSecond))
                .Subscribe(_ => _isFired = true)
                .AddTo(this);
        }

        private void OnDisable()
        {
            _bulletSpawn.Dispose();
            _isFired = false;
        }
    }
}