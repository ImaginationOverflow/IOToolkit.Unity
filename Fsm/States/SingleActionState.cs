using System;

namespace IOToolkit.Fsm.States
{
    public class SingleActionState : StateBase
    {
        private Action<StateMachineData> _state;

        public SingleActionState(Action<StateMachineData> state) : base(state)
        {
            _state = state;
        }

        public override void OnEnter(StateMachineData data)
        {
        }


        public override void OnState(StateMachineData data)
        {
            _state(data);
            Completed = true;
        }

        public override void OnExit(StateMachineData data)
        {
        }

        public override string ToString()
        {
#if !WINDOWS_UWP
            return _state.Method.Name;
#else
            return "";
#endif
        }
    }
}