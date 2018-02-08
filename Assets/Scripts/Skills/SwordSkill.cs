using System.Collections;
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
//  Last modification : Aub Ah - 01/02/2018
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

        // Private : 
        private Animator playerAnimator;        // The Animator of the player
        private AudioSource audioSource;        // The AudioSource of the player
        private GameObject player;              // Players' GameObject
        EffectManager effects;                  // Players' EffectManager

        public override void cast(SkillManager skillManager)
        {
            // Initialization :

            player = skillManager.gameObject;
            playerAnimator = player.GetComponent<Animator>();
            audioSource = player.GetComponent<AudioSource>();
            effects = skillManager.GetComponent<EffectManager>();


            // Adding the effects of the attack on the attacker : 

            effects.addEffect(new Effects.Delayed(castTime, () =>
            {
                // Play the attacks' animation
                playerAnimator.SetTrigger("sword");

                // Play The attacks' sound
                audioSource.PlayOneShot(sword_Sound, 0.3f);

            }));
        }
    }

}
