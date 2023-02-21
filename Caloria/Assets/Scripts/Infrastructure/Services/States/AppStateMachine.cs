using System;
using System.Collections.Generic;
using Infrastructure.Services.ServiceLocator;
using UnityEngine;

namespace Infrastructure.Services.States
{
    public class AppStateMachine
    {
        private readonly Dictionary<Type, IState> _states;
        private IState _activeState;
        
        public AppStateMachine(SceneLoader sceneLoader, GameObject curtain, AllServices container)
        {
            _states = new Dictionary<Type, IState>
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, container),
                [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader, curtain),
                [typeof(AppLoopState)] = new AppLoopState(this)
            };
        }

        public void Enter<TState>() where TState : class, IState
        {
            var state = ChangeState<TState>();
            state.Enter();
        }

        private TState ChangeState<TState>() where TState : class, IState
        {
            _activeState?.Exit();
            
            var state = GetState<TState>();
            _activeState = state;
            
            return state;
        }

        private TState GetState<TState>() where TState : class, IState => 
            _states[typeof(TState)] as TState;
    }
}