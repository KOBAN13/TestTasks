using Spawners.BonusSpawn;
using UnityEngine;

namespace Character.Buff
{
    public class TempBuff : IBuff
    {
        private IBuffable _buffable;
        private IBuff _buff;
        private float _time;

        public TempBuff(IBuff buff, IBuffable buffable, float timeToActive)
        {
            _buff = buff;
            _buffable = buffable;
            _time = timeToActive;
        }

        public CharacterStats AddBuff(CharacterStats baseBuff)
        {
            var newStats = _buff.AddBuff(baseBuff);
            var timer = new Timer(_time, () => _buffable.RemoveBuff(this));
            Debug.Log($"начало бафа {_buff}");
            timer.Initialize();
            return newStats;
        }
    }
}