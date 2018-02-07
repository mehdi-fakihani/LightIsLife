using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LIL.Skills
{
    public class AttackModel : ISkillModel
    {
        [SerializeField] private float castTime = 0.5f;

        public override void cast(SkillManager manager)
        {
            var effects = manager.GetComponent<EffectManager>();
            effects.addEffect(new Effects.Silence(castTime));
        }
    }
}

namespace LIL.Skills
{
    public class MissileModel : ISkillModel
    {
        [SerializeField] private GameObject missile;
        [SerializeField] private float castTime = 0.5f;
        [SerializeField] private float speed = 10f;
        [SerializeField] private float range = 30f;

        public override void cast(SkillManager manager)
        {
            var effects = manager.GetComponent<EffectManager>();
            effects.addEffect(new Effects.Silence(castTime));
            effects.addEffect(new Effects.Delayed(castTime, () => {
                var transform = manager.gameObject.transform;

            }));
        }
    }
}
