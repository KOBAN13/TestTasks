using UnityEngine;
using UnityEngine.AI;

namespace Enemy.Walk
{
    public interface IEnemyMove
    {
        void InitMove(NavMeshAgent agent);
        void Dispose();
    }
}