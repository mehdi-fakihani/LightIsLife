using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LIL.Effects
{
    public class IcyBlastImpact : IEffect
    {
        private float time;
        private float slow;
        private bool effective;

        public IcyBlastImpact(float time, float slow)
        {
            this.time = time;
            this.slow = slow;
        }

        protected override float duration() { return time; }

        protected override void update(float secs) { }

        protected override void apply()
        {
            var effect = manager.getEffect<IcyBlastImpact>();
            effective = effect == null;
            if (effective)
            {
                var movement = manager.gameObject.GetComponent<EnemyMovement>();
                //movement.slow(slow);
            }
            else
            {
                effect.time = time;
                manager.expireEffect(this);
            }
        }

        public override void expire(bool onDeath)
        {
            if (!effective) return;
            var movement = manager.gameObject.GetComponent<EnemyMovement>();
            //movement.slow(1f / slow);
        }
    }
}
