﻿using System;
using System.Collections.Generic;
using System.Linq;
using Enemy.Config;
using Loader;
using Spawners.PointSpawnBonuse;
using UniRx;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Spawners.EnemySpawner
{
    public class EnemySpawner : IInitializable
    {
        private PointsCamera _pointsCamera;
        private readonly Factory.Factory _factory;
        private readonly Loader.Loader _loader;
        private readonly ReferenceLoadAsset _referenceLoadAsset;

        private List<EnemyConfig> _enemyConfigs;
        private CompositeDisposable _compositeDisposable = new();

        public EnemySpawner(PointsCamera pointsCamera, Factory.Factory factory, Loader.Loader loader, ReferenceLoadAsset referenceLoadAsset)
        {
            _pointsCamera = pointsCamera;
            _factory = factory;
            _loader = loader;
            _referenceLoadAsset = referenceLoadAsset;
        }

        public async void Initialize()
        { 
            _enemyConfigs = await _loader.LoadAllResourcesUseLabel<EnemyConfig>(_referenceLoadAsset.EnemyConfigs);

            Observable
                .Timer(TimeSpan.FromSeconds(2f), TimeSpan.FromSeconds(2f))
                .Subscribe(_ =>
                {
                    StartSpawn();
                })
                .AddTo(_compositeDisposable);
        }
        
        private void StartSpawn()
        {
            var enemy = GetRandomEnemy();
            _pointsCamera.Invisible(out var position);
            _factory.CreateInitDiContainerAndInitializeEnemy(enemy.EnemyPrefab, enemy, position, Quaternion.identity);
        }

        private EnemyConfig GetRandomEnemy()
        {
            if (Mathf.Abs(_enemyConfigs.Sum(x => x.CoefficientSpawn)) - 1f > float.Epsilon) throw new FormatException();

            var value = Random.Range(0f, 1f);
            Debug.Log(value);
            
            var cumulativeChance = 0f;
            
            foreach (var enemy in _enemyConfigs)
            {
                cumulativeChance += enemy.CoefficientSpawn;
                if (value <= cumulativeChance)
                {
                    return enemy;
                }
            }
            
            return _enemyConfigs[^1];
        }
    }
}