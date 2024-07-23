using Weapon.WeaponBonus;

namespace Weapon
{
    public interface IVisitWeaponBonus
    {
        void Visit(PistolWeapon pistolWeapon, bool isActive);
        void Visit(RifleWeapon rifleWeapon, bool isActive);
    }
}