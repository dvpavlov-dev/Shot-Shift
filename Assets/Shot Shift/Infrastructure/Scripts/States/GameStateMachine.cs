using System;
using System.Collections.Generic;
using Shot_Shift.Infrastructure.Scripts.Factories;
using Shot_Shift.Infrastructure.Scripts.Services;
using Shot_Shift.UI.Scripts;
using UnityEngine;
using IInitializable = Zenject.IInitializable;

namespace Shot_Shift.Infrastructure.Scripts
{
    public class GameStateMachine : IInitializable
    {
        private readonly StateFactory _stateFactory;

        private Dictionary<Type, IExitableState> _states;
        private IExitableState _currentState;

        public GameStateMachine(StateFactory stateFactory)
        {
            _stateFactory = stateFactory;
        }

        public void Initialize()
        {
            _states = new Dictionary<Type, IExitableState>
            {
                [typeof(BootstrapState)] = _stateFactory.CreateState<BootstrapState>(),
                [typeof(StartSceneState)] = _stateFactory.CreateState<StartSceneState>(),
                [typeof(GameLoopState)]  = _stateFactory.CreateState<GameLoopState>()
            };
            
            Enter<BootstrapState>();
        }

        public void Enter<TState>() where TState : class, IState =>
            ChangeState<TState>()
                .Enter();

        // public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload> =>
        //     ChangeState<TState>()
        //         .Enter(payload);


        private TState GetState<TState>() where TState : class, IExitableState => 
            _states[typeof(TState)] as TState;

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _currentState?.Exit();

            var state = GetState<TState>();
            _currentState = state;
            
            Debug.Log($"state changed to {_currentState.GetType().Name}");

            return state;
        }
    }

    public class BootstrapState : IState
    {
        private readonly GameStateMachine _gameStateMachine;

        public BootstrapState(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        public void Enter()
        {
            _gameStateMachine.Enter<StartSceneState>();
        }
        
        public void Exit()
        {
        }
    }

    public class StartSceneState : IState
    {
        private readonly ILoadingCurtains _loadingCurtains;
        private readonly ISceneLoaderService _sceneLoaderService;

        public StartSceneState(ISceneLoaderService sceneLoaderService, ILoadingCurtains loadingCurtains)
        {
            _loadingCurtains = loadingCurtains;
            _sceneLoaderService = sceneLoaderService;
        }

        public void Enter()
        {
            _loadingCurtains.ShowLoadingCurtains("Loading game...");
            _sceneLoaderService.LoadScene("Start", OnLoadedScene);
        }
        
        public void Exit()
        {
            
        }
        
        private void OnLoadedScene()
        {
            _loadingCurtains.HideLoadingCurtains();
        }
    }
    
    public class GameLoopState : IState
    {
        private readonly ILevelProgressService _levelProgressService;
        private readonly ILoadingCurtains _loadingCurtains;
        private readonly ISceneLoaderService _sceneLoaderService;
        private readonly IActorsFactory _actorsFactory;
        private readonly IBulletsFactory _bulletsFactory;

        public GameLoopState(ILevelProgressService levelProgressService, 
            ILoadingCurtains loadingCurtains, 
            ISceneLoaderService sceneLoaderService, 
            IActorsFactory actorsFactory,
            IBulletsFactory bulletsFactory)
        {
            _levelProgressService = levelProgressService;
            _loadingCurtains = loadingCurtains;
            _sceneLoaderService = sceneLoaderService;
            _actorsFactory = actorsFactory;
            _bulletsFactory = bulletsFactory;
        }

        public void Enter()
        {
            _loadingCurtains.ShowLoadingCurtains("Loading level...");
            _sceneLoaderService.LoadScene("GameLoop", OnLoadedScene);
        }
        
        public void Exit()
        {
            
        }
        
        private void OnLoadedScene()
        {
            _actorsFactory.InitializeFactory(OnInitializedEnded);
            _bulletsFactory.InitializeFactory(OnInitializedEnded);
        }

        private void OnInitializedEnded()
        {
            _loadingCurtains.HideLoadingCurtains();
            _levelProgressService.LevelProgressWatcher.RunLevel();
        }
    }
}
