using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace LIL.Effects
{
    /// <summary>
    /// Effect which triggers the received callback at it's expiration.
    /// </summary>
    public class Delayed : IEffect
    {
        private readonly float time;
        private readonly UnityAction callback;

        public Delayed(float time, UnityAction callback)
        {
            this.time = time;
            this.callback = callback;
        }

        protected override float duration()           { return time; }
        protected override void actualize(float secs) { }
        protected override void apply()               { }
        protected override void timeout()             { callback.Invoke(); }
    }
}
