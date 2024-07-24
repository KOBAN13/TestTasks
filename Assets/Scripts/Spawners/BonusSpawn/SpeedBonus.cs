using Character.Buff;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Spawners.BonusSpawn
{
    public class SpeedBonus : BuffBonus
    {
        private void OnEnable()
        {
            var collider = GetComponent<Collider>();
            SpeedBuff speedBuff = new SpeedBuff(PlayerBaff.BaseStats.Speed * 1.5f);
            TempBuff buff = new TempBuff(speedBuff, PlayerBaff, 10f);
            
            collider.OnTriggerEnterAsObservable().Subscribe(_ =>
            {
                PlayerBaff.AddBuff(buff);
                collider.gameObject.SetActive(false);
            }).AddTo(this);
        }
    }
}