using Infrastructure.Services.ServiceLocator;
using Infrastructure.Services.States;
using UnityEngine;

namespace Infrastructure
{
    public class App
    {
        public AppStateMachine _stateMachine;

        public App(ICoroutineRunner coroutineRunner, GameObject curtain)
        {
            _stateMachine = new AppStateMachine(new SceneLoader(coroutineRunner), curtain, AllServices.Container);
        }
    }
}