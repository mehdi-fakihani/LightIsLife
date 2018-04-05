using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LIL.Effects
{

    public class Poison : IEffect
    {
        private readonly float time;
        private readonly float totalDamages;
        private readonly GameObject effectModel;
        private GameObject effect;

        public Poison(float time, float totalDamages, GameObject effectModel)
        {
            this.time = time;
            this.totalDamages = totalDamages;
            this.effectModel = effectModel;
        }

        protected override float duration() { return time; }

        protected override void apply()
        {
            effect = Object.Instantiate(effectModel, manager.transform.position, Quaternion.identity);
        }

        public override void expire(bool onDeath)
        {
            Object.Destroy(effect);
        }

        protected override void update(float secs)
        {
            manager.GetComponent<HealthManager>().harm(totalDamages * secs / time);
            effect.transform.position = manager.transform.position + Vector3.up * 0.5f;
        }

    }

}
