using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LIL.Effects
{
    public class Delayed : IEffect
    {
        private readonly float time;
        private readonly bool triggerOnDeath;
        private readonly Action callback;

        public Delayed(float time, bool triggerOnDeath, Action callback)
        {
            this.time = time;
            this.triggerOnDeath = triggerOnDeath;
            this.callback = callback;
        }

        protected override float duration() { return time; }

        protected override void update(float secs) { }

        protected override void apply() { }

        public override void expire(bool onDeath)
        {
            if (!onDeath || triggerOnDeath) callback.Invoke();
        }
    }
}
