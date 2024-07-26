using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Loader;
using UnityEngine;
using Weapon.Configs;
using Random = UnityEngine.Random;

namespace Weapon
{
    public class ShotgunBulletSpawn : IBulletSpawn, IDisposable
    {
        private Factory.Factory _factory;
        private ShotgunBulletConfig _bulletConfig;
        private Loader.Loader _loader;
        private ReferenceLoadAsset _referenceLoadAsset;
        private bool _isLoadConfig;
        private Transform _bulletSpawnPoint;
        private CancellationTokenSource _cancellationToken = new();
        private BulletRangeLimiter _bulletRangeLimiter;

        public ShotgunBulletSpawn(Factory.Factory factory, Loader.Loader loader, ReferenceLoadAsset referenceLoadAsset)
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
                await UniTask.WaitUntil(() => _isLoadConfig, cancellationToken: _cancellationToken.Token);
                BulletCalculate(damage);
            }
            catch (OperationCanceledException exception)
            {
                Debug.LogWarning($"{exception.Message}");
            }
        }


        private async void Initialize()
        {
            _bulletConfig = await _loader.LoadResourcesUsingReference(_referenceLoadAsset.BulletShotgunConfig) as ShotgunBulletConfig;
            _isLoadConfig = true;
        }

        private void BulletCalculate(float damage)
        {
            var spreadAngle = _bulletConfig.SpreadAngle * 0.5f;

            for (int i = 0; i < _bulletConfig.PelletsCount; i++)
            {
                var randomAngle = Random.Range(-spreadAngle, spreadAngle);
                var direction = Quaternion.Euler(0f, randomAngle, 0) * _bulletSpawnPoint.forward;

                var bullet = CreateBullet();
                bullet.InitBullet(damage);
                _bulletRangeLimiter = new BulletRangeLimiter();
                _bulletRangeLimiter.PointSpawnBullet(bullet.transform.position);
                _bulletRangeLimiter.CurrentBulletPosition(bullet);
                _bulletRangeLimiter.SetRange(_bulletConfig.BulletRange);
                _bulletRangeLimiter.Initialize();
                BulletLaunch(bullet, direction);
            }
        }

        private Bullet CreateBullet()
        {
            return _factory.CreateInitDiContainer<Bullet>(_bulletConfig.BulletPrefab, _bulletSpawnPoint.transform.position, Quaternion.LookRotation(_bulletSpawnPoint.transform.forward));
        }
        
        private void BulletLaunch(Bullet bullet, Vector3 direction)
        {
            var bulletRigidbody = bullet.GetComponent<Rigidbody>();
            Vector3 velocity = direction * _bulletConfig.BulletSpeed;
            bulletRigidbody.velocity = velocity;   
        }

        public void Dispose()
        {
            _bulletRangeLimiter?.Dispose();
            _cancellationToken?.Cancel();
        }
    }
}