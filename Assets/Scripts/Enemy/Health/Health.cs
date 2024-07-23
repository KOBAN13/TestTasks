using System;
using Enemy.Die;
using UnityEngine;

namespace Enemy
{
    public class Health<T> : IHealthStats where T : MonoBehaviour
    {
        public float MaxHealth { get; }
        public float CurrentHealth { get; private set; }
        private readonly Die<T> _objectHealth;
        private readonly T _objectDie;

        public Health(float health, Die<T> objectHealth, T objectDie)
        {
            MaxHealth = CurrentHealth = health;
            _objectHealth = objectHealth;
            _objectDie = objectDie;
        }

        public void SetDamage(float value)
        {
            if (value < 0) throw new ArgumentException($"The Argument {nameof(value)} cannot be <0");

            CurrentHealth = Mathf.Clamp(CurrentHealth - value, 0f, MaxHealth);
            
            if (CurrentHealth != 0f) return;
            
            _objectHealth.Died(_objectDie);
        }

        public void AddHealth(float value)
        {
            //empty
        }
    }
}