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
        public bool abc;
        private int radius = 20;

        public void Tick()
        {
            transform.position =
                Vector3.SmoothDamp(transform.position, _target.position + _offset, ref _velocity, _smoothTime);
        }


        /*public Camera mainCamera; // Ссылка на основную камеру
        public GameObject objectToSpawn; // Префаб объекта, который нужно спавнить// Радиус вокруг камеры, за пределами которого будет спавниться объект
        

        private void SpawnObject()
        {
            var i = 100;
            while (i != 0)
            {
                // Генерируем случайную позицию в пределах spawnRadius, игнорируя Y

                Invisible(out var position);
                Instantiate(objectToSpawn, position, Quaternion.identity);
                i--;
            }

            abc = false;
        }

        private void Invisible(out Vector3 position)
        {
            var a = new Random();
            var halfHeight = mainCamera.orthographicSize;
            var halfWight = mainCamera.aspect * halfHeight;
            float x = mainCamera.transform.position.x;
            float z = mainCamera.transform.position.z;

            position = Vector3.zero;
            

            while (true)
            {
                if (x >= mainCamera.transform.position.x - 1f - halfWight && x <= mainCamera.transform.position.x + 1f +
                                                                      halfWight
                                                                      && z >= mainCamera.transform.position.z - 1f -
                                                                      halfHeight
                                                                      && z <= mainCamera.transform.position.z + 1f +
                                                                      halfHeight)
                {
                    x = a.Next((int)(mainCamera.transform.position.x - halfWight - radius), (int)(mainCamera.transform.position.x + halfWight + radius));
                    z = a.Next((int)(mainCamera.transform.position.z - halfHeight - radius), (int)(mainCamera.transform.position.z + halfHeight + radius));
                    position = new Vector3(x, 0f, z);
                }
                else
                {
                    return;
                }
            }
        }

        private Vector3 Visible()
        {
            var a = new Random();
            var halfHeight = mainCamera.orthographicSize;
            var halfWight = mainCamera.aspect * halfHeight;
            float x = a.Next((int)(mainCamera.transform.position.x - halfWight), (int)(mainCamera.transform.position.x + halfWight));
            float z = a.Next((int)(mainCamera.transform.position.z - halfHeight), (int)(mainCamera.transform.position.z + halfHeight));
            return new Vector3(x, 0f, z);
        }*/
    }
    
}