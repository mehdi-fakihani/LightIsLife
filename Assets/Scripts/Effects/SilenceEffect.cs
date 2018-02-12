using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LIL.Effects
{
    /// <summary>
    /// Silences the target for the given duration, if it has a SkillManager.
    /// <inheritdoc />
    /// </summary>
    public class Silence : IEffect
    {
        private readonly float time;
        private SkillManager skillsManager;

        public Silence(float time)
        {
            this.time = time;
        }

        protected override float duration() { return time; }

        protected override void update(float secs) { }
        
        protected override void apply()
        {
            skillsManager = manager.gameObject.GetComponent<SkillManager>();
            if (skillsManager != null) skillsManager.beginSilence();
        }
        public override void expire(bool onDeath)
        {
            if (skillsManager != null) skillsManager.endSilence();
        }
    }
}
