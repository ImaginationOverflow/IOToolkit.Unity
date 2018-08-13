using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IOToolkit.Fsm
{
    public interface IState
    {
        bool Completed { get; }
        void OnEnter(StateMachineData data);
        void OnState(StateMachineData data);
        void OnExit(StateMachineData data);
        void Reset();
    }
}
