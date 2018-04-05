using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//------------------------------------------------------------------------
//
//  Name:   SwordSkill.cs
//
//  Desc:   Launch the sword attack, it can also be useful in playing the audio and the animation of the attack.
//          Some of the main params of this script are : sword_Sound (AudioClip), damageAttack (int), castTime (float)
//
//  Attachment : This script is added to the Skill object with all the other skills scripts
//
//  Creation :  01/02/2018
//
//  Last modification : Sidney - 22/02/2018
//
//------------------------------------------------------------------------


namespace LIL
{
    public class SwordSkill : ISkillModel
    {
        // Public :
        public AudioClip sword_Sound;        // The audio that must be played when the attack is launched
        public int damageAttack;                // The fireball damage
        public float castTime;                  // Time of the cast
        public float range;                     // Attack range
        public float width;                     // Size of the attack AoE

        public override void cast(SkillManager skillManager)
        {
            // Initialization :

            var caster = skillManager.gameObject;
            var trans  = caster.transform;
            var casterAnimator = caster.GetComponent<Animator>();
            var audioSource = caster.GetComponent<AudioSource>();
            var effects = skillManager.GetComponent<EffectManager>();
            
            // Adding the effects of the attack on the attacker : 
            
            effects.addEffect(new Effects.Silence(castTime));
            effects.addEffect(new Effects.Slow(castTime, 0.33f));
            casterAnimator.SetTrigger("sword");

            effects.addEffect(new Effects.Delayed(castTime / 2f, false, () => {
                audioSource.PlayOneShot(sword_Sound, 0.3f);

                var poisonBuff = caster.GetComponent<EffectManager>()
                        .getEffects()
                        .FirstOrDefault(e => e is Effects.PoisonBuff)
                        as Effects.PoisonBuff;

                var hits = Physics.OverlapSphere(trans.position + trans.forward * range + Vector3.up * 0.5f, width / 2f);
                var debug = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                Destroy(debug.GetComponent<Collider>());
                debug.transform.position = trans.position + trans.forward * range + Vector3.up;
                debug.transform.localScale = Vector3.one * width;
                Destroy(debug, 1f);
                foreach (var hit in hits)
                {
                    if (hit.isTrigger) continue;
                    if (!caster.isEnemyWith(hit.gameObject))
                    {
                        Debug.Log(hit.name + " is not an enemy");
                        continue;
                    }

                    var health = hit.GetComponent<HealthManager>();
                    if (!health) continue;

                    // Cause damage to the enemy
                    health.harm(damageAttack);

                    if (poisonBuff == null) continue;

                    // Try applying the poison

                    var enemyEffects = hit.GetComponent<EffectManager>();
                    var poisonEffect = enemyEffects
                        .getEffects()
                        .FirstOrDefault(e => e is Effects.Poison)
                        as Effects.Poison;

                    if (poisonEffect == null)
                        enemyEffects.addEffect(poisonBuff.makeEffect());
                    else
                        poisonEffect.setTime(poisonBuff.effectTime);
                }
            }));
        }
    }

}
