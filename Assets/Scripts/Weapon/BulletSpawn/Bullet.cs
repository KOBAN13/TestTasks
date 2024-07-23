using System;
using Enemy;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Weapon
{
    public class Bullet : MonoBehaviour
    {
        private float _damage;

        public void InitBullet(float value)
        {
            if (_damage < 0) throw new ArgumentOutOfRangeException();

            _damage = value;
        }
        
        private void OnEnable()
        {
            var collider = GetComponent<Collider>();
            IDamage damage = null;

            collider
                .OnTriggerEnterAsObservable()
                .Where(x => x.TryGetComponent(out damage))
                .Subscribe(_ =>
                {
                    damage.Damagable.SetDamage(_damage);
                    collider.gameObject.SetActive(false);
                }).AddTo(this);
        }
    }
}