using SaveSystem;
using Zenject;

namespace DI
{
    public class DataInstaller : MonoInstaller   
    {
        public override void InstallBindings()
        {
            BindData();
        }

        private void BindData()
        {
            Container.Bind<GameData>().To<GameData>().AsSingle().NonLazy();
            Container.Bind<JsonDataContext>().AsSingle().NonLazy();
        }
    }
}