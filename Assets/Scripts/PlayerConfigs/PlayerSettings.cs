using UnityEngine;

namespace PlayerConfigs
{
    [CreateAssetMenu(fileName = "Characters Configs", menuName = "CharactersConfigs / MovementParameters")]
    public class PlayerSettings : ScriptableObject
    {
        [field: SerializeField] public float Speed { get; private set; }
    }
}