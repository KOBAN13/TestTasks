using Character;
using UnityEngine;
using Zenject;
using Random = System.Random;

namespace Spawners.PointSpawnBonuse
{
    public class PointsCamera : IInitializable
    {
        private PlayerComponents _playerComponents;
        private float _halfHeight;
        private float _halfWight;
        private Transform _transformCamera;
        private float radius = 20f;

        public PointsCamera(PlayerComponents playerComponents)
        {
            _playerComponents = playerComponents;
        }


        public void Invisible(out Vector3 position)
        {
            var random = new Random();
            float x = _playerComponents.Camera.transform.position.x;
            float z = _playerComponents.Camera.transform.position.z;

            position = Vector3.zero;
            

            while (true)
            {
                if (x >= _transformCamera.position.x - 1f - _halfWight && x <= _transformCamera.position.x + 1f +
                                                                     _halfWight
                                                                     && z >= _transformCamera.position.z - 1f -
                                                                     _halfHeight
                                                                     && z <= _transformCamera.position.z + 1f +
                                                                     _halfHeight)
                {
                    x = random.Next((int)(_transformCamera.position.x - _halfWight - radius), (int)(_transformCamera.position.x + _halfWight + radius));
                    z = random.Next((int)(_transformCamera.position.z - _halfHeight - radius), (int)(_transformCamera.position.z + _halfHeight + radius));
                    position = new Vector3(x, 0f, z);
                }
                else
                {
                    return;
                }
            }
        }

        public Vector3 Visible()
        {
            var random = new Random();
            float x = random.Next((int)(_transformCamera.position.x - _halfWight), (int)(_transformCamera.position.x + _halfWight));
            float z = random.Next((int)(_transformCamera.position.z - _halfHeight), (int)(_transformCamera.position.z + _halfHeight));
            return new Vector3(x, 0f, z);
        }

        public void Initialize()
        {
            _halfHeight = _playerComponents.Camera.orthographicSize;
            _halfWight = _playerComponents.Camera.aspect * _halfHeight;
            _transformCamera = _playerComponents.Camera.transform;
        }
    }
}