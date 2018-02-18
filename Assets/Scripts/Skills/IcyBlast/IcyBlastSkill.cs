using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LIL
{
    /// <summary>
    /// Model for the icy blast skill.
    /// It launches many little missiles in front of the caster at short range.
    /// The missiles causes damages and slow the enemies being hit.
    /// </summary>
    public class IcyBlastSkill : ISkillModel
    {
        [SerializeField] private int damages;
        [SerializeField] private int missilesCount;
        [SerializeField] private float speed;
        [SerializeField] private float range;
        [SerializeField] private float castTime;
        [SerializeField] private float castSlow;
        [SerializeField] private float castAngle;
        [SerializeField] private float castDelayRatio;
        [SerializeField] private float impactTime;
        [SerializeField] private float impactSlow;
        [SerializeField] private GameObject prefab;
        [SerializeField] private AudioClip castSound;

        public override void cast(SkillManager manager)
        {
            StartCoroutine("casting", manager);
        }

        IEnumerator casting(SkillManager manager)
        {
            var player = manager.gameObject;
            var effects = player.GetComponent<EffectManager>();
            //var animator = player.GetComponent<Animator>();

            if (castSound != null)
            {
                var audioSource = player.GetComponent<AudioSource>();
                audioSource.PlayOneShot(castSound, 0.3f);
            }

            effects.addEffect(new Effects.Slow(castTime, castSlow));
            effects.addEffect(new Effects.Silence(castTime));
            int count = missilesCount;

            yield return new WaitForSeconds(castTime * castDelayRatio);

            for (int i = 0; i < count; ++i)
            {
                float angle = castAngle * (i % 2 == 0 ? i : -i) / count;
                var orientation = (new Quaternion(0, angle, 0, 1) * player.transform.forward).normalized;

                var position = player.transform.position + orientation * 1.5f;
                position.y += 1;

                var blast = Instantiate(prefab, position, player.transform.rotation * new Quaternion(0, angle, 0, 1));
                blast.GetComponent<IcyBlast>().setup(damages, impactTime, impactSlow);
                blast.GetComponent<Rigidbody>().AddForce(orientation * speed);
                Destroy(blast, 100f * range / speed);
                
                yield return new WaitForSeconds(castTime * (1 - castDelayRatio) / missilesCount);
            }
        }
    }
}
