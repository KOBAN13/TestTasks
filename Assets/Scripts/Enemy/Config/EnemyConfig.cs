using UnityEngine;

namespace Enemy.Config
{
    [CreateAssetMenu(fileName = "EnemyConfig",menuName = "Enemy Config/EnemyType")]
    public class EnemyConfig : ScriptableObject
    {
        [field: SerializeField] public float Health { get; private set; }
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public float CoefficientSpawn { get; private set; }
        [field: SerializeField] public float ScoreCount { get; private set; }
        [field: SerializeField] public GameObject EnemyPrefab { get; private set; }
    }
}