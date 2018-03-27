using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LIL.Effects
{

    public class Whirlwind : IEffect
    {
        private readonly WhirlwindSkill skill;
        private readonly float impactDamages;
        private float currentTime;
        private GameObject castEffect;

        public Whirlwind(WhirlwindSkill skill)
        {
            this.skill = skill;
            float occurences = skill.duration / skill.impactPeriod;
            impactDamages = skill.maxDamages / occurences;
        }

        protected override float duration()
        {
            return skill.duration;
        }

        protected override void apply()
        {
            currentTime = 0f;
            castEffect = Object.Instantiate(
                skill.impactEffect,
                manager.transform.position,
                skill.impactEffect.transform.rotation);
        }

        protected override void update(float secs)
        {
            currentTime += secs;
            castEffect.transform.position = manager.transform.position;

            manager.transform.Rotate(Vector3.up, secs * skill.speedRotation);

            if (currentTime < skill.impactPeriod) return;
            currentTime -= skill.impactPeriod;

            var hits = Physics.OverlapSphere(manager.transform.position, skill.range);
            foreach (var hit in hits)
            {
                if (!manager.gameObject.isEnemyWith(hit.gameObject)) continue;
                var health = hit.GetComponent<HealthManager>();
                if (!health) continue;
                health.harm(impactDamages);
                var impact = Object.Instantiate(skill.impactEffect, hit.transform.position, Quaternion.identity);
                Object.Destroy(impact, 1f);
            }
        }

        public override void expire(bool onDeath)
        {
            Object.Destroy(castEffect);
        }
    }

}
