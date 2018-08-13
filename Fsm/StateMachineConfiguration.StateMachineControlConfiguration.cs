using System;
using IOToolkit.Fsm.States;

namespace IOToolkit.Fsm
{
    public partial class StateMachineConfiguration
    {
        public class StateMachineControlConfiguration
        {
            public StateMachineConfiguration StateMachineConfiguration { get; private set; }
            public Action<IState> StateSetter { get; private set; }

            public StateMachineControlConfiguration(StateMachineConfiguration fsmc, Action<IState> stateSetter)
            {
                StateMachineConfiguration = fsmc;
                StateSetter = stateSetter;
            }

            public StateMachineConfiguration Restart()
            {
                StateSetter(new SingleActionState((_) => StateMachineConfiguration._fsm.Reset(true)));
                return StateMachineConfiguration;
            }

            public StateMachineConfiguration Stop()
            {
                StateSetter(new SingleActionState((_) => StateMachineConfiguration._fsm.Stop()));
                return StateMachineConfiguration;
            }

            public StateMachineConfiguration ResetAndStop()
            {
                StateSetter(new SingleActionState((_) => StateMachineConfiguration._fsm.Reset(false)));
                return StateMachineConfiguration;
            }
        }

    
    }
}