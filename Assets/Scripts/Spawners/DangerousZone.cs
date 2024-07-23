using System.Collections.Generic;
using System.Linq;
using Configs;
using Cysharp.Threading.Tasks;
using Loader;
using Spawners.Zones;
using Unity.Mathematics;
using UnityEngine;
using Zenject;
using Random = System.Random;

public class DangerousZone : IInitializable
{
    private ZonesConfigs _dieZone;
    private ZonesConfigs _decelerationZone;
    private ZoneSpawnerConfig _zoneSpawnerConfig;
    private Loader.Loader _loader;
    private ReferenceLoadAsset _referenceLoad;
    private Factory.Factory _factory;

    private GameObject _prefabDieZone;
    private GameObject _prefabDecelerationZone;

    private List<Vector2> _zonePosition = new();

    public DangerousZone(Loader.Loader loader, ReferenceLoadAsset referenceLoad, Factory.Factory factory)
    {
        _loader = loader;
        _referenceLoad = referenceLoad;
        _factory = factory;
    }
    
    public async void Initialize()
    {
        await AwaitLoadConfigs();
        FindPositionZone();
    }

    private void FindPositionZone()
    {
        var mapSize = _zoneSpawnerConfig.MapSize;
        var minDistance = _zoneSpawnerConfig.MinDistance;
        var zoneSize = _zoneSpawnerConfig.ZoneSize;
        var rows = (int)Mathf.Floor((mapSize.y - minDistance) / (zoneSize.y + minDistance));
        var columns = (int)Mathf.Floor((mapSize.x - minDistance) / (zoneSize.x + minDistance));

        for (var i = 0; i < rows; i++)
        {
            for (var j = 0; j < columns; j++)
            {
                var zonePosition = new Vector2(
                    minDistance + j * (zoneSize.x + minDistance) + zoneSize.x / 2,
                    minDistance + i * (zoneSize.y + minDistance) + zoneSize.y / 2
                );
                
                if (IsPositionValid(zonePosition, zoneSize, mapSize, minDistance))
                {
                    _zonePosition.Add(zonePosition);
                }
            }
        }

        for (int i = 0; i < _dieZone.countSpawnZone; i++)
        {
            CreateZone<DieZone>(_prefabDieZone);
        }

        for (int i = 0; i < _decelerationZone.countSpawnZone; i++)
        {
            CreateZone<DecelerationZone>(_prefabDecelerationZone);
        }
    }

    private void CreateZone<T>(GameObject prefab) where T : Object
    {
        var index = FindRandomPosition();
        _factory.CreateInitDiContainer<T>(prefab, new Vector3(_zonePosition[index].x, 0f, _zonePosition[index].y), Quaternion.identity);
        _zonePosition.RemoveAt(index);
    }
    

    private int FindRandomPosition()
    {
        return UnityEngine.Random.Range(0, _zonePosition.Count);
    }
    
    
    private bool IsPositionValid(Vector2 zonePosition, Vector2 zoneSize, Vector2 mapSize, float minDistance)
    {
        if (zonePosition.x - zoneSize.x / 2 < 0 || zonePosition.x + zoneSize.x / 2 > mapSize.x ||
            zonePosition.y - zoneSize.y / 2 < 0 || zonePosition.y + zoneSize.y / 2 > mapSize.y)
        {
            return false;
        }

        return _zonePosition.All(zone => Vector2.Distance(zonePosition, zone) < minDistance == false);
    }
    
    private async UniTask AwaitLoadConfigs()
    {
        _decelerationZone = await _loader.LoadResourcesUsingReference(_referenceLoad.DecelerationZone) as ZonesConfigs;
        _dieZone = await _loader.LoadResourcesUsingReference(_referenceLoad.DieZone) as ZonesConfigs;
        _zoneSpawnerConfig = await _loader.LoadResourcesUsingReference(_referenceLoad.ZoneSpawn) as ZoneSpawnerConfig;
        _prefabDecelerationZone = await _loader.LoadResourcesUsingReference(_referenceLoad.DecelerationZonePrefab);
        _prefabDieZone = await _loader.LoadResourcesUsingReference(_referenceLoad.DieZonePrefab);
    }
}