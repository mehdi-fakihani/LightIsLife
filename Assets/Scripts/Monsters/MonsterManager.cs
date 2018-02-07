using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LIL
{
    [RequireComponent(typeof(SkillManager))]
    public class MonsterManager : MonoBehaviour
    {
        [SerializeField]
        private string[] skillIds = new string[0];

        private readonly List<Skill> skills = new List<Skill>();

        private static readonly HashSet<MonsterManager> monsters
            = new HashSet<MonsterManager>();

        // Use this for initialization
        void Start()
        {
            monsters.Add(this);
            var skillManager = GetComponent<SkillManager>();
            foreach (string id in skillIds)
            {
                skills.Add(skillManager.addSkill(id));
            }
            skills.Sort((s1, s2) => s1.model.cooldown.CompareTo(s2.model.cooldown));
        }

        /// <summary>
        /// Must be called at the monster's death.
        /// </summary>
        public void onDeath()
        {
            monsters.Remove(this);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
