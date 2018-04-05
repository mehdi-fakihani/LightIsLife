using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using LIL.Inputs;

//------------------------------------------------------------------------
//
//  Name:   Fireball.cs
//
//  Desc:   This script cheks if the fireball gameobject has entered in collision with an other object of the scene.
//              - Collision with the enemy -> apply the dammage system on the enemy + destroy the fireball object
//              - Collision with an object other than enemy -> destroy the fireball object
//
//  Attachment : This script is used in the fireball prefab object
//
//  Creation :  01/02/2018
//
//  Last modification : Sidney - 22/02/2018
//
//------------------------------------------------------------------------

namespace LIL
{

    public class Sword : MonoBehaviour
    {

        // Private :
        private GameObject player;               // Sword GameObject
        private int damageAttack;               // This var is for the damage that will be caused by the fireball
        Skill swordSkill;
        private Profile profile;
        private bool attack = false;
        private float attackTimeInterval = 0.3f;
        private float currentAttackTime;
        private bool canAttack;
        private Animator playerAnimator;        // Players' Animator
        private int playerNum;

        void Start()
        {
            // ----------------- /!\ THE GENERAL DATA SCRIPT MUST BE CREATED /!\ -----------------
            //(It contains all the info for the skills and is responsable for the save and load)

            // Get the damage caused by the fireball from the GeneralData script
            player = this.transform.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.gameObject;
            playerNum = player.GetComponent<PlayerController>().getPlayerNum();
            damageAttack = GeneralData.GetSkillByName("Sword", playerNum).damage;
            attackTimeInterval = GeneralData.GetSkillByName("Sword", playerNum).cooldown;
            swordSkill = player.GetComponent<SkillManager>().getSkill(SkillsID.HeroAttack);
            profile = new Profile(playerNum,0);
            currentAttackTime = 0;
            canAttack = false;
            playerAnimator = this.GetComponentInParent<Animator>();
        }

        void Update()
        {
            if (currentAttackTime > 0) currentAttackTime -= Time.deltaTime;
            if (profile.getKeyDown(PlayerAction.Attack) && currentAttackTime <= 0)
            {
                attack = true;
                currentAttackTime = attackTimeInterval;
            }

            if(attack)
            {
                canAttack = true;
                //swordSkill.tryCast();
                attack = false;
            }
        }


        public void OnTriggerStay(Collider other)
        {
            var enemy = other.gameObject;
            
            // We test canAttack to prevent the Enemy being hit more than once in a single attack
            if (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Sword_Attack") && player.isEnemyWith(enemy) && canAttack)
            {
                Debug.Log("OK");
                canAttack = false;
                var health = enemy.GetComponent<HealthManager>();
                // Cause damage to the enemy
                health.harm(damageAttack);
                
                // Try applying the poison
                var poisonBuff = player.GetComponent<EffectManager>().getEffects()
                    .Where(e => e is Effects.PoisonBuff)
                    .FirstOrDefault(null) as Effects.PoisonBuff;

                if (poisonBuff == null) return;

                var effects = enemy.GetComponent<EffectManager>();
                var poisonEffect = effects.getEffects()
                    .Where(e => e is Effects.Poison)
                    .FirstOrDefault(null) as Effects.Poison;

                if (poisonEffect == null)
                    effects.addEffect(poisonBuff.makeEffect());
                else
                    poisonEffect.setTime(poisonBuff.effectTime);
            }

        }

    }
}
