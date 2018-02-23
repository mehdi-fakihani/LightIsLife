using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*

    How to create a skill :

        1. If no skill model can be reused, create a class which inherits from ISkillModel.
        It must implements void cast(SkillManager).

        2. Instanciate the skill model as a component of a game object. The game object is not
        important, but it must not be destroyed during the game (to keep the skill model alive).

        3. Set the ID, the cooldown and the number of charges of the skill model.

        4. Add a skill manager to the skill's owner.

        5a. To set the initial skills of a game object :
            In the skill ID list of the new manager, add the ID set to the model.

        5b. To modify the skills of a game object in game :
            
            // Get the owner's skill manager
            var manager = owner.GetComponent<SkillManager>();
            
            // Add a skill
            Skill newSkill = manager.addSkill(SkillsID.NiceSkill);

            // Remove a skill
            manager.removeSkill(SkillsID.NiceSkill);

    How to use a skill :
        
            // Get the owner's skill manager
            var manager = owner.GetComponent<SkillManager>();
            
            // Get the skill
            Skill niceSkill = manager.getSkill(SkillsID.NiceSkill);

            // Try to cast the skill
            bool success = niceSkill.tryCast();
        
*/

namespace LIL {
    /// <summary>
    /// Instance of a skill model created like that :
    /// skillManager.create("myFireballId");
    /// </summary>
    public class Skill
    {
        /// <summary>
        /// Used by SkillManager.
        /// </summary>
        public static readonly Dictionary<SkillsID, ISkillModel> Models
            = new Dictionary<SkillsID, ISkillModel>();

        public readonly SkillManager manager;
        public readonly ISkillModel model;

        private float currentCD;

        /// <summary>
        /// Used by SkillManager.
        /// </summary>
        public void update(float secs)
        {
            currentCD -= secs;
            if (currentCD < 0f) currentCD = 0f;
        }

        /// <summary>
        /// Returns the number of charges available to cast the skill.
        /// </summary>
        public int chargesAvailable()
        {
            return (int) ((model.cooldown * model.chargesCount - currentCD) / model.cooldown);
        }

        /// <summary>
        /// Returns the current cooldown of the spell.
        /// </summary>
        /// <returns></returns>
        public float currentCooldown()
        {
            return (model.cooldown * model.chargesCount - currentCD) % model.cooldown;
        }

        /// <summary>
        /// Indicates if the skill can be casted.
        /// </summary>
        public bool canbeCasted()
        {
            return manager.canCast() && chargesAvailable() >= 1;
        }

        /// <summary>
        /// Try to use the skill. Returns true on success.
        /// </summary>
        public bool tryCast()
        {
            if (!canbeCasted()) return false;
            currentCD += model.cooldown;
            model.cast(manager);
            return true;
        }
        
        /// <summary>
        /// Returns the skill unique ID.
        /// </summary>
        public SkillsID id()
        {
            return model.id;
        }

        /// <summary>
        /// Used by SkillManager.
        /// </summary>
        public Skill(ISkillModel model, SkillManager manager)
        {
            this.model = model;
            this.manager = manager;
            currentCD = 0f;
        }
    }
}
