using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LIL.Effects
{
    /// <summary>
    /// Slow a game object which has a MovementManager for the given time (in seconds) and slow
    /// ratio (in percent).
    /// </summary>
    public class Slow : IEffect
    {
        private readonly float time;
        private readonly float slowRatio;
        private MovementManager movement;

        public Slow(float time, float slowRatio)
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