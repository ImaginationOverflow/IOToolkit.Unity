using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace IOToolkit.Fsm.States
{
    public class WaitState : StateBase
    {
        private readonly float _duration;
        private readonly Action<float> _progress;
        private float _exitTime;
        private float _startTime;

        public WaitState(float duration, Action<float> progress = null)
        {
            _duration = duration;
            _progress = progress;

        }

        public WaitState(float duration, Action<float, object> onProgress, object state) : this(duration, (p) => onProgress(p, state))
        {
        }

        public override void OnEnter(StateMachineData data)
        {
            _startTime = Time.time;
            _exitTime = _startTime + _duration;
        }

        public override void OnState(StateMachineData data)
        {
            Completed = Time.time > _exitTime;

            if (_progress != null)
            {
                var progress = (Time.time - _startTime) / (_exitTime - _startTime);

                progress = float.IsInfinity(progress) || float.IsNaN(progress) ? 0 : progress;

                if (Completed)
                    progress = 1;

                _progress(progress);
            }

        }
    }
}
