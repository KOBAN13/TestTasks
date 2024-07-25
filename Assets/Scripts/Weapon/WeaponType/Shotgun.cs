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
        private CompositeDisposable _compositeDisposable;

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
            if(_isFired == false) return;
            _bulletSpawn.BulletSpawnTask(WeaponConfig.Damage);
            _isFired = false;
        }

        private void OnEnable()
        {
            _compositeDisposable = new CompositeDisposable();
            Observable
                .Timer(TimeSpan.FromSeconds(1 / WeaponConfig.SpeedFireInSecond), TimeSpan.FromSeconds(1 / WeaponConfig.SpeedFireInSecond))
                .Subscribe(_ => _isFired = true)
                .AddTo(_compositeDisposable);
        }

        private void OnDisable()
        {
            _compositeDisposable.Clear();
            _compositeDisposable.Dispose();
        }
    }
}