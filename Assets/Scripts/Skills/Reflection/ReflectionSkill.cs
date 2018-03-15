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
    public class ReflectionSkill : ISkillModel
    {
        [SerializeField] private float postInvulnerabilityTime;
        [SerializeField] private float castTime;
        [SerializeField] private float timeReflect;
        [SerializeField] private AudioClip castSound;

        public override void cast(SkillManager manager)
        {
            var caster = manager.gameObject;

            if (castSound != null)
            {
                var audioSource = caster.GetComponent<AudioSource>();
                audioSource.PlayOneShot(castSound);
            }

            caster.GetComponent<SkillManager>().beginSilence();
            caster.GetComponent<HealthManager>().beginInvulnerability();

            caster.GetComponent<EffectManager>().addEffect(new Effects.Delayed(castTime, true, () =>
            {
                var reflection = caster.AddComponent<Reflection>();
                reflection.setup(timeReflect, postInvulnerabilityTime);
            }));
        }
    }
}
