using System;
using UniRx;
using UnityEngine;
using Weapon.WeaponBonus;

namespace Character
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerComponents : MonoBehaviour, ICurrentWeaponBonus
    {
        private readonly ReactiveProperty<WeaponBonus> CurrentWeaponBonus = new();
        [field: SerializeField] public CharacterController CharacterController { get; private set; }
        [field: SerializeField] public Transform PlayerTransform { get; private set; }
        [field: SerializeField] public Camera Camera { get; private set; }
        
        public float TargetDirectionY { get; set; }
        
        public void SetCurrentWeaponBonus(WeaponBonus weaponBonus)
        {
            CurrentWeaponBonus.Value = weaponBonus;
        }

        public void Subscribe(Action weaponBonus)
        {
            CurrentWeaponBonus.SkipLatestValueOnSubscribe().Subscribe(_ => weaponBonus.Invoke()).AddTo(this);
        }

        public WeaponBonus GetCurrentWeaponBonus()
        {
            return CurrentWeaponBonus.Value;
        }
    }
    public interface ICurrentWeaponBonus
    {
        void SetCurrentWeaponBonus(WeaponBonus weaponBonus);
        WeaponBonus GetCurrentWeaponBonus();
        void Subscribe(Action weaponBonus);
    }
}