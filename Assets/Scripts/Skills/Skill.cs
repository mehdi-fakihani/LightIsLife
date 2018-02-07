using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        public static readonly Dictionary<string, ISkillModel> Models
            = new Dictionary<string, ISkillModel>();

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
