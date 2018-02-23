using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LIL
{
    /// <summary>
    /// Helper function to determine game object nature and
    /// relations (between players, monstersand others).
    /// </summary>
    public static class GameObjectFunctions
    {
        /// <summary>
        /// Indicates if the game object is a player.
        /// </summary>
        public static bool isPlayer(this GameObject current)
        {
            return current.CompareTag("Player");
        }

        /// <summary>
        /// Indicates if the game object is a monster (a player's enemy).
        /// </summary>
        public static bool isMonster(this GameObject current)
        {
            return current.CompareTag("Enemy");
        }

        /// <summary>
        /// Indicates if the game object isn't a player or a player's ennemy.
        /// </summary>
        public static bool isNeutral(this GameObject current)
        {
            return !current.isPlayer() && !current.isMonster();
        }

        /// <summary>
        /// Indicates if one of the game object is a player and the other a monster.
        /// </summary>
        public static bool isEnemyWith(this GameObject current, GameObject other)
        {
            return  (current.isPlayer() && other.isMonster()) ||
                    (other.isPlayer() && current.isMonster());
        }

        /// <summary>
        /// Indicates if both of the game objects are players or monsters.
        /// </summary>
        public static bool isAllyWith(this GameObject current, GameObject other)
        {
            return (current.isPlayer() && other.isPlayer()) ||
                   (other.isMonster()  && current.isMonster());
        }
    }
}
