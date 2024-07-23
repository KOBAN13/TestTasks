using UnityEngine;

namespace Weapon
{
    public interface IBulletSpawn
    {
        void BulletSpawnTask();
        public void Init(BulletPoint bulletSpawnPoint);
    }
}