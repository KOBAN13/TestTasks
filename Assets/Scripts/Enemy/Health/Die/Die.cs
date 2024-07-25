using Character;
using Character.Score;
using UnityEngine;
using Zenject;

namespace Enemy.Die
{
    public class Die<T> where T : MonoBehaviour
    {
        private IScore _score;

        public Die(IScore score)
        {
            _score = score;
        }

        public void Died(T objectDie)
        {
            if (typeof(T) == typeof(HumanoidEnemy))
            {
                _score.OnOnEnemyDie(objectDie.gameObject.GetComponent<HumanoidEnemy>().PointsForDeath);
            }

            if (typeof(T) == typeof(PlayerComponents))
            {
                _score.OnOnPlayerDie();
            }
            
            objectDie.gameObject.SetActive(false);
        }
    }
}