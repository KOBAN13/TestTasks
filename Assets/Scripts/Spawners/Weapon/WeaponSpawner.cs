using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Loader;
using Spawners.PointSpawnBonuse;
using UniRx;
using Unity.VisualScripting;
using UnityEngine;
using Weapon.WeaponBonus;
using IInitializable = Zenject.IInitializable;
using Random = UnityEngine.Random;

namespace Spawners.Weapon
{
    public class WeaponSpawner : IInitializable, IDisposable
    {
        private PointsCamera _pointsCamera;
        private readonly Factory.Factory _factory;
        private readonly Loader.Loader _loader;
        private readonly ReferenceLoadAsset _referenceLoadAsset;

        private event Action<WeaponBonus> OnWeaponBonus;

        private Dictionary<Type, GameObject> _dictionary;
        private (Type key, GameObject gameObject) _currentWeapon;
        private CompositeDisposable _compositeDisposable = new();

        public WeaponSpawner(PointsCamera pointsCamera, Factory.Factory factory, Loader.Loader loader, ReferenceLoadAsset referenceLoadAsset)
        {
            _pointsCamera = pointsCamera;
            _factory = factory;
            _loader = loader;
            _referenceLoadAsset = referenceLoadAsset;
        }

        private void CurrentWeapon(WeaponBonus weaponBonus)
        {
            var typeBonus = weaponBonus.GetType();
            
            if (_currentWeapon.key != null && _dictionary.ContainsKey(_currentWeapon.key) == false)
            {
                _dictionary.Add(_currentWeapon.key, _currentWeapon.gameObject);
            } 
            
            if (_dictionary.TryGetValue(typeBonus, out GameObject gameObject))
            {
                _currentWeapon = (typeBonus, gameObject);
            }
            

            _dictionary.Remove(typeBonus);
        }

        public async void Initialize()
        {
            OnWeaponBonus += CurrentWeapon;
            
            _dictionary = new()
            {
                {typeof(PistolBonus), await _loader.LoadResourcesUsingReference(_referenceLoadAsset.PistolBonus)},
                {typeof(RifleBonus), await _loader.LoadResourcesUsingReference(_referenceLoadAsset.RifleBonus)},
                {typeof(ShotgunBonus), await _loader.LoadResourcesUsingReference(_referenceLoadAsset.ShotgunBonus)}
            };

            Observable
                .Timer(TimeSpan.FromSeconds(10f), TimeSpan.FromSeconds(15f))
                .Subscribe(_ =>
                {
                    StartSpawn().Forget();
                    Debug.LogWarning("спавн");
                })
                .AddTo(_compositeDisposable);
        }
        
        private async UniTaskVoid StartSpawn()
        {
            var gameObject = _factory.CreateInitDiContainer<WeaponBonus>(_dictionary[GetRandomKey()], _pointsCamera.Visible(),
                Quaternion.identity);

            await UniTask.Delay(TimeSpan.FromSeconds(10f));
            _loader.ClearMemoryInstance(gameObject.GameObject());
            gameObject.GameObject().SetActive(false);
        }

        private Type GetRandomKey()
        {
            var index = Random.Range(0, _dictionary.Keys.Count);
            Type[] keys = new Type[_dictionary.Keys.Count];
            
            _dictionary.Keys.CopyTo(keys, 0);
            return keys[index];
        }

        public void WeaponBonus(WeaponBonus obj)
        {
            OnWeaponBonus?.Invoke(obj);
        }

        public void Dispose()
        {
            OnWeaponBonus -= CurrentWeapon;
            _compositeDisposable.Clear();
            _compositeDisposable.Dispose();
        }
    }
}