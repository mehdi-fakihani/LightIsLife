using System.Collections;
using System.Collections.Generic;
using LIL.Inputs;
using UnityEngine;

namespace LIL
{
    public class HeroController : MonoBehaviour
    {
        private Profile profile;
        private Skill fireball;
        
        void Start()
        {
            profile = new Profile(ProfilsID.XBoxGamepad, 0);
            var skillManager = GetComponent<SkillManager>();
            fireball = skillManager.addSkill("fireball");
        }
        
        void Update()
        {
            var trans = gameObject.transform;
            
            if (profile.getKeyDown(PlayerAction.Attack))
            {
                bool success = fireball.tryCast();
                Debug.Log("cast success ? " + success);
            }
        }
    }
}
