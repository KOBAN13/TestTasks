using UnityEngine;

namespace Enemy.Die
{
    public class Die<T> where T : MonoBehaviour
    {
        public void Died(T objectDie)
        {
            objectDie.gameObject.SetActive(false);
            
            //ИВЕНТ ДЛЯ UIКИ
        }
    }
}