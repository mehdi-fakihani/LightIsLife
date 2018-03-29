using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LIL
{

    public class PoisonSkill : ISkillModel
    {
        [SerializeField] private float totalDamages;
        [SerializeField] private float effectDuration;
        [SerializeField] private float buffDuration;
        [SerializeField] private GameObject impactEffect;
        [SerializeField] private GameObject castEffect;
        [SerializeField] private AudioClip castSound;

        public override void cast(SkillManager manager)
        {
            var caster = manager.gameObject;

            if (castSound != null)
            {
                var audioSource = caster.GetComponent<AudioSource>();
                audioSource.PlayOneShot(castSound);
            }

            var effect = Instantiate(castEffect, manager.transform.position, Quaternion.identity);
            Destroy(effect, 2f);
            
            caster.GetComponent<EffectManager>().addEffect(
                new Effects.PoisonBuff(buffDuration, effectDuration, () =>
                new Effects.Poison(effectDuration, totalDamages, impactEffect)
            ));
        }
    }

}
