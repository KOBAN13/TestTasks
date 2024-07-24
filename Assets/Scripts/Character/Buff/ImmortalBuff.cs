using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Character.Buff
{
    public class ImmortalBuff : IBuff
    {
        public CharacterStats AddBuff(CharacterStats baseBuff)
        {
            var newBonus = baseBuff;

            newBonus.IsImmortal = true;
            
            Debug.Log($"Я не уязвим {newBonus.IsImmortal}");
            
            return newBonus;
        }
    }
}