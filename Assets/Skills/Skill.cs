using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LIL
{
    /// <summary>
    /// Class used to make an actor uses skills (with tryCast).
    /// It is created automatically when actors are instanciated from their models.
    /// </summary>
    public class Skill
    {
        public static readonly Dictionary<SkillsID, ISkillModel> Models
            = new Dictionary<SkillsID, ISkillModel>();

        /// <summary>
        /// Used by actors to create their spell when instanciated.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="owner"></param>
        public Skill(ISkillModel model, Actor owner)
        {
            this.model = model;
            this.owner = owner;
            remainingCD = 0f;
        }

        /// <summary>
        /// Used by actors to actualize their spells.
        /// </summary>
        /// <param name="secs"></param>
        public void update(float secs)
        {
            remainingCD -= secs;
            if (remainingCD < 0f) remainingCD = 0f;
        }

        /// <summary>
        /// Indicates if the actor can cast this spell.
        /// </summary>
        /// <returns></returns>
        public bool canBeCasted()
        {
            float threshold = model.cooldown * (model.chargesCount - 1);
            return owner.canCast() && remainingCD <= threshold;
        }

        /// <summary>
        /// Try to cast the skill, ten returns true on success.
        /// </summary>
        /// <returns></returns>
        public bool tryCast()
        {
            if (!canBeCasted()) return false;
            remainingCD += model.cooldown;
            model.cast(owner);
            return true;
        }

        /// <summary>
        /// Returns the skill id.
        /// </summary>
        /// <returns></returns>
        public SkillsID id()
        {
            return model.id;
        }
        
        private readonly ISkillModel model;
        private readonly Actor owner;
        private float remainingCD;
    }
}
