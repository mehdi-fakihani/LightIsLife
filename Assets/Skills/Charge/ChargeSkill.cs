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
        public float damages;
        public float stunTime;
        public float postInvulnerabilityTime;
        public float speed;
        public float range;
        public float castTime;
        public AudioClip castSound;


        public override void cast(SkillManager manager)
        {
            var caster = manager.gameObject;
            var casterAnimator = caster.GetComponent<Animator>();

            if (castSound != null)
            {
                var audioSource = caster.GetComponent<AudioSource>();
                audioSource.PlayOneShot(castSound);
            }

            // Play the attacks' animation
            casterAnimator.SetTrigger("charge");

            caster.GetComponent<MovementManager>().beginImmobilization();
            caster.GetComponent<SkillManager   >().beginSilence();
            caster.GetComponent<HealthManager  >().beginInvulnerability();

            caster.GetComponent<EffectManager>().addEffect(new Effects.Delayed(castTime, true, () =>
            {
                var charge = caster.AddComponent<Charge>();
                charge.setup(damages, stunTime, speed, range, postInvulnerabilityTime);
            }));
        }
    }
}
