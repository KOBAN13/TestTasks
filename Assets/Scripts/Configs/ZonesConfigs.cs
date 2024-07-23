using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "Zone Configs", menuName = "Zone Configs / Zone")]
    public class ZonesConfigs : ScriptableObject
    {
        [field: SerializeField] public int countSpawnZone { get; private set; }
    }
}