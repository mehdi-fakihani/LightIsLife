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
        public GameObject impactEffect;
        public AudioClip castSound;

        private AudioSource audioSource;
        private SoundManager soundManager = new SoundManager();
        private const float fadingTime = 1.0f;

        public override void cast(SkillManager manager)
        {
            var caster = manager.gameObject;
            var casterAnimator = caster.GetComponent<Animator>();

            if (castSound != null)
            {
                audioSource = caster.GetComponent<AudioSource>();
                audioSource.PlayOneShot(castSound, 0.3f);
                StartCoroutine(SoundFading());
            }

            // Play the attacks' animation
            casterAnimator.SetTrigger("charge");

            caster.GetComponent<MovementManager>().beginImmobilization();
            caster.GetComponent<SkillManager   >().beginSilence();
            caster.GetComponent<HealthManager  >().beginInvulnerability();

            caster.GetComponent<EffectManager>().addEffect(new Effects.Delayed(castTime, true, () =>
            {
                var charge = caster.AddComponent<Charge>();
                charge.setup(damages, stunTime, speed, range, postInvulnerabilityTime, impactEffect);
            }));
        }

        private IEnumerator SoundFading()
        {
            float duration = range / speed;
            //wait for the end of the sound
            yield return new WaitForSeconds(Mathf.Max(0, duration - fadingTime));
            //make the sound faid out
            StartCoroutine(soundManager.AudioFade(audioSource, fadingTime));
        }
    }
}
