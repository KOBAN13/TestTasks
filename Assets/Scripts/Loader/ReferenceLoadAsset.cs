using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Loader
{
    public class ReferenceLoadAsset : MonoBehaviour
    {
        [field: SerializeField] public AssetReferenceT<ScriptableObject> PlayerSettings { get; private set; }
        [field: SerializeField] public AssetReferenceT<ScriptableObject> DieZone { get; private set; }
        [field: SerializeField] public AssetReferenceT<ScriptableObject> DecelerationZone { get; private set; }
        [field: SerializeField] public AssetReferenceT<ScriptableObject> ZoneSpawn { get; private set; }
        [field: SerializeField] public AssetReferenceT<ScriptableObject> BulletConfig { get; private set; }
        [field: SerializeField] public AssetReferenceT<ScriptableObject> BulletShotgunConfig { get; private set; }
        [field: SerializeField] public AssetReferenceT<GameObject> DieZonePrefab { get; private set; }
        [field: SerializeField] public AssetReferenceT<GameObject> DecelerationZonePrefab { get; private set; }
        [field: SerializeField] public AssetReferenceT<GameObject> PistolBonus { get; private set; }
        [field: SerializeField] public AssetReferenceT<GameObject> RifleBonus { get; private set; }
        [field: SerializeField] public AssetReferenceT<GameObject> ShotgunBonus { get; private set; }
        [field: SerializeField] public AssetLabelReference EnemyConfigs { get; private set; }
    }
}