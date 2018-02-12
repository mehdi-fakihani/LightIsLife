using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LIL.Effects
{
    /// <summary>
    /// Immobilize a game object which has a MovementManager for the given time (in seconds).
    /// </summary>
    public class Immobilize : IEffect
    {
        private float time;

        public Immobilize(float time)
        {
            this.time = time;
        }

        protected override float duration() { return time; }

        protected override void update(float secs) { }

        protected override void apply()
        {
            manager.GetComponent<MovementManager>().beginImmobilization();
        }

        public override void expire(bool onDeath)
        {
            manager.GetComponent<MovementManager>().endImmobilization();
        }
    }
}