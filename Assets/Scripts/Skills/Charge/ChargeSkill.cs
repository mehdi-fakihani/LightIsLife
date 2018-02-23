using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LIL
{
    /// <summary>
    /// Model for the charge skill.
    /// It makes the caster run in a straight line, causing damage and stunning the first enemy
    /// encoutered.
    /// </summary>
    public class ChargeSkill : ISkillModel
    {
        [SerializeField] private float damages;
        [SerializeField] private float stunTime;
        [SerializeField] private float speed;
        [SerializeField] private float range;
        [SerializeField] private float castTime;
        [SerializeField] private AudioClip castSound;

        public override void cast(SkillManager manager)
        {
            var caster = manager.gameObject;

            if (castSound != null)
            {
                var audioSource = caster.GetComponent<AudioSource>();
                audioSource.PlayOneShot(castSound, 0.3f);
            }

            caster.GetComponent<MovementManager>().beginImmobilization();
            caster.GetComponent<SkillManager>().beginSilence();

            caster.GetComponent<EffectManager>().addEffect(new Effects.Delayed(castTime, () =>
            {
                var charge = caster.AddComponent<Charge>();
                charge.setup(damages, stunTime, speed, range);
            }));
        }
    }
}
