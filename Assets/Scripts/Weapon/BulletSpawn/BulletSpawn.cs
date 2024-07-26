using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;
using Loader;
using UnityEngine;
using Weapon.Configs;

namespace Weapon
{
    public class BulletSpawn : IBulletSpawn, IDisposable
    {
        private Factory.Factory _factory;
        private BulletConfig _bulletConfig;
        private Loader.Loader _loader;
        private ReferenceLoadAsset _referenceLoadAsset;
        private bool _isLoadConfig;
        private Transform _bulletSpawnPoint;
        private CancellationTokenSource _cancellationToken = new();

        public BulletSpawn(Factory.Factory factory, Loader.Loader loader, ReferenceLoadAsset referenceLoadAsset)
        {
            _factory = factory;
            _loader = loader;
            _referenceLoadAsset = referenceLoadAsset;
            
            Initialize();
        }
        
        public async void BulletSpawnTask(float damage, Transform position)
        {
            _bulletSpawnPoint = position;
            _cancellationToken = new CancellationTokenSource();
            try
            {
                await UniTask.WaitUntil(() => _isLoadConfig);
                var bullet = _factory.CreateInitDiContainer<Bullet>(_bulletConfig.BulletPrefab, _bulletSpawnPoint.position, Quaternion.LookRotation(_bulletSpawnPoint.transform.forward));
                bullet.InitBullet(damage);
                await BulletLaunch(bullet);
            }
            catch (OperationCanceledException exception)
            {
                Debug.LogWarning($"{exception.Message}");
            }
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
            try
            {
                await ReturnBullet(bullet);
            }
            catch (OperationCanceledException exception)
            {
                Debug.LogWarning($"{exception.Message}");
            }
        }
        
        private async UniTask ReturnBullet(Bullet bullet)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(5f), cancellationToken: _cancellationToken.Token);
            bullet.gameObject.SetActive(false);
        }

        private async void Initialize()
        {
            _bulletConfig = await _loader.LoadResourcesUsingReference(_referenceLoadAsset.BulletConfig) as BulletConfig;
            _isLoadConfig = true;
        }

        public void Dispose()
        {
            _cancellationToken.Cancel();
        }
    }
}