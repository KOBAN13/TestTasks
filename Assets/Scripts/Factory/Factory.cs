using Character;
using Character.Score;
using Enemy;
using Enemy.Config;
using Enemy.Die;
using Enemy.EnemyKill;
using Enemy.Walk;
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

        public HumanoidEnemy CreateInitDiContainerAndInitializeEnemy(GameObject prefab, EnemyConfig enemyConfig, Vector3 position = default, Quaternion rotation = default)
        {
            var enemyInstance = _diContainer.InstantiatePrefabForComponent<HumanoidEnemy>(prefab);
            enemyInstance.transform.position = position;
            enemyInstance.transform.rotation = rotation;
            var enemyHealth = new Health<HumanoidEnemy>(enemyConfig.Health, CreateDieComponent<HumanoidEnemy>(), enemyInstance);
            
            enemyInstance.InitEnemy(enemyHealth, 
                new Damage(enemyInstance), 
                new EnemyWalk(GetInjection<ITarget>(), enemyConfig.Speed), 
                new HumanoidEnemyKill(CreateDieComponent<PlayerComponents>() ,GetInjection<PlayerBaff>()),
                enemyConfig.ScoreCount);

            return enemyInstance;
        }

        public Die<T> CreateDieComponent<T>() where T : MonoBehaviour
        {
            return new Die<T>(GetInjection<IScore>(), GetInjection<IMenu>());
        }

        public T GetInjection<T>()
        {
            return _diContainer.Resolve<T>();
        }

        public GameObject CreateGameObject(GameObject prefab)
        {
            return _diContainer.InstantiatePrefab(prefab);
        }
    }
}