using System.Collections.Generic;
using System.Linq;
using Character;
using Spawners.PointSpawnBonuse;
using Spawners.Weapon;
using Weapon.WeaponBonus;
using Zenject;

namespace Weapon
{
    public class WeaponSwitch : IInitializable, IVisitWeaponBonus
    {
        private ICurrentWeaponBonus _weaponBonus;
        private PointsCamera _pointsCamera;
        private WeaponBonus.WeaponBonus _currentWeapon;
        private WeaponBonus.WeaponBonus _previoseWeapon;
        private List<IWeapon> _weapons;
        private readonly ISetWeapon _setWeapon;
        private readonly WeaponSpawner _weaponSpawner;

        private IWeapon _pistol;
        private IWeapon _rifle;
        private IWeapon _shotgun;


        public WeaponSwitch(ICurrentWeaponBonus weaponBonus, List<IWeapon> weapons, ISetWeapon weapon, WeaponSpawner weaponSpawner)
        {
            _weaponBonus = weaponBonus;
            _weapons = weapons;
            _setWeapon = weapon;
            _weaponSpawner = weaponSpawner;
        }


        public void Initialize()
        {
            _pistol = _weapons.FirstOrDefault(pistol => pistol.GetType() == typeof(Pistol));
            _rifle = _weapons.FirstOrDefault(rifle => rifle.GetType() == typeof(Rifle));
            _shotgun = _weapons.FirstOrDefault(rifle => rifle.GetType() == typeof(Shotgun));
            
            _weaponBonus.Subscribe(() =>
            {
                _previoseWeapon?.Accept(this, false);
                _currentWeapon = _weaponBonus.GetCurrentWeaponBonus();
                _weaponSpawner.WeaponBonus(_currentWeapon);
                _currentWeapon.Accept(this, true);
                _previoseWeapon = _currentWeapon;
            });
        }

        public void Visit(PistolBonus pistolBonus, bool isActive)
        {
            SwitchWeapon(_pistol, isActive);
        }

        public void Visit(RifleBonus rifleBonus, bool isActive)
        {
            SwitchWeapon(_rifle, isActive);
        }

        public void Visit(ShotgunBonus shotgunBonus, bool isActive)
        {
            SwitchWeapon(_shotgun, isActive);
        }

        private void SwitchWeapon(IWeapon weapon, bool isActive)
        {
            weapon?.GetWeaponGameObject().SetActive(isActive);
            
            if(isActive == false) return;
            _setWeapon.SetWeapon(weapon);
        }
    }
}