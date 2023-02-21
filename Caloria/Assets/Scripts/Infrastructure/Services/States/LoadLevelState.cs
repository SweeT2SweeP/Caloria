using UnityEngine;

namespace Infrastructure.Services.States
{
    public class LoadLevelState : IState
    {
        private AppStateMachine _gameStateMachine;
        private SceneLoader _sceneLoader;
        private readonly GameObject _loadingCurtain;

        public LoadLevelState(
            AppStateMachine gameStateMachine, 
            SceneLoader sceneLoader, 
            GameObject loadingCurtain)
        {
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _gameStateMachine = gameStateMachine;
        }

        public void Enter()
        {
            _loadingCurtain.SetActive(true);
            _sceneLoader.Load("MainScene", onLoaded);
        }

        public void Exit() => 
            _loadingCurtain.SetActive(false);

        private void onLoaded() => 
            _gameStateMachine.Enter<AppLoopState>();
    }
}