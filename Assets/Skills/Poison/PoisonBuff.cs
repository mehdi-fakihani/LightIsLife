
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LIL.Effects
{

    public class PoisonBuff : IEffect
    {
        private readonly PoisonSkill skill;
        private GameObject cast;
        private GameObject buff;

        public PoisonBuff(PoisonSkill skill)
        {
            this.skill = skill;
        }

        public float buffDuration()
        {
            return skill.buffDuration;
        }

        public Poison makeEffect()
        {
            return new Poison(skill);
        }

        protected override float duration()
        {
            return skill.buffDuration;
        }

        protected override void apply()
        {
            cast = Object.Instantiate(skill.castEffect,   manager.transform.position, Quaternion.identity);
            buff = Object.Instantiate(skill.impactEffect, manager.transform.position, Quaternion.identity);
            buff.transform.Rotate(new Vector3(1, 0, 0), 90);
        }

        protected override void update(float secs)
        {
            var pos = manager.transform.position + Vector3.up * 0.2f;
            buff.transform.position = pos;
            if (cast) cast.transform.position = pos;
        }

        public override void expire(bool onDeath)
        {
            Object.Destroy(buff);
        }
    }

}
