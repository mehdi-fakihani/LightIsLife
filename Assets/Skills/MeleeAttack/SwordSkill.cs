﻿using System.Collections;
using System.Collections.Generic;
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
        
        public override void cast(SkillManager skillManager)
        {
            // Initialization :

            var caster = skillManager.gameObject;
            var casterAnimator = caster.GetComponent<Animator>();
            var audioSource = caster.GetComponent<AudioSource>();
            var effects = skillManager.GetComponent<EffectManager>();


            // Adding the effects of the attack on the attacker : 

            // Added by Sidney
            effects.addEffect(new Effects.Silence(castTime));
            effects.addEffect(new Effects.Slow(castTime, 0.33f));

            /* Modified by Sidney : Do not delay the attack (the animation time already delays
             * the attack).
             * 
            effects.addEffect(new Effects.Delayed(castTime, () =>*/
            
                // Play the attacks' animation
                casterAnimator.SetTrigger("sword");

                // Play The attacks' sound
                audioSource.PlayOneShot(sword_Sound, 0.3f);

            //));
        }
    }

}