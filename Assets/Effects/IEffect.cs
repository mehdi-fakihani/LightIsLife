using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LIL
{
    /// <summary>
    /// A base class used to implement effects. For this, 4 functions need to be implemented :
    /// duration, apply, actualize and timeout.
    /// Effects are then used with : actor.addEffect(new MyEffect());
    /// </summary>
    public abstract class IEffect
    {
        private float remainingTime;

        protected Actor target;
        
        /// <summary>
        /// Indicates the duration of the effect.
        /// </summary>
        /// <returns></returns>
        protected abstract float duration();

        /// <summary>
        /// Called when the effect is added on an actor.
        /// </summary>
        protected abstract void apply();

        /// <summary>
        /// Called each frame until the effect expires.
        /// </summary>
        /// <param name="secs"></param>
        protected abstract void actualize(float secs);

        /// <summary>
        /// Called when the effect expires.
        /// </summary>
        protected abstract void timeout();

        /// <summary>
        /// Used by the actors to actualize their effects.
        /// </summary>
        /// <param name="secs"></param>
        public void update(float secs)
        {
            remainingTime -= secs;
            actualize(secs);
        }

        /// <summary>
        /// Inidcates if the effect has expired.
        /// </summary>
        /// <returns></returns>
        public bool isFinished()
        {
            return remainingTime <= 0f;
        }
        
        /// <summary>
        /// Used by the actors when an effect is added to them.
        /// </summary>
        /// <param name="target"></param>
        public void initialize(Actor target)
        {
            this.target = target;
            remainingTime = duration();
            apply();
        }

        /// <summary>
        /// Used by the actors when the effect is finished.
        /// </summary>
        public void destroy()
        {
            timeout();
        }
    }
}
