using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LIL
{

    public class WhirlwindSkill : ISkillModel
    {
        public float maxDamages;
        public float range;
        public float duration;
        public float slowRatio;
        public float impactPeriod;
        public float speedRotation;
        public GameObject castEffect;
        public GameObject impactEffect;
        public AudioClip castSound;
        
        public override void cast(SkillManager manager)
        {
            var caster = manager.gameObject;

            if (castSound != null)
            {
                var audioSource = caster.GetComponent<AudioSource>();
                audioSource.PlayOneShot(castSound, 0.3f);
            }

            var cast = Instantiate(castEffect, caster.transform.position, Quaternion.identity);
            Destroy(cast, duration);
            
            caster.GetComponent<EffectManager>().addEffect(new Effects.Slow(duration, slowRatio));
            caster.GetComponent<EffectManager>().addEffect(new Effects.Silence(duration));
            caster.GetComponent<EffectManager>().addEffect(new Effects.Whirlwind(this));
        }
    }

}
