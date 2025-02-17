using System;
using System.Collections.Generic;
using Shot_Shift.Infrastructure.Scripts.Factories;
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

}
