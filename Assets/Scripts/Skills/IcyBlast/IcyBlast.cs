using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace LIL
{

    public class IcyBlast : MonoBehaviour
    {
        private int damages;
        private float slowTime;
        private float slowRatio;

        public void setup(int damages, float slowTime, float slowRatio)
        {
            this.damages = damages;
            this.slowTime = slowTime;
            this.slowRatio = slowRatio;
        }
        
        void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.GetComponent<IcyBlast>() != null) return;

            if (other.gameObject.CompareTag("Enemy"))
            {
                var enemy = other.gameObject;
                var health = enemy.GetComponent<HealthEnemy>();
                var effects = enemy.GetComponent<EffectManager>();
                
                health.takeDammage(damages);
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