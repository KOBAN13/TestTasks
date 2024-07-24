using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Loader;
using Spawners.PointSpawnBonuse;
using UniRx;
using UnityEngine;
using Weapon.WeaponBonus;
using Zenject;
using Random = UnityEngine.Random;

namespace Spawners.BonusSpawn
{
    public class BuffSpawn : IInitializable, IDisposable
    {
        private readonly ReferenceLoadAsset _referenceLoadAsset;
        private readonly Factory.Factory _factory;
        private readonly CompositeDisposable _compositeDisposable = new();
        private readonly PointsCamera _pointsCamera;
        private readonly Loader.Loader _loader;
        private List<GameObject> _bonus;

        public BuffSpawn(Factory.Factory factory, PointsCamera pointsCamera, Loader.Loader loader, ReferenceLoadAsset referenceLoadAsset)
        {
            _referenceLoadAsset = referenceLoadAsset;
            _factory = factory;
            _pointsCamera = pointsCamera;
            _loader = loader;
        }


        public async void Initialize()
        {
            _bonus = await _loader.LoadAllResourcesUseLabel<GameObject>(_referenceLoadAsset.BonusPrefab);
            
            Debug.LogWarning("старт");
            
            Observable
                .Timer(TimeSpan.FromSeconds(5f), TimeSpan.FromSeconds(10f))
                .Subscribe(_ =>
                {
                    StartSpawn().Forget();
                    Debug.LogWarning("спавн бонуса");
                })
                .AddTo(_compositeDisposable);
        }
        
        private async UniTaskVoid StartSpawn()
        {
            var gameObject = _factory.CreateInitDiContainer<BuffBonus>(GetRandomElementInList(), _pointsCamera.Visible(),
                Quaternion.identity);

            await UniTask.Delay(TimeSpan.FromSeconds(5f));
            _loader.ClearMemoryInstance(gameObject.gameObject);
            gameObject.gameObject.SetActive(false);
        }

        private GameObject GetRandomElementInList()
        {
            return _bonus[Random.Range(0, _bonus.Count)];
        }

        public void Dispose()
        {
            _compositeDisposable?.Dispose();
        }
    }
}