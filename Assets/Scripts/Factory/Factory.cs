using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace Factory
{
    public class Factory
    {
        private DiContainer _diContainer;

        public Factory(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }
        
        public T CreateInitDiContainer<T>(GameObject prefab, Vector3 position = default, Quaternion rotation = default) where T : Object
        {
            var gameObject = _diContainer.InstantiatePrefabForComponent<T>(prefab);
            gameObject.GameObject().transform.position = position;
            gameObject.GameObject().transform.rotation = rotation;
            return gameObject;
        }

        public T GetInjection<T>() where T : new()
        {
            return _diContainer.Resolve<T>();
        }

        public GameObject CreateGameObject(GameObject prefab)
        {
            return _diContainer.InstantiatePrefab(prefab);
        }
    }
}