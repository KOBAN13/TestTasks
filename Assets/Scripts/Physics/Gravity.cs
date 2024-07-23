using Character;
using UnityEngine;
using Zenject;

namespace Physics
{
    public class Gravity : IGravityForce, ITickable
    {
        private float _gravityForce = 9.8f;
        private PlayerComponents _playerComponents;

        public Gravity(PlayerComponents playerComponents)
        {
            _playerComponents = playerComponents;
        }

        public float GravityForce
        {
            get => _gravityForce;
            set
            {
                if (value >= 0)
                    _gravityForce = value;
            }
        }
        

        public void Tick()
        {
            GravityHandling();
        }
        
        private void GravityHandling()
        {
            _playerComponents.TargetDirectionY = _playerComponents.CharacterController.isGrounded switch
            {
                false => -_gravityForce * Time.deltaTime,
                true when _playerComponents.CharacterController.velocity.y <= 0 => -0.02f,
                _ => _playerComponents.TargetDirectionY
            };
        }
    }
}