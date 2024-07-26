using UnityEngine;
using Zenject;

namespace DI
{
    public class MenuInstaller : MonoInstaller
    {
        [SerializeField] private RecordView _recordView;
        public override void InstallBindings()
        {
            BindViews();
            BindModels();
        }

        private void BindModels()
        {
            Container.BindInterfacesAndSelfTo<ButtonModel>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<RecordModel>().AsSingle().NonLazy();
        }

        private void BindViews()
        {
            Container.BindInterfacesAndSelfTo<RecordView>().FromInstance(_recordView).AsSingle().NonLazy();
        }
    }
}