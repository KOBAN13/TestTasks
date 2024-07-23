using UnityEngine;
using Weapon.Configs;

namespace Weapon
{
    public interface IWeapon
    {
        GameObject GetWeaponGameObject();
        void Fire();
    }
}