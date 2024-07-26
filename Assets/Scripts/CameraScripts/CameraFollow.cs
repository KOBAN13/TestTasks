using System;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;
using Random = System.Random;

namespace CameraScripts
{
    public class CameraFollow : MonoBehaviour, ITickable
    {
        [SerializeField] private Transform _target;
        [SerializeField] private Vector3 _offset;
        [SerializeField] private BoxCollider _mapCollider;

        private Bounds _bounds;

        private float _smoothTime = 0.3f;
        private Vector3 _velocity = Vector3.zero;

        private void Start()
        {
            _bounds = _mapCollider.bounds;
        }

        public void Tick()
        {
            Vector3 targetPosition = _target.position + _offset;

            float cameraHeight = Camera.main.orthographicSize * 2;
            float cameraWidth = cameraHeight * Camera.main.aspect;
            
            targetPosition.x = Mathf.Clamp(targetPosition.x, _bounds.min.x + cameraWidth / 2, _bounds.max.x - cameraWidth / 2);
            targetPosition.z = Mathf.Clamp(targetPosition.z, _bounds.min.z + cameraHeight / 2, _bounds.max.z - cameraHeight / 2);
            
            targetPosition.y = transform.position.y;
            
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, _smoothTime);
        }
    }
}