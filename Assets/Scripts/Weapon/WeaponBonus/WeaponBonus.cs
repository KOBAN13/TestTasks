using Character;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Weapon.WeaponBonus
{
    public abstract class WeaponBonus : MonoBehaviour
    {
        protected Collider Collider;
        protected CompositeDisposable _compositeDisposable = new();
        
        public abstract void Accept(IVisitWeaponBonus visitor, bool isActive);
        protected void SubscribeOnTrigger()
        {
            Collider = GetComponent<Collider>();
            Collider.OnTriggerEnterAsObservable().Subscribe(collider =>
            {
                if (collider.TryGetComponent(out ICurrentWeaponBonus playerComponents))
                {
                    playerComponents.SetCurrentWeaponBonus(this);
                    Collider.gameObject.SetActive(false);
                }
            }).AddTo(_compositeDisposable);
        }
    }
}