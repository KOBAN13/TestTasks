using UnityEngine;

namespace Weapon
{
    public interface IBulletSpawn
    {
        void BulletSpawnTask(float damage);
        public void Init(BulletPoint bulletSpawnPoint);
    }
}