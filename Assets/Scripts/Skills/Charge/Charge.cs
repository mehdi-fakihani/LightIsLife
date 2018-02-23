using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LIL
{
    /// <summary>
    /// The temporary component on the charge skill's user.
    /// It block and make him move on a line, and apply charge effects on the first enemy touched.
    /// </summary>
    public class Charge : MonoBehaviour
    {
        private float damages;
        private float stunTime;
        private float speed;
        private float timeMax;
        private float timeElasped;

        public void setup(float damages, float stunTime, float speed, float range)
        {
            this.damages = damages;
            this.stunTime = stunTime;
            this.speed = speed;
            timeMax = 100f * range / speed;
            timeElasped = 0f;
        }

        private void finish()
        {
            GetComponent<MovementManager>().endImmobilization();
            GetComponent<SkillManager   >().endSilence();
            Destroy(this);
        }

        private void impact(GameObject enemy)
        {
            var health = enemy.GetComponent<HealthManager>();
            if (health)
            {
                health.harm(damages);
            }

            var effects = enemy.GetComponent<EffectManager>();
            if (effects)
            {
                effects.addEffect(new Effects.Immobilize(stunTime));
                effects.addEffect(new Effects.Silence(stunTime));
            }
        }

        void Update()
        {
            float secs = Time.deltaTime;
            timeElasped += secs;
            if (timeElasped > timeMax)
            {
                finish();
                return;
            }

            gameObject.transform.position += gameObject.transform.forward * speed * secs / 100f;
        }

        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.isEnemyWith(gameObject))
                impact(collision.gameObject);

            finish();
        }
    }
}
