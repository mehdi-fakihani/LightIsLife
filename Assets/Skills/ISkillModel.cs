using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LIL
{
    /// <summary>
    /// A base class used to implement skills. For this, the cast method need to be implemented and
    /// the members cooldown and chargesCount need to be set.
    /// </summary>
    public abstract class ISkillModel : MonoBehaviour
    {
        [SerializeField] public SkillsID id;
        [SerializeField] public float cooldown = 0f;
        [SerializeField] public int chargesCount = 1;

        /// <summary>
        /// Used by skills to cast spells. You must use Skill.tryCast instead of this method.
        /// </summary>
        /// <param name="owner"></param>
        public abstract void cast(Actor owner);
        
        /// <summary>
        /// Used by actors at their creation to get their skills from the skill models.
        /// </summary>
        /// <param name="owner"></param>
        /// <returns></returns>
        public Skill create(Actor owner)
        {
            return new Skill(this, owner);
        }

        /// <summary>
        /// The model registers himself at the start.
        /// </summary>
        void Awake()
        {
            Skill.Models.Add(id, this);
        }
    }
}
