using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LIL
{

    public class IcyBlastSkill : ISkillModel
    {
        [SerializeField] private AudioClip castSound;
        [SerializeField] private AudioClip impactSound;
        [SerializeField] private int damages;
        [SerializeField] private float castTime;
        [SerializeField] private float castSlow;
        [SerializeField] private float impactTime;
        [SerializeField] private float impactSlow;

        public override void cast(SkillManager manager)
        {
            throw new System.NotImplementedException();
        }
    }
}
