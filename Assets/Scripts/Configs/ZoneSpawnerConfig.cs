using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "Zone Configs", menuName = "Zone Configs / SpawerZone")]
    public class ZoneSpawnerConfig : ScriptableObject
    {
        [field: SerializeField] public Vector2 MapSize { get; private set; }
        [field: SerializeField] public Vector2 ZoneSize { get; private set; }
        [field: SerializeField] public float MinDistance { get; private set; }
    }
}