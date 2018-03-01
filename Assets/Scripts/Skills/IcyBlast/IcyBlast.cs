using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace LIL
{
    /// <summary>
    /// Missile script for the icy blast skill.
    /// Harm, and apply and actualize the slow effect on enemies touched.
    /// </summary>
    public class IcyBlast : MonoBehaviour
    {
        private GameObject caster;
        private int damages;
        private float slowTime;
        private float slowRatio;

        public void setup(GameObject caster, int damages, float slowTime, float slowRatio)
        {
            this.caster = caster;
            this.damages = damages;
            this.slowTime = slowTime;
            this.slowRatio = slowRatio;
        }
        
        void OnCollisionEnter(Collision other)
        {
            var entity = other.gameObject;

            if (entity.GetComponent<IcyBlast>() != null) return;

            if (entity.isEnemyWith(caster))
            {
                var health = entity.GetComponent<HealthManager>();
                var effects = entity.GetComponent<EffectManager>();
                
                health.harm(damages);
                var effect = effects.getEffect<Effects.IcyBlastImpact>();
                if (effect == null)
                {
                    effects.addEffect(new Effects.IcyBlastImpact(slowTime, slowRatio));
                }
                else
                {
                    effect.setTime(slowTime);
                }
            }
            
            Destroy(gameObject);
        }

    }
}