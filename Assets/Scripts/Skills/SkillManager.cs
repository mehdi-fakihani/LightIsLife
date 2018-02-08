using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using JetBrains.Annotations;
using NUnit.Framework;
using UnityEngine;

namespace LIL
{
    /// <summary>
    /// Component which handles the skills of a game object.
    /// They can be created or retrieved with their id, set in skill models.
    /// </summary>
    public class SkillManager : MonoBehaviour
    {
        [SerializeField] private SkillsID[] skillIds = new SkillsID[0];

        private readonly Dictionary<SkillsID, Skill> skills
            = new Dictionary<SkillsID, Skill>();

        private int silenceTokens = 0;

        /// <summary>
        /// Lists all skills stored by the manager.
        /// </summary>
        public IEnumerable<Skill> getSkills()
        {
            return skills.Values;
        }
        
        /// <summary>
        /// Returns the skill corresponding to the id, or null if it is not found.
        /// </summary>
        [CanBeNull] public Skill getSkill(SkillsID skillId)
        {
            Skill skill;
            skills.TryGetValue(skillId, out skill);
            return skill;
        }

        /// <summary>
        /// Creates then add a skill of the id specified to the manager, then returns it.
        /// </summary>
        public Skill addSkill(SkillsID skillId)
        {
            ISkillModel model;
            bool hasId = Skill.Models.TryGetValue(skillId, out model);
            Assert.IsTrue(hasId, "The skill key '" + skillId + "' does not exists");
            Assert.IsNull(getSkill(skillId), "The skill is already stored by this manager");

            var skill = new Skill(model, this);
            skills.Add(skillId, skill);
            return skill;
        }

        /// <summary>
        /// Removes the given skill.
        /// </summary>
        /// <param name="skillId"></param>
        public void removeSkill(Skill skill)
        {
            skills.Remove(skill.model.id);
        }
        /// <summary>
        /// Removes the skill with the given id.
        /// </summary>
        /// <param name="skillId"></param>
        public void removeSkill(SkillsID skillId)
        {
            skills.Remove(skillId);
        }

        /// <summary>
        /// Silences the object until endSilence() is called on it.
        /// </summary>
        public void beginSilence()
        {
            ++silenceTokens;
        }

        /// <summary>
        /// Let the object cast again. It must be called after beginSilence().
        /// </summary>
        public void endSilence()
        {
            --silenceTokens;
        }

        /// <summary>
        /// Indicates if the object can cast a spell, or if it is silenced.
        /// </summary>
        /// <returns></returns>
        public bool canCast()
        {
            return silenceTokens == 0;
        }

        void Start()
        {
            foreach (var id in skillIds)
            {
                addSkill(id);
            }
        }
        
        void Update()
        {
            float secs = Time.deltaTime;

            foreach (var skill in skills.Values)
            {
                skill.update(secs);
            }
        }
    }
}
