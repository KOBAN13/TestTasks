using UnityEngine;

namespace Weapon
{
    public interface IBulletSpawn
    {
        void BulletSpawnTask(float damage, Transform position);
        void Dispose();
    }
}