using System;
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
//  Last modification : Sidney - 22/02/2018
//
//------------------------------------------------------------------------

namespace LIL
{

    public class Fireball : MonoBehaviour {

        // Private :
        [NonSerialized] public float damages;              // This var is for the damage that will be caused by the fireball
        [NonSerialized] public GameObject caster;          // Game object which casted the fireball

        void Start()
        {
            // ----------------- /!\ THE GENERAL DATA SCRIPT MUST BE CREATED /!\ -----------------
            //(It contains all the info for the skills and is responsable for the save and load)

            // Get the damage caused by the fireball from the GeneralData script
            // ----> Commented by Sidney (value set by the spell model in setup) <------
            // damageAttack = GeneralData.GetSkillByName("Fireball").damage;
        }
        
        public void setup(GameObject caster, float damages)
        {
            this.caster = caster;
            this.damages = damages;
        }


        void OnCollisionEnter(Collision other)
        {
            var entity = other.gameObject;

            // Checks if the fireball has hit an enemy
            if (entity.isEnemyWith(caster))
            {
                //var effects = entity.GetComponent<EffectManager>();
                var health = entity.GetComponent<HealthManager>();

                // Cause damage to the enemy
                health.harm(damages);

                // Apply effects of the fireball on the enemy
                // effects.addEffect(new Effects.Silence(GeneralData.GetEffectByName("Silence").effect));
                // Commented by Sidney (why the fireball silences the enemy ?)
            }

            // The fireball is destroyed the collision

            Destroy(gameObject);
        }

    }
}