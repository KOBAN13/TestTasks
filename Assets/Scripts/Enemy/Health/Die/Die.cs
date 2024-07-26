using Character;
using Character.Score;
using UnityEngine;

namespace Enemy.Die
{
    public class Die<T> where T : MonoBehaviour
    {
        private IScore _score;
        private readonly IMenu _menu;

        public Die(IScore score, IMenu menu)
        {
            _score = score;
            _menu = menu;
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
                _menu.OnOnPlayerDied();
            }
            
            objectDie.gameObject.SetActive(false);
        }
    }
}