using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LIL.Sandbox
{
    public class SimplePlayerController : IActorController
    {
        private readonly Inputs.Profile profile;
        private Actor hero;
        private Skill baseAttack;
        private Skill fireball;

        public SimplePlayerController(Inputs.Profile profile)
        {
            this.profile = profile;
        }

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

            if (profile.getKey(PlayerAction.Up))
                transform.position += Vector3.forward * shift;
            
            if (profile.getKey(PlayerAction.Down))
                transform.position -= Vector3.forward * shift;
            
            if (profile.getKey(PlayerAction.Left))
                transform.position += Vector3.left * shift;
            
            if (profile.getKey(PlayerAction.Right))
                transform.position -= Vector3.left * shift;
        }

        private void checkSkillInputs()
        {
            if (profile.getKeyDown(PlayerAction.Attack))
                baseAttack.tryCast();
            
            if (profile.getKeyDown(PlayerAction.Skill1))
                fireball.tryCast();
        }
    }
}
