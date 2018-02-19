using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LIL
{
    public class Charge : MonoBehaviour
    {
        private int damages;
        private float stunTime;
        private float speed;
        private float timeMax;
        private float timeElasped;

        public void setup(int damages, float stunTime, float speed, float range)
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
            var health = enemy.GetComponent<HealthEnemy>();
            if (health)
            {
                health.takeDammage(damages);
            }

            var effects = enemy.GetComponent<EffectManager>();
            if (effects)
            {
                Debug.Log("stunned");
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
            if (collision.gameObject.CompareTag("Enemy"))
                impact(collision.gameObject);

            finish();
        }
    }
}
