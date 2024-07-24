using Character;
using UnityEngine;
using Zenject;

namespace Spawners.BonusSpawn
{
    public abstract class BuffBonus : MonoBehaviour
    {
        protected PlayerBaff PlayerBaff;

        [Inject]
        private void Construct(PlayerBaff playerBaff) => PlayerBaff = playerBaff;
    }
}