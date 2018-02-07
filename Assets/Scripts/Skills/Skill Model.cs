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
        [SerializeField] public string id;
        [SerializeField] public float cooldown;
        [SerializeField] public int chargesCount;

        public abstract void cast(SkillManager manager);
        
        void Awake()
        {
            Assert.IsFalse(Skill.Models.ContainsKey(id), "Skill key '" + id + "' is duplicated");
            Skill.Models.Add(id, this);
        }
    }
}
