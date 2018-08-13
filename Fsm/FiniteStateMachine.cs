using System.Linq;
using System.Text;
using UnityEngine;

namespace IOToolkit.Fsm
{
    public class FiniteStateMachine
    {
        private StateMachineConfiguration _configuration;
        public IState CurrentState { get { return _currState.State; } }
        public StateMachineData Data { get; private set; }
        public bool IsRunning { get; private set; }
        private FsmState _currState;


#if DEBUG
        public bool PrintCurrStateDebug { get; set; }
#endif

        public void Start()
        {
#if DEBUG
            CheckStates();
#endif
            Data = new StateMachineData();
            _currState = _configuration.InitialState;
            _currState.State.OnEnter(Data);
            IsRunning = true;
        }

        public void Step()
        {
            if (IsRunning == false)
                return;

            FsmState nextState;
            _currState.State.OnState(Data);

#if DEBUG
            if(PrintCurrStateDebug)
                Debug.Log(_currState.State);
#endif

            if(_currState.TransitionCondition == null)
                return;

            if (_currState.TransitionCondition())
                nextState = _currState.OnTransitionTrue;
            else
                nextState = _currState.OnTransitionFalse;


            if (_currState == nextState)
                return;

            _currState.State.OnExit(Data);

            nextState.State.OnEnter(Data);
            _currState = nextState;

        }

        public void Stop()
        {
            IsRunning = false;
        }
        public void Reset(bool restart = false)
        {
            Stop();

            if (_configuration == null)
                return;

            foreach (var state in _configuration.States.Values)
            {
                state.State.Reset();
            }
            if (restart)
                Start();
        }

        private void CheckStates()
        {
            foreach (var state in _configuration.States.Values)
            {
                if (state.TransitionCondition == null || state.OnTransitionFalse == null ||
                    state.OnTransitionTrue == null || state.State == null)
                {
                    //Debug.Log("State not configured! " + state.ToString());
                }
            }
        }

        public StateMachineConfiguration GetConfiguration()
        {
            return _configuration ?? (_configuration = new StateMachineConfiguration(this));
        }

        public void Dispose()
        {
            Reset();
        }
    }
}
