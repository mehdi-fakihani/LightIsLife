using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LIL
{
    /// <summary>
    /// The temporary component on the charge skill's user.
    /// It block and make him move on a line, and apply charge effects on the first enemy touched.
    /// </summary>
    public class Reflection : MonoBehaviour
    {
        private float timeMax;
        private float timeElapsed;
        private float invulnerabilityTime;

        public void setup(float timeReflect, float invulnerabilityTime)
        {
            this.invulnerabilityTime = invulnerabilityTime;
            timeMax = timeReflect;
            timeElapsed = 0f;
        }

        private void finish()
        {
            GetComponent<SkillManager>().endSilence();
            GetComponent<HealthManager>().endInvulnerability();

            Destroy(this);
        }

        void Update()
        {
            float secs = Time.deltaTime;
            timeElapsed += secs;
            if (timeElapsed > timeMax)
            {
                finish();
                return;
            }
        }

        void OnCollisionEnter(Collision collision)
        {
            var entity = collision.gameObject;

            if (entity.isMissile())
            {
                Debug.Log("collider with fire ball");
                //entity.GetComponent<Rigidbody>().AddForce(this.transform.position + this.transform.forward * 10);
            }
            else return;
        }
    }
}
