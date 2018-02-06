using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LIL
{
    /// <summary>
    /// Component handling effects on a game object.
    /// Usage : effectManager.addEffect(new MyEffect());
    /// </summary>
    public class EffectManager : MonoBehaviour
    {
        private readonly List<IEffect> effects
            = new List<IEffect>();
        private readonly List<IEffect> finishedEffets
            = new List<IEffect>();
        
        /// <summary>
        /// Add a new effect to the game object.
        /// </summary>
        public void addEffect(IEffect effect)
        {
            effects.Add(effect);
            effect.initialize(this);
        }

        /// <summary>
        /// Clean all effects, which triggers expire effects.
        /// </summary>
        public void onDeath()
        {
            foreach (var effect in effects)
            {
                effect.expire(true);
            }
            effects.Clear();
        }
        
        void Update()
        {
            float secs = Time.deltaTime;

            foreach (var effect in effects)
            {
                effect.actualize(secs);
                if (effect.isFinished()) finishedEffets.Add(effect);
            }
            foreach (var effect in finishedEffets)
            {
                effects.Remove(effect);
                effect.expire(false);
            }
            finishedEffets.Clear();
        }
    }
}
