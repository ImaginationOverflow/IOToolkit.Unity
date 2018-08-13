
namespace IOToolkit.Fsm.States
{
    public abstract class StateBase : IState
    {
        private readonly object _obj;

        protected StateBase(object hashEqualObj)
        {
            _obj = hashEqualObj;

            if (_obj is System.Delegate)
            {
#if !WINDOWS_UWP
                var res = ((System.Delegate)_obj).Method;
#else
                var methodInfo = System.Reflection.RuntimeReflectionExtensions.GetMethodInfo((System.Delegate) _obj);
                var res = methodInfo;
#endif

                _obj = res;
            }
        }
        protected StateBase()
        {
        }

        public virtual bool Completed { get; protected set; }
        public virtual void Reset()
        {
            Completed = false;
        }

        public virtual void OnEnter(StateMachineData data) { }
        public virtual void OnState(StateMachineData data) { }
        public virtual void OnExit(StateMachineData data) { }


        protected bool Equals(StateBase other)
        {
            return Equals(_obj, other._obj);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((StateBase)obj);
        }

        public override int GetHashCode()
        {

            return (_obj != null ? _obj.GetHashCode() : base.GetHashCode());
        }

        public static bool operator ==(StateBase left, StateBase right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(StateBase left, StateBase right)
        {
            return !Equals(left, right);
        }

        public override string ToString()
        {
            return _obj.ToString();
        }
    }
}