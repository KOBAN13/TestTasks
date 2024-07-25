using CameraScripts;
using Character;
using Character.Score;
using Loader;
using Physics;
using Spawners.BonusSpawn;
using Spawners.EnemySpawner;
using Spawners.PointSpawnBonuse;
using UnityEngine;
using Weapon;
using Zenject;

namespace DI
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private PlayerComponents _playerComponents;
        [SerializeField] private ReferenceLoadAsset _referenceLoadAsset;
        [SerializeField] private CameraFollow _cameraFollow;
        
        public override void InstallBindings()
        {
            BindScoreSaver();
            BindPlayer();
            BindCamera();
            BindInput();
            BindPlayerParameters();
            BindGravity();
            BindLoader();
            BindMovements();
            BindFactory();
            BindCreateZone();
            BindPointCamera();
            BindEnemySpawner();
            BindBonusSpawner();
        }

        private void BindScoreSaver()
        {
            Container.BindInterfacesAndSelfTo<ScoreSaver>().AsSingle().NonLazy();
        }

        private void BindBonusSpawner()
        {
            Container.BindInterfacesAndSelfTo<BuffSpawn>().AsSingle().NonLazy();
        }

        private void BindEnemySpawner()
        {
            Container.BindInterfacesAndSelfTo<EnemySpawner>().AsSingle().NonLazy();
        }

        private void BindPointCamera()
        {
            Container.BindInterfacesAndSelfTo<PointsCamera>().AsSingle().NonLazy();
        }

        private void BindFactory()
        {
            Container.BindInterfacesAndSelfTo<Factory.Factory>().AsSingle().NonLazy();
        }

        private void BindCreateZone()
        {
            Container.BindInterfacesAndSelfTo<DangerousZone>().AsSingle().NonLazy();
        }

        private void BindCamera()
        {
            Container.BindInterfacesAndSelfTo<CameraFollow>().FromInstance(_cameraFollow).AsSingle().NonLazy();
        }


        private void BindInput()
        {
            Container.Bind<NewInputSystem>().To<NewInputSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<InputSystemPC>().AsSingle().NonLazy();
        }
        
        private void BindPlayer()
        {
            Container.BindInterfacesAndSelfTo<PlayerBaff>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<Player>().AsSingle().NonLazy();
        }

        private void BindPlayerParameters()
        {
            Container.BindInterfacesAndSelfTo<PlayerComponents>().FromInstance(_playerComponents).AsSingle().NonLazy();
        }

        private void BindGravity()
        {
            Container.BindInterfacesAndSelfTo<Gravity>().AsSingle().NonLazy();
        }
        
        private void BindLoader()
        {
            Container.Bind<Loader.Loader>().AsSingle().NonLazy();
            Container.Bind<ReferenceLoadAsset>().FromInstance(_referenceLoadAsset).AsSingle().NonLazy();
        }

        private void BindMovements()
        {
            Container.BindInterfacesAndSelfTo<Movement>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<Rotate>().AsSingle().NonLazy();
        }
    }
}