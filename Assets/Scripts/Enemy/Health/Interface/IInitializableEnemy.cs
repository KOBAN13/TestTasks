﻿using Enemy.Walk;

namespace Enemy
{
    public interface IInitializableEnemy
    {
        void InitEnemy(IHealthStats healthStats, IDamagable damagable, IEnemyMove enemyMove);
    }
}