using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LIL.Sandbox
{
    public class SimplePlayerController : IActorController
    {
        private Actor hero;
        private Skill baseAttack;
        private Skill fireball;

        public void register(Actor actor)
        {
            hero = actor;
            // Get the skills
            foreach (var skill in actor.skills)
            {
                switch (skill.id())
                {
                    case SkillsID.HeroAttack:
                        baseAttack = skill;
                        break;
                    case SkillsID.Fireball:
                        fireball = skill;
                        break;
                    default:
                        throw new Exception("Wrong skill id found");
                }
            }
        }

        public void update(float secs)
        {
            checkMovementInputs(secs);
            checkSkillInputs();
        }

        private void checkMovementInputs(float secs)
        {
            if (!hero.canMove()) return;
            
            var transform = hero.gameObject.transform;
            float shift = hero.speed() * secs;

            if (Input.GetKey(KeyCode.Z))
            {
                transform.position += Vector3.forward * shift;
            }
            if (Input.GetKey(KeyCode.S))
            {
                transform.position -= Vector3.forward * shift;
            }
            if (Input.GetKey(KeyCode.Q))
            {
                transform.position += Vector3.left * shift;
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.position -= Vector3.left * shift;
            }
        }

        private void checkSkillInputs()
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                baseAttack.tryCast();
            }
            if (Input.GetKeyDown(KeyCode.Y))
            {
                fireball.tryCast();
            }
        }
    }
}
