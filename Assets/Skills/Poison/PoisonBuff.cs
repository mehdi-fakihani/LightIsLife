using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LIL.Effects
{

    public class PoisonBuff : IEffect
    {
        private readonly float time;
        public readonly float effectTime;
        private readonly Func<Poison> impactFactory;

        public PoisonBuff(float time, float effectTime, Func<Poison> impactFactory)
        {
            this.time = time;
            this.effectTime = effectTime;
            this.impactFactory = impactFactory;
        }

        public Poison makeEffect()
        {
            return impactFactory();
        }

        protected override float duration()        { return time; }
        protected override void apply()            { }
        protected override void update(float secs) { }
        public override void expire(bool onDeath)  { }
    }

}
