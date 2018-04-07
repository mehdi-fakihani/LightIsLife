using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LIL.Effects
{

    public class Poison : IEffect
    {
        private readonly PoisonSkill skill;
        private readonly float ticksCount;
        private GameObject effect;
        private float tickTime;

        public Poison(PoisonSkill skill)
        {
            this.skill = skill;
            ticksCount = skill.effectDuration / skill.tickStep;
        }

        protected override float duration() { return skill.effectDuration; }

        protected override void apply()
        {
            effect = Object.Instantiate(skill.impactEffect, manager.transform.position, Quaternion.identity);
            tickTime = 0f;
        }

        public override void expire(bool onDeath)
        {
            Object.Destroy(effect);
        }

        protected override void update(float secs)
        {
            effect.transform.position = manager.transform.position + Vector3.up * 0.5f;

            tickTime += secs;
            if (tickTime < skill.tickStep) return;

            tickTime -= skill.tickStep;
            manager.GetComponent<HealthManager>().harm(skill.totalDamages / ticksCount);
        }

    }

}
