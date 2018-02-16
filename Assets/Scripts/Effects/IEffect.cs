using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*

    How to create an effect :

        Create a class with inerits from IEffect. You must then implement :
            - float duration();
            - void apply();
            - void update(float secs);
            - void expires(bool onDeath);
    
    How to use an effect :

        // Get the effect manager
        var manager = target.GetComponent<EffectManager>();

        // Creates the effect
        var effect = new MyEffect(...);

        // Add it to the manager
        manager.addEffect(effect);
        
*/

namespace LIL
{
    /// <summary>
    /// Affect objects with an EffectManager.
    /// Usage : effectManager.addEffect(new MyEffect());
    /// A new effect class must implement four functions :
    /// duration, apply, update and expire.
    /// </summary>
    public abstract class IEffect
    {
        private float remainingTime;

        /// <summary>
        /// Used to access the manager (and so the game object) from the effect.
        /// </summary>
        public EffectManager manager;

        /// <summary>
        /// Returns the duration of the effect in seconds.
        /// </summary>
        /// <returns></returns>
        protected abstract float duration();

        /// <summary>
        /// Called when the effect is assigned to the object.
        /// </summary>
        protected abstract void apply();

        /// <summary>
        /// Called each frame until the effect expires.
        /// </summary>
        protected abstract void update(float secs);

        /// <summary>
        /// Called at the end of the effect duration.
        /// The parameters indicates if the effect ends due to the target's death.
        /// </summary>
        public abstract void expire(bool onDeath);

        /// <summary>
        /// Returns true if the effect has expired.
        /// </summary>
        public bool isFinished()
        {
            return remainingTime <= 0f;
        }

        /// <summary>
        /// Used by EffectManager.
        /// </summary>
        public void actualize(float secs)
        {
            update(secs);
            remainingTime -= secs;
        }

        /// <summary>
        /// Set the remaining time of the effect.
        /// </summary>
        /// <param name="secs"></param>
        public void setTime(float secs)
        {
            remainingTime = secs;
        }

        /// <summary>
        /// Used by EffectManager.
        /// </summary>
        public void initialize(EffectManager manager)
        {
            this.manager = manager;
            remainingTime = duration();
            apply();
        }
    }
}
