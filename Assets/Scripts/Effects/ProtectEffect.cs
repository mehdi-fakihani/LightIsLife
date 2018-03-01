using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LIL.Effects
{
    /// <summary>
    /// Modify the protection value of a game object which has a HealthManager for the given time
    /// (in seconds) and ratio (in percent).
    /// </summary>
    public class Protect : IEffect
    {
        private readonly float time;
        private readonly float protectionRatio;
        private HealthManager health;

        public Protect(float time, float protectionRatio)
        {
            this.time = time;
            this.protectionRatio = protectionRatio;
        }

        protected override float duration() { return time; }

        protected override void update(float secs) { }

        protected override void apply()
        {
            health = manager.gameObject.GetComponent<HealthManager>();
            health.beginProtection(protectionRatio);
        }

        public override void expire(bool onDeath)
        {
            health.endProtection(protectionRatio);
        }
    }
}
