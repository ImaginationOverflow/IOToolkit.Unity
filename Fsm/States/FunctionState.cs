using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IOToolkit.Fsm.States
{
    public class FunctionState : StateBase
    {
        private readonly FiniteStateMachineDelegate _func;

        public FunctionState(FiniteStateMachineDelegate func) : base(func)
        {
            _func = func;
        }

        public override void OnEnter(StateMachineData data)
        {
        }

        public override void OnState(StateMachineData data)
        {
            Completed = _func(data);
        }

        public override void OnExit(StateMachineData data)
        {
        }

        public override string ToString()
        {
#if !WINDOWS_UWP
            return _func.Method.Name;
#else
            return "";
#endif
        }
    }
}
