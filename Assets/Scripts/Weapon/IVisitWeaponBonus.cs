using Weapon.WeaponBonus;

namespace Weapon
{
    public interface IVisitWeaponBonus
    {
        void Visit(PistolBonus pistolBonus, bool isActive);
        void Visit(RifleBonus rifleBonus, bool isActive);
        void Visit(ShotgunBonus shotgunBonus, bool isActive);
    }
}