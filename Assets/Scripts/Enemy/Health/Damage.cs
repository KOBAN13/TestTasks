using System;

namespace Enemy
{
    public class Damage : IDamagable
    {
        private IHealth _health;

        public Damage(IHealth health)
        {
            _health = health;
        }
        
        public void SetDamage(float value)
        {
            if (value < 0) throw new ArgumentOutOfRangeException();
            
            _health.HealthStats.SetDamage(value);
        }
    }
}