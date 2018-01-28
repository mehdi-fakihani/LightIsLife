using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LIL.Effects
{
    /// <summary>
    /// Effect which prevents the target to cast spells until it expires.
    /// </summary>
    public class Silence : IEffect
    {
        private readonly float time;

        public Silence(float time)
        {
            this.time = time;
        }

        protected override float duration()           { return time; }
        protected override void actualize(float secs) { }
        protected override void apply()               { ++target.silenceTokens; }
        protected override void timeout()             { --target.silenceTokens; }
    }
}
