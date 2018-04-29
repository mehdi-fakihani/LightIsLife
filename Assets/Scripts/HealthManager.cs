﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LIL
{
    /// <summary>
    /// Used to manage an entity's life.
    /// It handles protection ratios, invulnerability and indicates it's death.
    /// </summary>
    public class HealthManager : MonoBehaviour
    {
        [SerializeField] private float initialLife = 100f;
        [SerializeField] private GameObject expCollectable;

        private float life;
        private float protectionRatio = 1f;
        private int invulnerableTokens = 0;
        private bool alive = true;
        private Action hurtCallback  = () => { };
        private Action deathCallback = () => { };
        public GameObject damagePopUp;
        int playerNum;

        /// <summary>
        /// Set the function called each time the entity takes damages.
        /// </summary>
        public void setHurtCallback(Action callback)
        {
            hurtCallback = callback;
        }

        /// <summary>
        /// Set the function called once when the entity dies.
        /// </summary>
        public void setDeathCallback(Action callback)
        {
            deathCallback = callback;
        }

        /// <summary>
        /// Returns the initial entity's life.
        /// </summary>
        public float getInitialLife()
        {
            return initialLife;
        }

        /// <summary>
        /// Returns the current entity's life.
        /// </summary>
        public float getLife()
        {
            return life;
        }

        /// <summary>
        /// Indicates if the entity is alive.
        /// </summary>
        public bool isAlive()
        {
            return alive;
        }

        /// <summary>
        /// Set the entity's life to zero and makes it dead, even if she is invulnerable.
        /// </summary>
        public void kill()
        {
            alive = false;
            life = 0f;
            var effects = GetComponent<EffectManager>();
            if (effects) effects.onDeath();
            deathCallback.Invoke();
            if(CompareTag("Enemy"))
            {
                Instantiate(expCollectable, transform.position + new Vector3(0, 0.5f, 0), transform.rotation);
            }
        }
        /// <summary>
        /// Inflicts damages to the entity according to it's protection, if she is not invulnerable.
        /// </summary>
        /// <param name="damages"></param>
        public void harm(float damages)
        {
            if (isInvulnerable() || !isAlive()) return;

            life -= damages / protectionRatio;

            if (gameObject.tag == "Player")
                GeneralData.SetCurrentLife(life, playerNum);

            damagePopUp.GetComponent<DamagePopUp>().SetTextDamage((int) (damages / protectionRatio));

            if (life > 0f)
                hurtCallback.Invoke();
            else
                kill();
        }

        /// <summary>
        /// Modify the entity's protection ratio. It should be cancelled later with "endProtection()".
        /// Exemple : beginProtection(3f) will triple the entity's protection. 
        /// </summary>
        public void beginProtection(float ratio)
        {
            protectionRatio *= ratio;
        }

        /// <summary>
        /// Undoes the entity's protection modification. It must be called after "beginProtection()".
        /// </summary>
        public void endProtection(float ratio)
        {
            protectionRatio /= ratio;
        }
        
        /// <summary>
        /// Makes the entity invulnerable. It should be cancelled later with "endInvulnerability()".
        /// </summary>
        public void beginInvulnerability()
        {
            invulnerableTokens += 1;
        }

        /// <summary>
        /// Cancels the entity's invulnerability. It must be called after "beginInvulnerability()".
        /// </summary>
        public void endInvulnerability()
        {
            invulnerableTokens -= 1;
        }

        /// <summary>
        /// Indicates if the entity is invulnerable.
        /// </summary>
        public bool isInvulnerable()
        {
            return invulnerableTokens != 0;
        }
        void Awake()
        {
            if(gameObject.tag == "Player")
            {
                playerNum = gameObject.GetComponent<PlayerController>().getPlayerNum();
                initialLife = GeneralData.GetInitialLife(playerNum);

                life = GeneralData.GetCurrentLife(playerNum);

                StartCoroutine(RegenerateHealth());
            }
            else
            {
                life = initialLife;
            }
            
        }
        void Update()
        {
            if (gameObject.tag == "Player" && initialLife != GeneralData.GetInitialLife(playerNum))
            {
                initialLife = GeneralData.GetInitialLife(playerNum);
            }
            if (gameObject.tag == "Player" && life != GeneralData.GetCurrentLife(playerNum))
            {
                life = GeneralData.GetCurrentLife(playerNum);
            }
        }

        IEnumerator RegenerateHealth()
        {
            while (true)
            { // loops forever...
                if (life < initialLife/2)
                { // if current life < initialLife/2...
                    life += GeneralData.GetHealingRate(playerNum); // increase current life and wait the specified time
                    GeneralData.SetCurrentLife(life, playerNum);
                    Debug.Log("healing " + life + "/"+initialLife);
                    yield return new WaitForSeconds(1);
                }
                else
                { // if current life >= initialLife/2, just yield 
                    yield return null;
                }
            }
        }

    }
}
