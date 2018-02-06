using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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

namespace LIL.Skills
{

    public class Fireball : MonoBehaviour {

        // Private :
        private GameObject fireball;        // Fireballs' GameObject
        private int damageAttack;           // This var is for the damage that will be caused by the fireball
        private GameObject enemy;           // Enemys' GameObject
        private EffectManager effects;      // Enemys' EffectManager
        private Health enemyHealth;         // Enemys' Health System

        void Start()
        {
            // ----------------- /!\ THE GENERAL DATA SCRIPT MUST BE CREATED /!\ -----------------
            //(It contains all the info for the skills and is responsable for the save and load)

            // Get the damage caused by the fireball from the GeneralData script
            //damageAttack = GeneralData.GetCurrentWeapon().damage;
        }


        public void OnCollisionEnter(Collision other)
        {
            // Checks if the fireball has hit an enemy
            if (other.gameObject.CompareTag("Enemy"))
            {
                // Initialization :

                enemy = other.gameObject;
                effects = enemy.GetComponent<EffectManager>();
                enemyHealth = enemy.GetComponent<Health>();

                // Cause damage to the enemy
                enemyHealth.takeDammage(damageAttack);

                // Apply effects of the fireball on the enemy
                //effects.addEffect(new Effects.Silence(GeneralData.getEffect("Silence")));


            }

            // The fireball is destroyed the collision 
            Destroy(gameObject);
        }

    }
}