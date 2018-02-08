using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

namespace LIL
{
    /// <summary>
    /// A skill model class which must be inherited to implement the cast() method.
    /// Usage : skillManager.create("myFireballId");
    /// </summary>
    public abstract class ISkillModel : MonoBehaviour
    {
        [SerializeField] public SkillsID id;
        [SerializeField] public float cooldown;
        [SerializeField] public int chargesCount;

        /// <summary>
        /// The effect of te skill. The skill's owner is passed as parameter.
        /// </summary>
        public abstract void cast(SkillManager manager);
        
        void Awake()
        {
            if (cooldown <= 0f) cooldown = 0.01f;
            Assert.IsFalse(Skill.Models.ContainsKey(id), "Skill key '" + id + "' is duplicated");
            Skill.Models.Add(id, this);
        }
    }
}
