using System;
using Character.Buff;
using InputSystem;
using Interface;
using Loader;
using PlayerConfigs;
using UniRx;
using UnityEngine;
using Weapon;
using Zenject;

namespace Character
{
    public class Player : IInitializable, IDisposable, ISetWeapon, ITickable
    {
        private readonly IMovable _movable;
        private readonly IInputSystem _input;
        private readonly IRotate _rotate;
        private IWeapon _weapon;
        private readonly CompositeDisposable _compositeDisposable = new();
        private bool _isFire;
        private PlayerBaff _playerBaff;
        private PlayerSettings _playerSettings;

        public Player(IMovable movable, IInputSystem input, IRotate rotate, PlayerBaff playerBaff)
        {
            _movable = movable;
            _input = input;
            _rotate = rotate;
            _playerBaff = playerBaff;
        }

        public void SetWeapon(IWeapon weapon)
        {
            _weapon = weapon;
            _isFire = true;
        }

        public void Dispose()
        {
            _compositeDisposable.Clear();
            _compositeDisposable.Dispose();
        }

        public void Initialize()
        {
            _input.MouseClick.SkipLatestValueOnSubscribe()
                .Subscribe(async vector =>
                {
                    await _rotate.RotateCharacter(vector);
                    
                    if(_isFire == false) return;
                    _weapon.Fire();
                })
                .AddTo(_compositeDisposable);
            
        }

        public void Tick()
        {
            _movable.Move(_input.Input, _playerBaff.CurrentStats.Speed);
        }
    }

    public interface ISetWeapon
    {
        public void SetWeapon(IWeapon weapon);
    }
}