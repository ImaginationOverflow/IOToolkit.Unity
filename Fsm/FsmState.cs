using System;

namespace IOToolkit.Fsm
{
    public class FsmState
    {
        public IState State { get; set; }
        public Func<bool> TransitionCondition { get; set; }
        public FsmState OnTransitionTrue { get; set; }
        public FsmState OnTransitionFalse { get; set; }

        protected bool Equals(FsmState other)
        {
            return Equals(State, other.State);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((FsmState) obj);
        }

        public override int GetHashCode()
        {
            return (State != null ? State.GetHashCode() : 0);
        }

        public static bool operator ==(FsmState left, FsmState right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(FsmState left, FsmState right)
        {
            return !Equals(left, right);
        }

        public override string ToString()
        {
            return State.ToString();
        }
    }
}