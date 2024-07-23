﻿using System;
using Cysharp.Threading.Tasks;
using Loader;
using UnityEngine;
using Weapon.Configs;
using Zenject;

namespace Weapon
{
    public class BulletSpawn : IInitializable, IBulletSpawn
    {
        private Factory.Factory _factory;
        private BulletConfig _bulletConfig;
        private Loader.Loader _loader;
        private ReferenceLoadAsset _referenceLoadAsset;
        private bool _isLoadConfig;
        private Transform _bulletSpawnPoint;

        public BulletSpawn(Factory.Factory factory, Loader.Loader loader, ReferenceLoadAsset referenceLoadAsset)
        {
            _factory = factory;
            _loader = loader;
            _referenceLoadAsset = referenceLoadAsset;
        }
        
        public async void BulletSpawnTask()
        {
            await UniTask.WaitUntil(() => _isLoadConfig);
            var bullet = _factory.CreateInitDiContainer<Bullet>(_bulletConfig.BulletPrefab, _bulletSpawnPoint.transform.position, Quaternion.LookRotation(_bulletSpawnPoint.transform.forward));
            await BulletLaunch(bullet);
        }

        public void Init(BulletPoint bulletSpawnPoint)
        {
            _bulletSpawnPoint = bulletSpawnPoint.transform;
        }

        private async UniTask BulletLaunch(Bullet bullet)
        {
            var bulletRigidbody = bullet.GetComponent<Rigidbody>();
            Vector3 velocity = _bulletSpawnPoint.transform.forward * _bulletConfig.BulletSpeed;
            bulletRigidbody.velocity = velocity;   
            await ReturnBullet(bullet);
        }
        
        private async UniTask ReturnBullet(Bullet bullet)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(5f));
        }

        public async void Initialize()
        {
            _bulletConfig = await _loader.LoadResourcesUsingReference(_referenceLoadAsset.BulletConfig) as BulletConfig;
            _isLoadConfig = true;
        }
    }
}