using UnityEngine;

namespace Weapon.Configs
{
    [CreateAssetMenu(fileName = "Weapon",menuName = "Weapon/Config")]
    public class WeaponConfig : ScriptableObject
    {
        [field: SerializeField] public float Damage { get; private set; }
        [field: SerializeField] public float SpeedFireInSecond { get; private set; }
    }
}