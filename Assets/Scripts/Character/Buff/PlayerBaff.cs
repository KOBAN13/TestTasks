using System.Collections.Generic;
using Character.Buff;
using Loader;
using PlayerConfigs;
using Zenject;

namespace Character
{
    public class PlayerBaff : IBuffable, IInitializable
    {
        private readonly Loader.Loader _addressableLoader;
        private readonly ReferenceLoadAsset _nameLoaderResources;
        private PlayerSettings _playerSettings;
        private List<IBuff> _buffs = new();
        public CharacterStats BaseStats { get; private set; }
        public CharacterStats CurrentStats { get; private set; }
        

        public PlayerBaff(Loader.Loader addressableLoader, ReferenceLoadAsset nameLoaderResources)
        {
            _addressableLoader = addressableLoader;
            _nameLoaderResources = nameLoaderResources;
        }
        
        public void AddBuff(IBuff buff)
        {
            _buffs.Add(buff);
            
            ApplyBuff();
        }

        public void RemoveBuff(IBuff buff)
        {
            _buffs.Remove(buff);
            
            ApplyBuff();
        }

        private void ApplyBuff()
        {
            CurrentStats = BaseStats;

            foreach (var buff in _buffs)
            {
                CurrentStats = buff.AddBuff(CurrentStats);
            }
        }

        public async void Initialize()
        {
            _playerSettings = await _addressableLoader.LoadResourcesUsingReference(_nameLoaderResources.PlayerSettings) as PlayerSettings;
            BaseStats = new CharacterStats { IsImmortal = false, Speed = _playerSettings.Speed };
            CurrentStats = BaseStats;
        }
    }
}