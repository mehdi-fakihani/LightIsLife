using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LIL.Skills
{
    /// <summary>
    /// A skill which trigger a melee attack with an AoE. The code which trigger the AoE is not
    /// implemented yet.
    /// </summary>
    [System.Serializable]
    public class AttackSkill : ISkillModel
    {
        [SerializeField]
        private int damages = 10;
        [SerializeField]
        private float range = 1f;
        [SerializeField]
        private float castTime = .5f;
        [SerializeField]
        private float slowRatio = .75f;

        public override void cast(Actor owner)
        {
            owner.addEffect(new Effects.Slow(castTime, slowRatio));
            owner.addEffect(new Effects.Silence(castTime));
            owner.addEffect(new Effects.Delayed(castTime, () =>
            {
                Debug.Log("Imagine a melee attack");
            }));
        }
    }
}
