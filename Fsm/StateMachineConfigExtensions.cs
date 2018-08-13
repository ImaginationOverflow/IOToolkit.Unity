using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IOToolkit.Fsm.States;
using UnityEngine;

namespace IOToolkit.Fsm
{
    public static class StateMachineConfigExtensions
    {
        public static StateMachineConfiguration Wait(this StateMachineConfiguration config, float f)
        {
            return config.Then(new WaitState(f));
        }

        public static StateMachineConfiguration WaitProgress(this StateMachineConfiguration config, float f, Action<float> onProgress)
        {
            return config.Then(new WaitState(f, onProgress));
        }

        public static StateMachineConfiguration WaitProgress(this StateMachineConfiguration config, float f, Action<float, object> onProgress, object state)
        {
            return config.Then(new WaitState(f, onProgress, state));
        }


        public static StateMachineConfiguration ThenDisableCollider2D(this StateMachineConfiguration config, GameObject parent)
        {
            var go = parent;
            return config.Then(new SingleActionState((_) => go.GetComponent<Collider2D>().enabled = false));
        }

        public static StateMachineConfiguration ThenEnableColliders2D(this StateMachineConfiguration config, GameObject parent)
        {
            var go = parent;
            return config.Then(new SingleActionState((_) =>
             {
                 var cols = go.GetComponents<Collider2D>();
                 for (int i = 0; i < cols.Length; i++)
                 {
                     cols[i].enabled = true;
                 }
             }));
        }

        public static StateMachineConfiguration ThenEnableCollider2D(this StateMachineConfiguration config, GameObject parent)
        {
            var go = parent;
            return config.Then(new SingleActionState((_) => go.GetComponent<Collider2D>().enabled = true));
        }

        public static StateMachineConfiguration ThenDisableCollider2D(this StateMachineConfiguration.StateMachineControlConfiguration config, GameObject parent)
        {
            var go = parent;
            config.StateSetter(new SingleActionState((_) => go.GetComponent<Collider2D>().enabled = false));
            return config.StateMachineConfiguration;
        }

        public static StateMachineConfiguration ThenDisableColliders2D(this StateMachineConfiguration.StateMachineControlConfiguration config, GameObject parent)
        {
            var go = parent;
            config.StateSetter(new SingleActionState((_) =>
            {
                var cols = go.GetComponents<Collider2D>();
                for (int i = 0; i < cols.Length; i++)
                {
                    cols[i].enabled = false;
                }
            }));
            return config.StateMachineConfiguration;
        }



        public static StateMachineConfiguration ThenEnableCollider2D(this StateMachineConfiguration.StateMachineControlConfiguration config, GameObject parent)
        {
            var go = parent;
            config.StateSetter(new SingleActionState((_) => go.GetComponent<Collider2D>().enabled = true));
            return config.StateMachineConfiguration;
        }

        public static StateMachineConfiguration ThenEnableColliders2D(this StateMachineConfiguration.StateMachineControlConfiguration config, GameObject parent)
        {
            var go = parent;
            config.StateSetter(new SingleActionState((_) =>
            {
                var cols = go.GetComponents<Collider2D>();
                for (int i = 0; i < cols.Length; i++)
                {
                    cols[i].enabled = true;
                }
            }));
            return config.StateMachineConfiguration;
        }


    }
}
