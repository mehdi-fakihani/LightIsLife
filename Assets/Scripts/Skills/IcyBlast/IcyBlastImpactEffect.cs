using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LIL.Effects
{
    /// <summary>
    /// Acts as a non-cumulable slow (which duration is reset on multiple impacts).
    /// </summary>
    public class IcyBlastImpact : IEffect
    {
        private readonly float time;
        private readonly float slowRatio;
        private MovementManager movement;

        public IcyBlastImpact(float time, float slowRatio)
        {
            this.time = time;
            this.slowRatio = slowRatio;
        }

        protected override float duration() { return time; }

        protected override void update(float secs) { }

        protected override void apply()
        {
            movement = manager.gameObject.GetComponent<MovementManager>();
            movement.beginSlow(slowRatio);
        }

        public override void expire(bool onDeath)
        {
            movement.endSlow(slowRatio);
        }
    }
}
