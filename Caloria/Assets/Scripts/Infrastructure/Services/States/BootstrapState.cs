using Infrastructure.Services.ServiceLocator;

namespace Infrastructure.Services.States
{
    public class BootstrapState : IState
    {
        private const string InitialScene = "BootstrapScene";
        
        private readonly AppStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;

        public BootstrapState(AppStateMachine gameStateMachine, SceneLoader sceneLoader, AllServices services)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _services = services;
            
            RegisterServices();
        }

        public void Enter()
        {
            _sceneLoader.Load(InitialScene, onLoaded: EnterLoadLevel);
        }

        public void Exit()
        {
        }

        private void EnterLoadLevel() => 
            _gameStateMachine.Enter<LoadLevelState>();

        private void RegisterServices()
        {
            
        }
    }
}