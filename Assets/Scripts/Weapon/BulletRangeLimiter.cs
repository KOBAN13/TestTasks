using System;
using UniRx;
using UnityEngine;

namespace Weapon
{
    public class BulletRangeLimiter : IDisposable
    {
        private Vector3 _startPointShoot;
        private Bullet _bullet;
        private float _range;
        private CompositeDisposable _compositeDisposable = new();

        public void PointSpawnBullet(Vector3 position)
        {
            _startPointShoot = position;
        }

        public void CurrentBulletPosition(Bullet bullet)
        {
            _bullet = bullet;
        }

        public void SetRange(float range)
        {
            if (range < 0) throw new ArgumentOutOfRangeException();
            _range = range;
        }
        
        public void Initialize()
        {
            _compositeDisposable = new CompositeDisposable();
            
            Observable
                .EveryUpdate()
                .Where(_ => Vector3.Distance(_startPointShoot, _bullet.transform.position) > _range)
                .Subscribe(_ =>
                {
                    _bullet.gameObject.SetActive(false);
                    Dispose();
                })
                .AddTo(_compositeDisposable);
        }

        public void Dispose()
        {
            _compositeDisposable?.Clear();
            _compositeDisposable?.Dispose();
        }
    }
}