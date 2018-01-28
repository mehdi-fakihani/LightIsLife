using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LIL
{
    /// <summary>
    /// The actor controller. It can represents player inputs or an AI and is responsible for
    /// triggering the actor's actions wen updated. 
    /// </summary>
    public interface IActorController
    {
        /// <summary>
        /// Called when the controller is bound to the new actor.
        /// </summary>
        /// <param name="actor"></param>
        void register(Actor actor);

        /// <summary>
        /// Called each frame with the time elapsed since last frame.
        /// </summary>
        /// <param name="actor"></param>
        /// <param name="secs"></param>
        void update(float secs);
    }
}
