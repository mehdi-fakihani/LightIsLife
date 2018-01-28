using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LIL.Effects
{
    /// <summary>
    /// Effect which slows the target until it expires.
    /// </summary>
    public class Slow : IEffect
    {
        private readonly float time;
        private readonly float ratio;

        public Slow(float time, float ratio)
        {
            this.time = time;
            this.ratio = ratio;
        }

        protected override float duration()           { return time; }
        protected override void actualize(float secs) { }
        protected override void apply()               { target.speedRatio *= (1f - ratio); }
        protected override void timeout()             { target.speedRatio /= (1f - ratio); }
    }
}
