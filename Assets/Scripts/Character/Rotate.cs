using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Interface;
using UnityEngine;

namespace Character
{
    public class Rotate : IRotate, IDisposable
    {
        private PlayerComponents _playerComponents;
        public CancellationTokenSource CancellationTokenSource { get; private set; }
        
        public Rotate(PlayerComponents playerComponents)
        {
            _playerComponents = playerComponents;
        }

        public async void RotateCharacter(Vector3 mousePosition)
        {
            CancellationTokenSource?.Cancel();
            CancellationTokenSource = new CancellationTokenSource();
            
            var ray = _playerComponents.Camera.ScreenPointToRay(mousePosition);

            if (UnityEngine.Physics.Raycast(ray, out var hit, 100f, LayerMask.GetMask("Ground")))
            {
                var direction = hit.point - _playerComponents.PlayerTransform.position;
                
                try
                {
                    await Lerp(-direction);
                }
                catch (OperationCanceledException cancel)
                {
                    Debug.LogWarning($"Operation is cancelled {cancel.Message}");
                }
            }
        }

        private async UniTask Lerp(Vector3 direction)
        {
            direction.y = 0f;

            var angle = Quaternion.Angle(_playerComponents.PlayerTransform.rotation,
                Quaternion.LookRotation(direction));

            var rotationSpeed = 180f;
            var rotationStep = rotationSpeed * Time.deltaTime;

            while (angle > rotationStep)
            {
                _playerComponents.PlayerTransform.rotation = Quaternion.Slerp(
                    _playerComponents.PlayerTransform.rotation, Quaternion.LookRotation(direction),
                    rotationStep / angle);

                angle -= rotationStep;

                await UniTask.Yield(CancellationTokenSource.Token);
            }

            await UniTask.CompletedTask;
        }

        public void Dispose()
        {
            CancellationTokenSource?.Cancel();
            CancellationTokenSource?.Dispose();
        }
    }
}