using Configs.Game;
using Infrastructure.AssetManagement;
using Services;
using Services.MapGenerator;
using UI;

namespace Infrastructure.States
{
    public class BootstrapState : IState
    {
        private const string Initial = "InitialScene";
        private const string MainMenu = "MenuScene";
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;
        private readonly GameConfig _config;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices services, GameConfig config)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _services = services;
            _config = config;
            RegisterServices();
        }

        public void Enter() =>
          _sceneLoader.Load(Initial, onLoaded: EnterLoadMenu);

        public void Exit()
        {
        }

        private void RegisterServices()
        {
            _services.RegisterSingle<IAssetProvider>(new AssetProvider());
            _services.RegisterSingle(new MenuService());
            _services.RegisterSingle<IMapLoader>(new MapFromFileLoader());
            _services.RegisterSingle<IMapInstatiator>(new MapInstatiator(_config.MapConfig, _services.Single<IAssetProvider>()));
        }

        private void EnterLoadMenu()
        {
            _stateMachine.Enter<LoadMenuState, string>(MainMenu);
        }
    }
}
