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
//  Last modification : Aub Ah - 01/02/2018
//
//------------------------------------------------------------------------


namespace LIL.Skills
{
    public class FireballSkill : ISkillModel
    {
        // Public :
        public AudioClip fireball_Sound;        // The audio that must be played when the attack is launched
        public GameObject fireball_Prefab;      // The prefab that must be generated
        public float strength;                  // The strength of the ball firing
        public float range;                     // The range of the ball
        public int damageAttack;                // The fireball damage
        public float castTime;                  // Time of the cast
        public string animationToPlay;          // The name of the animation that we need to play

        // Private : 
        private Animator playerAnimator;        // The Animator of the player
        private AudioSource audioSource;        // The AudioSource of the player
        private GameObject player;              // Players' GameObject
        private GameObject fireball;            // fireballs' GameObject
        EffectManager effects;                  // Players' EffectManager

        public override void cast(SkillManager skillManager)
        {
            // Initialization :

            player = skillManager.gameObject;
            playerAnimator = player.GetComponent<Animator>();
            audioSource = player.GetComponent<AudioSource>();
            effects = skillManager.GetComponent<EffectManager>();


            // Adding the effects of the attack on the attacker : 

            //effects.addEffect(new Effects.Silence(castTime));
            /*effects.addEffect(new Effects.Delayed(castTime, () =>
            {
                // The fireballs' ejection pos
                Vector3 EjectPos = player.transform.position + player.transform.forward * 2;

                // Play the attacks' animation
                playerAnimator.SetTrigger(animationToPlay);

                // Play The attacks' sound
                audioSource.PlayOneShot(fireball_Sound, 0.3f);

                // Instantiating the fireball Gameobject
                fireball = Instantiate(fireball_Prefab, EjectPos, player.transform.rotation) as GameObject;

                // Appliquer une force 
                fireball.GetComponent<Rigidbody>().AddForce(player.transform.forward * strength);

                // Destroy the fireball
                Destroy(fireball, range);

            }));*/
        }
    }

}

