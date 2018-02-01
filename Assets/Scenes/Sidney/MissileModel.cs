using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LIL.Skills
{
    public class MissileModel : ISkillModel
    {
        [SerializeField] public float castTime;
        [SerializeField] public float speed;
        [SerializeField] public GameObject missile;
        
        public override void cast(SkillManager manager)
        {
            var effects = manager.GetComponent<EffectManager>();
            effects.addEffect(new Effects.Silence(castTime));
            effects.addEffect(new Effects.Delayed(castTime, () =>
            {
                var caster = manager.gameObject.transform;
                var missilePos = caster.position + caster.forward * 2;
                var obj = Instantiate(
                    missile,
                    missilePos,
                    caster.rotation);

                var body = obj.GetComponent<Rigidbody>();
                body.velocity = caster.forward * speed;
            }));
        }
    }
}
