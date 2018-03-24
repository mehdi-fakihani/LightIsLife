using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LIL.Effects
{
    /// <summary>
    /// Acts as a non-cumulable slow (which duration is reset on multiple impacts).
    /// </summary>
    public class IcyBlastImpact : IEffect
    {
        private readonly float time;
        private readonly float slowRatio;
        private readonly GameObject slowPrefab;
        private MovementManager movement;
        private GameObject slowEffect;
        
        public IcyBlastImpact(float time, float slowRatio, GameObject slowPrefab)
        {
            this.time = time;
            this.slowRatio = slowRatio;
            this.slowPrefab = slowPrefab;
        }

        protected override float duration() { return time; }

        protected override void update(float secs)
        {
            slowEffect.transform.position = movement.transform.position + new Vector3(0f, 0.1f, 0f);
        }

        protected override void apply()
        {
            movement = manager.gameObject.GetComponent<MovementManager>();
            movement.beginSlow(slowRatio);
            slowEffect = Object.Instantiate(slowPrefab, manager.transform.position, slowPrefab.transform.rotation);
        }

        public override void expire(bool onDeath)
        {
            movement.endSlow(slowRatio);
            Object.Destroy(slowEffect);
        }
    }
}
