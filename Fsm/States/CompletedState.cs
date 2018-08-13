namespace IOToolkit.Fsm.States
{
    public class CompletedState : SingleActionState
    {
        public CompletedState() : base(CompletedStateMethod)
        {
        }

        private static void CompletedStateMethod(StateMachineData obj)
        {
        }
    }
}