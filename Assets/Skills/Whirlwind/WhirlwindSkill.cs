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
        private AudioSource audioSource;
        private SoundManager soundManager = new SoundManager();
        private const float fadingTime = 1.0f;
        
        public override void cast(SkillManager manager)
        {
            var caster = manager.gameObject;

            if (castSound != null)
            {
                audioSource = caster.GetComponent<AudioSource>();
                audioSource.PlayOneShot(castSound, 0.3f);
                StartCoroutine(SoundFading());
            }
            
            caster.GetComponent<EffectManager>().addEffect(new Effects.Slow(duration, slowRatio));
            caster.GetComponent<EffectManager>().addEffect(new Effects.Silence(duration));
            caster.GetComponent<EffectManager>().addEffect(new Effects.Whirlwind(this));
        }

        private IEnumerator SoundFading()
        {
            //wait for the end of the sound
            yield return new WaitForSeconds(Mathf.Max(0, duration - fadingTime));
            //make the sound faid out
            StartCoroutine(soundManager.AudioFade(audioSource, fadingTime));
        }
    }

}
