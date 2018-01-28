using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LIL.Skills
{
    /// <summary>
    /// A skill launching a missile. The code creating the missile is not written yet.
    /// </summary>
    [System.Serializable]
    public class MissileSkill : ISkillModel
    {
        [SerializeField] private GameObject missile;
        [SerializeField] private float castTime = .7f;

        public override void cast(Actor owner)
        {
            owner.addEffect(new Effects.Immobilize(castTime));
            owner.addEffect(new Effects.Silence(castTime));
            owner.addEffect(new Effects.Delayed(castTime, () =>
            {
                Debug.Log("Imagine a missile being casted");
            }));
        }
    }
}
