using System;
using Enemy.Walk;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class HumanoidEnemy : MonoBehaviour, IHealth, IDamage, IInitializableEnemy
    {
        private IEnemyMove _enemyMove;
        public IHealthStats HealthStats { get; private set; }
        public IDamagable Damagable { get; private set; }

        public void InitEnemy(IHealthStats healthStats, IDamagable damagable, IEnemyMove enemyMove)
        {
            if (healthStats == null || damagable == null || enemyMove == null) throw new ArgumentNullException();

            HealthStats = healthStats;
            Damagable = damagable;
            _enemyMove = enemyMove;
            InitMove();
        }


        private void InitMove()
        {
            _enemyMove.InitMove(GetComponent<NavMeshAgent>());
        }
    }
}