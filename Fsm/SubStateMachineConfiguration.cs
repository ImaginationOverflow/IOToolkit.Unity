namespace IOToolkit.Fsm
{
    public class SubStateMachineConfiguration : StateMachineConfiguration
    {
        private readonly StateMachineConfiguration _stateConfig;

        public SubStateMachineConfiguration(FiniteStateMachine fsm, StateMachineConfiguration stateConfig) : base(fsm)
        {
            _stateConfig = stateConfig;
        }

        public void End()
        {
            foreach (var s in States)
            {
                _stateConfig.States.Add(s.Key, s.Value);
            }
        }
    }
}