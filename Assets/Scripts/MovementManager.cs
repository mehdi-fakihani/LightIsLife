using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LIL
{
    /// <summary>
    /// Used to get the game object's current speed ratio and see if he is immobilized.
    /// </summary>
    public class MovementManager : MonoBehaviour
    {
        private float speedRatio = 1f;
        private int immobilizeTokens = 0;
        
        /// <summary>
        /// Returns the current speed modifier (1.0 by default).
        /// </summary>
        public float getSpeedRatio() { return speedRatio; }

        /// <summary>
        /// Indicates if the game object is immobilized.
        /// </summary>
        public bool isImmobilized() { return immobilizeTokens != 0; }

        /// <summary>
        /// Applies a slow on the game object. It must be temporary and reverted with "endSlow(value)".
        /// Exemple : beginSlow(1f / 3f) will divide by 3 it's speed.
        /// </summary>
        public void beginSlow(float ratio)
        {
            speedRatio *= ratio;
        }

        /// <summary>
        /// Reverts a slow on the game object applied with "beginSlow(value)".
        /// </summary>
        public void endSlow(float ratio)
        {
            speedRatio /= ratio;
        }

        /// <summary>
        /// Immobilizes the game object. It must be cancelled later with "endImmobilization()".
        /// </summary>
        public void beginImmobilization()
        {
            immobilizeTokens += 1;
        }

        /// <summary>
        /// Finishes the game object's immobilization, after a call to "beginImmobilization()".
        /// </summary>
        public void endImmobilization()
        {
            immobilizeTokens -= 1;
        }
    }
}
