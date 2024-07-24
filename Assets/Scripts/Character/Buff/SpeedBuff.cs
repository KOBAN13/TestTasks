using UnityEngine;

namespace Character.Buff
{
    public class SpeedBuff : IBuff
    {
        private readonly float _speed;

        public SpeedBuff(float speed)
        {
            _speed = speed;
        }
        
        public CharacterStats AddBuff(CharacterStats baseBuff)
        {
            var newBonus = baseBuff;
            newBonus.Speed = Mathf.Max(_speed, 0f);
            Debug.Log($"Баф скорости на {newBonus.Speed}");
            return newBonus;
        }
    }
}