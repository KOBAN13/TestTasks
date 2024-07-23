using System.Collections.Generic;
using Spawners.Weapon;
using UnityEngine;
using Weapon;
using Zenject;

namespace DI
{
    public class WeaponInstaller : MonoInstaller
    {
        [SerializeField] private Pistol _pistol;
        [SerializeField] private Rifle _rifle;
        public override void InstallBindings()
        {
            BindBulletSpawn();
            BindWeaponSpawner();
            BindWeaponChanger();
        }

        private void BindWeaponChanger()
        {
            Container.Bind<List<IWeapon>>().FromInstance(GetWeapons()).AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<WeaponSwitch>().AsSingle().NonLazy();
        }

        private List<IWeapon> GetWeapons()
        {
            return new List<IWeapon>()
            {
                _pistol, _rifle
            };
        }

        private void BindWeaponSpawner()
        {
            Container.BindInterfacesAndSelfTo<WeaponSpawner>().AsSingle().NonLazy();
        }


        private void BindBulletSpawn()
        {
            Container.BindInterfacesAndSelfTo<BulletSpawn>().AsSingle().NonLazy();

            Container.BindInterfacesAndSelfTo<Pistol>().FromInstance(_pistol).AsSingle().WhenInjectedInto<BulletSpawn>().NonLazy();
            Container.BindInterfacesAndSelfTo<Rifle>().FromInstance(_rifle).AsSingle().WhenInjectedInto<BulletSpawn>().NonLazy();
        }
    }
}