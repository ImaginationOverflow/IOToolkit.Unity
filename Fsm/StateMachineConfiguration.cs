using System;
using System.Collections.Generic;
using IOToolkit.Fsm.States;


namespace IOToolkit.Fsm
{
    public partial class StateMachineConfiguration
    {
        private readonly FiniteStateMachine _fsm;
        public Dictionary<int, FsmState> States { get; private set; }
        public FsmState InitialState { get; private set; }

        private FsmState _currentState;
        private FsmState _forkedState;


        public StateMachineConfiguration(FiniteStateMachine fsm)
        {
            _fsm = fsm;
            States = new Dictionary<int, FsmState>();
        }

        public StateMachineConfiguration StartAt(IState state)
        {
            InitialState = _currentState = GetOrCreateState(state);
            return this;
        }

        public StateMachineConfiguration StartAt(FiniteStateMachineDelegate state)
        {
            return StartAt(new FunctionState(state));
        }
        public StateMachineConfiguration Then(IState state)
        {
            var currState = _currentState;
            _currentState.TransitionCondition = () => currState.State.Completed;
            _currentState.OnTransitionFalse = _currentState;

            _currentState = _currentState.OnTransitionTrue = GetOrCreateState(state);
            return this;
        }

        public StateMachineConfiguration Then(FiniteStateMachineDelegate state)
        {
            return Then(new FunctionState(state));
        }

        public StateMachineControlConfiguration Then()
        {
            return new StateMachineControlConfiguration(this, state => Then(state));
        }

        public StateMachineConfiguration Fork(Func<bool> checkIfMiss)
        {
            Then(new CompletedState());
            _currentState.TransitionCondition = checkIfMiss;
            _forkedState = _currentState;
            return this;
        }


        public StateMachineConfiguration OnTrue(IState state)
        {
            if (_forkedState.OnTransitionTrue != null)
                throw new InvalidOperationException("Already setted " + state);

            _currentState = _forkedState.OnTransitionTrue = GetOrCreateState(state);

            return this;
        }

        public StateMachineConfiguration OnFalse(FiniteStateMachineDelegate state)
        {
            return OnFalse(new FunctionState(state));
        }

        public StateMachineConfiguration OnFalse(IState state)
        {
            if (_forkedState.OnTransitionFalse != null)
                throw new InvalidOperationException("Already setted " + state);

            _currentState = _forkedState.OnTransitionFalse = GetOrCreateState(state);

            return this;
        }

        public StateMachineConfiguration OnTrue(FiniteStateMachineDelegate state)
        {
            return OnTrue(new FunctionState(state));
        }

        /// <summary>
        /// Replaces the transition of previous state
        /// </summary>
        /// <param name="transitionCondition"></param>
        /// <returns></returns>
        public StateMachineConfiguration If(Func<bool> transitionCondition)
        {
            _currentState.TransitionCondition = transitionCondition;
            return this;
        }

        public StateMachineConfiguration True(IState state)
        {
            if (_currentState.OnTransitionTrue != null)
                throw new InvalidOperationException("Already setted " + state);

            _currentState.OnTransitionTrue = GetOrCreateState(state);

            UpdateCurrentStateIfAllConditionsSet(_currentState, _currentState.OnTransitionTrue);
            return this;
        }

        public StateMachineConfiguration True(FiniteStateMachineDelegate state)
        {
            return True(new FunctionState(state));
        }

        public StateMachineControlConfiguration True()
        {
            return new StateMachineControlConfiguration(this, state => True(state));
        }

        public StateMachineConfiguration False(IState state)
        {
            if (_currentState.OnTransitionFalse != null)
                throw new InvalidOperationException("Already setted " + state);

            _currentState.OnTransitionFalse = GetOrCreateState(state);

            UpdateCurrentStateIfAllConditionsSet(_currentState, _currentState.OnTransitionFalse);
            return this;
        }
        public StateMachineConfiguration False(FiniteStateMachineDelegate state)
        {
            return False(new FunctionState(state));
        }

        public StateMachineControlConfiguration False()
        {
            return new StateMachineControlConfiguration(this, state => False(state));
        }

        public SubStateMachineConfiguration RegisterSubStateMachine()
        {
            return new SubStateMachineConfiguration(_fsm, this);
        }

        public void End()
        {
            if ((this is SubStateMachineConfiguration) == false)
                throw new InvalidOperationException("This is not an SubStateMachineConfiguration");

            ((SubStateMachineConfiguration)this).End();
        }

        private void UpdateCurrentStateIfAllConditionsSet(FsmState currentState, FsmState lastSettedState)
        {
            if (currentState.OnTransitionFalse != null && currentState.OnTransitionTrue != null)
                _currentState = lastSettedState;
        }


        private FsmState GetOrCreateState(IState state)
        {
            FsmState s;
            if (States.TryGetValue(state.GetHashCode(), out s) == false)
            {
                s = new FsmState
                {
                    State = state
                };

                States.Add(state.GetHashCode(), s);
            }

            return s;
        }
    }


}