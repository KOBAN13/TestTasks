using System;
using InputSystem;
using Interface;
using UniRx;
using Weapon;
using Zenject;

namespace Character
{
    public class Player : IInitializable, IDisposable, ISetWeapon
    {
        private readonly IMovable _movable;
        private readonly IInputSystem _input;
        private readonly IRotate _rotate;
        private IWeapon _weapon;
        private readonly CompositeDisposable _compositeDisposable = new();
        private bool _isFire;

        public Player(IMovable movable, IInputSystem input, IRotate rotate)
        {
            _movable = movable;
            _input = input;
            _rotate = rotate;
        }

        public void SetWeapon(IWeapon weapon)
        {
            _weapon = weapon;
        }

        public void Dispose()
        {
            _compositeDisposable.Clear();
            _compositeDisposable.Dispose();
        }

        public void Initialize()
        {
            _input.MoveInput.SkipLatestValueOnSubscribe()
                .Subscribe(vector =>
                {
                    _movable.Move(vector);
                })
                .AddTo(_compositeDisposable);

            _input.MouseClick.SkipLatestValueOnSubscribe()
                .Subscribe(vector =>
                {
                    _rotate.RotateCharacter(vector);
                    
                    if(_isFire == false) return;
                    _weapon.Fire();
                })
                .AddTo(_compositeDisposable);
            
        }
    }

    public interface ISetWeapon
    {
        public void SetWeapon(IWeapon weapon);
    }
}