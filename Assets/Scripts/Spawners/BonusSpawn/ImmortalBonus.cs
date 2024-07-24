using Character;
using Character.Buff;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;
using TempBuff = Character.Buff.TempBuff;

namespace Spawners.BonusSpawn
{
    public class ImmortalBonus : BuffBonus
    {
        private void OnEnable()
        {
            var collider = GetComponent<Collider>();
            ImmortalBuff immortalBuff = new ImmortalBuff();
            TempBuff buff = new TempBuff(immortalBuff, PlayerBaff, 10f);
            
            collider.OnTriggerEnterAsObservable().Subscribe(_ =>
            {
                PlayerBaff.AddBuff(buff);
                collider.gameObject.SetActive(false);
            }).AddTo(this);
        }
    }
}