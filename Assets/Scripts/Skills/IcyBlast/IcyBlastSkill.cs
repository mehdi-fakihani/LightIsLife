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

            for (int i = 0; i < count; ++i)
            {
                float p = (count - (i % 2 == 0 ? i : -i)) / (float) count;

                var position = player.transform.position + player.transform.forward * 1.5f;
                position.y += 1;

                var forward = player.transform.forward;
                var right = player.transform.right;
                var orientation = missilesCount * (p * forward + (1 - p) * castAngle * right);
                orientation.Normalize();

                var blast = Instantiate(prefab, position, player.transform.rotation);
                blast.GetComponent<IcyBlast>().setup(damages, impactTime, impactSlow);
                blast.GetComponent<Rigidbody>().AddForce(orientation * speed);
                Destroy(blast, 100f * range / speed);
                
                yield return new WaitForSeconds(castTime / missilesCount);
            }
        }
    }
}
