using Interface;
using Loader;
using Physics;
using PlayerConfigs;
using UnityEngine;
using Zenject;

namespace Character
{
    public class Movement : IInitializable, ITickable, IMovable
    {
        private PlayerComponents _playerComponents;
        private PlayerSettings _playerSettings;
        private Loader.Loader _addressableLoader;
        private ReferenceLoadAsset _nameLoaderResources;
        private Vector3 _targetDirection;
        private Vector2 _input;

        public Movement(PlayerComponents playerComponents, Loader.Loader addressableLoader, ReferenceLoadAsset nameLoaderResources)
        {
            _playerComponents = playerComponents;
            _addressableLoader = addressableLoader;
            _nameLoaderResources = nameLoaderResources;
        }
        
        public void Move(Vector2 input)
        {
            _input = input;
        }

        public void Tick()
        {
            if(_playerSettings == null) return;
            _targetDirection.x = _playerSettings.Speed * _input.x;
            _targetDirection.z = _playerSettings.Speed * _input.y;
            _targetDirection.y = _playerComponents.TargetDirectionY;
            _playerComponents.CharacterController.Move(_targetDirection * Time.deltaTime);
        }

        public async void Initialize()
        {
            _playerSettings = await _addressableLoader.LoadResourcesUsingReference(_nameLoaderResources.PlayerSettings) as PlayerSettings;
        }
    }
}