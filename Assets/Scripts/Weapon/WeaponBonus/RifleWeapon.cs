using UniRx;

namespace Weapon.WeaponBonus
{
    public class RifleWeapon : WeaponBonus
    {
        private void OnEnable()
        {
            _compositeDisposable = new CompositeDisposable();
            SubscribeOnTrigger();
        }

        private void OnDestroy()
        {
            _compositeDisposable.Clear();
            _compositeDisposable.Dispose();
        }

        public override void Accept(IVisitWeaponBonus visitor, bool isActive)
        {
            visitor.Visit(this, isActive);
        }
    }
}