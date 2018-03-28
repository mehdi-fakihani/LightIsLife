using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LIL
{

    public class PoisonSkill : ISkillModel
    {
        [SerializeField] private float totalDamages;
        [SerializeField] private float effectDuration;
        [SerializeField] private GameObject impactEffect;
        [SerializeField] private GameObject castEffect;
        [SerializeField] private AudioClip castSound;

        public override void cast(SkillManager manager)
        {

        }
    }

}
