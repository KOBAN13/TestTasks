using UnityEngine;
using Zenject;
using Random = System.Random;

namespace CameraScripts
{
    public class CameraFollow : MonoBehaviour, ITickable
    {
        [SerializeField] private Transform _target;
        [SerializeField] private Vector3 _offset;
        private float _smoothTime = 0.3f;
        private Vector3 _velocity = Vector3.zero;

        public void Tick()
        {
            transform.position =
                Vector3.SmoothDamp(transform.position, _target.position + _offset, ref _velocity, _smoothTime);
        }
    }
    
}