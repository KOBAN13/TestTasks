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

        private IWeapon pistol;
        private IWeapon rifle;


        public WeaponSwitch(ICurrentWeaponBonus weaponBonus, List<IWeapon> weapons, ISetWeapon weapon, WeaponSpawner weaponSpawner)
        {
            _weaponBonus = weaponBonus;
            _weapons = weapons;
            _setWeapon = weapon;
            _weaponSpawner = weaponSpawner;
        }


        public void Initialize()
        {
            pistol = _weapons.FirstOrDefault(pistol => pistol.GetType() == typeof(Pistol));
            rifle = _weapons.FirstOrDefault(rifle => rifle.GetType() == typeof(Rifle));
            
            _weaponBonus.Subscribe(() =>
            {
                _previoseWeapon?.Accept(this, false);
                _currentWeapon = _weaponBonus.GetCurrentWeaponBonus();
                _weaponSpawner.WeaponBonus(_currentWeapon);
                _currentWeapon.Accept(this, true);
                _previoseWeapon = _currentWeapon;
            });
        }

        public void Visit(PistolWeapon pistolWeapon, bool isActive)
        {
            pistol?.GetWeaponGameObject().SetActive(isActive);
            
            if(isActive == false) return;
            _setWeapon.SetWeapon(pistol);
        }

        public void Visit(RifleWeapon rifleWeapon, bool isActive)
        {
            rifle?.GetWeaponGameObject().SetActive(isActive);
            
            if(isActive == false) return;
            _setWeapon.SetWeapon(rifle);
        }
    }
}