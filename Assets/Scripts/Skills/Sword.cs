using System.Collections;
using System.Collections.Generic;
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
//  Last modification : Aub Ah - 01/02/2018
//
//------------------------------------------------------------------------

namespace LIL
{

    public class Sword : MonoBehaviour
    {

        // Private :
        private GameObject player;               // Sword GameObject
        private int damageAttack;               // This var is for the damage that will be caused by the fireball
        private GameObject enemy;               // Enemys' GameObject
        private EffectManager effects;          // Enemys' EffectManager
        private HealthEnemy enemyHealth;        // Enemys' Health System
        Skill swordSkill;
        public ProfilsID input;
        private Profile profile;
        private bool attack = false;
        public float attackTimeInterval = 0.3f;
        private float currentAttackTime;
        private bool canAttack;
        private Animator playerAnimator;        // Players' Animator

        void Start()
        {
            // ----------------- /!\ THE GENERAL DATA SCRIPT MUST BE CREATED /!\ -----------------
            //(It contains all the info for the skills and is responsable for the save and load)

            // Get the damage caused by the fireball from the GeneralData script
            damageAttack = GeneralData.GetSkillByName("Sword").damage;
            player = GameObject.FindGameObjectWithTag("Player");
            swordSkill = player.GetComponent<SkillManager>().getSkill(SkillsID.HeroAttack);
            profile = new Profile(input, 0);
            currentAttackTime = 0;
            canAttack = false;
            playerAnimator = this.GetComponentInParent<Animator>();
        }

        void Update()
        {
            if (currentAttackTime > 0) currentAttackTime -= Time.deltaTime;
            if (profile.getKeyDown(PlayerAction.Skill2) && currentAttackTime<=0)
            {
                Debug.Log("test");
                attack = true;
                currentAttackTime = attackTimeInterval;
            }

            if(attack)
            {
                canAttack = true;
                swordSkill.tryCast();
                attack = false;
            }
        }


        public void OnTriggerStay(Collider other)
        {
            // We test canAttack to prevent the Enemy being hit more than once in a single attack
            if (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Sword_Attack") && other.gameObject.CompareTag("Enemy") && canAttack)
            {
                canAttack = false;
                enemy = other.gameObject;
                effects = enemy.GetComponent<EffectManager>();
                enemyHealth = enemy.GetComponent<HealthEnemy>();
                // Cause damage to the enemy
                enemyHealth.takeDammage(damageAttack);      
                // Apply effects of the fireball on the enemy
                effects.addEffect(new Effects.Silence(GeneralData.GetEffectByName("Silence").effect));
            }

        }

    }
}