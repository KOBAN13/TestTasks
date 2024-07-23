using Cysharp.Threading.Tasks;
using Loader;
using UnityEngine;
using Weapon.Configs;

namespace Weapon
{
    public class ShotgunBulletSpawn : IBulletSpawn
    {
        private Factory.Factory _factory;
        private ShotgunBulletConfig _bulletConfig;
        private Loader.Loader _loader;
        private ReferenceLoadAsset _referenceLoadAsset;
        private bool _isLoadConfig;
        private Transform _bulletSpawnPoint;

        public ShotgunBulletSpawn(Factory.Factory factory, Loader.Loader loader, ReferenceLoadAsset referenceLoadAsset)
        {
            _factory = factory;
            _loader = loader;
            _referenceLoadAsset = referenceLoadAsset;
            Initialize();
        }
        
        public async void BulletSpawnTask(float damage)
        {
            await UniTask.WaitUntil(() => _isLoadConfig);
            BulletCalculate(damage);
        }

        public void Init(BulletPoint bulletSpawnPoint)
        {
            _bulletSpawnPoint = bulletSpawnPoint.transform;
        }

        public async void Initialize()
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
                var bulletLimiter = new BulletRangeLimiter();
                bulletLimiter.PointSpawnBullet(bullet.transform.position);
                bulletLimiter.CurrentBulletPosition(bullet);
                bulletLimiter.SetRange(_bulletConfig.BulletRange);
                bulletLimiter.Initialize();
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
    }
}