using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//------------------------------------------------------------------------
//
//  Name:   FireballSkill.cs
//
//  Desc:   Launch the fireball attack by creating an instance of the fireball prefab, it can also be useful in playing the audio 
//          and the animation of the attack.
//          Some of the main params of this script are : fireball_Sound (AudioClip), Fireball_Prefab (GameObject), strength (float), 
//                                                      range (float), damageAttack (int), castTime (float)
//
//  Attachment : This script is added to the Skill object with all the other skills scripts
//
//  Creation :  01/02/2018
//
//  Last modification : Sidney - 12/02/2018
//
//------------------------------------------------------------------------


namespace LIL
{
    public class FireballSkill : ISkillModel
    {
        // Public :
        public AudioClip fireball_Sound;        // The audio that must be played when the attack is launched
        public GameObject fireball_Prefab;      // The prefab that must be generated
        public GameObject fireball_Impact;      // The prefab impact created on the impact
        public float strength;                  // The strength of the ball firing
        public float range;                     // The range of the ball
        public float damageAttack;              // The fireball damage
        public float castTime;                  // Time of the cast
        public float ejection;                    // ejection factor
        
        public override void cast(SkillManager skillManager)
        {
            // Initialization :

            var caster = skillManager.gameObject;
            var casterAnimator = caster.GetComponent<Animator>();
            var audioSource = caster.GetComponent<AudioSource>();
            var effects = skillManager.GetComponent<EffectManager>();

            // Play The attacks' sound
            if (fireball_Sound != null)
            {
                audioSource.PlayOneShot(fireball_Sound, 0.3f);
            }

            // Play the attacks' animation
            casterAnimator.SetTrigger("fireball");

            // Adding the effects of the attack on the attacker : 
            effects.addEffect(new Effects.Immobilize(castTime));

            effects.addEffect(new Effects.Silence(castTime));
            effects.addEffect(new Effects.Delayed(castTime, false, () =>
            {
                // The fireballs' ejection pos
                Vector3 EjectPos = caster.transform.position + caster.transform.forward * ejection;
                EjectPos.y += 1;
                
                // Instantiating the fireball Gameobject with it's script
                var fireball = Instantiate(fireball_Prefab, EjectPos, caster.transform.rotation);
                var script = fireball.AddComponent<Fireball>();
                script.setup(caster, damageAttack, fireball_Impact);

                // Appliquer une force 
                fireball.GetComponent<Rigidbody>().AddForce(caster.transform.forward * strength);

                // Destroy the fireball
                Destroy(fireball, range);
            }));
        }
    }

}

