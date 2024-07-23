using UnityEngine;

namespace Weapon.Configs
{
    [CreateAssetMenu(fileName = "BulletConfig",menuName = "ShootConfig/ShotgunConfig")]
    public class ShotgunBulletConfig : ScriptableObject
    {
        [field: SerializeField] public int PelletsCount { get; private set; }
        [field: SerializeField] public float SpreadAngle { get; private set; }
        [field: SerializeField] public float BulletSpeed { get; private set; }
        [field: SerializeField] public float BulletRange { get; private set; }
        [field: SerializeField] public GameObject BulletPrefab { get; private set; }
    }
}