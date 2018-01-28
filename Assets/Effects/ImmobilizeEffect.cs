using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LIL.Effects
{
    /// <summary>
    /// Effect which prevents the target to move until it expires.
    /// </summary>
    public class Immobilize : IEffect
    {
        private readonly float time;

        public Immobilize(float time)
        {
            this.time = time;
        }

        protected override float duration()           { return time; }
        protected override void actualize(float secs) { }
        protected override void apply()               { ++target.immobilityTokens; }
        protected override void timeout()             { --target.immobilityTokens; }
    }
}
