using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

namespace LIL.Inputs
{
    /// <summary>
    /// Used to get values of buttons of each action available for the player.
    /// Created by a profile model.
    /// </summary>
    public class Profile
    {
        public static readonly Dictionary<ProfilsID, ProfileModel> Models
            = new Dictionary<ProfilsID, ProfileModel>();

        private readonly int keyShift;
        private readonly Dictionary<PlayerAction, Key> keys;
        private readonly Dictionary<Key, AxisInfo> axises;

        private AxisStatus getAxis(Key key)
        {
            return Recorder.StatusOf(axises[key]);
        }

        /// <summary>
        /// Indicates if the given action button has just been released.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public bool getKeyUp(PlayerAction action)
        {
            var key = keys[action];
            return AxisInfo.CanComesFrom(key)
                ? getAxis(key).isUp
                : Input.GetKeyUp((KeyCode) (key + keyShift));
        }

        /// <summary>
        /// Indicates if the given action button has just been pressed.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public bool getKeyDown(PlayerAction action)
        {
            var key = keys[action];
            return AxisInfo.CanComesFrom(key)
                ? getAxis(key).isDown
                : Input.GetKeyDown((KeyCode)(key + keyShift));
        }

        /// <summary>
        /// Indicates if the given action button is currently pressed.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public bool getKey(PlayerAction action)
        {
            var key = keys[action];
            return AxisInfo.CanComesFrom(key)
                ? getAxis(key).isPressed
                : Input.GetKey((KeyCode)(key + keyShift));
        }

        /// <summary>
        /// Used by ProfileModel to create a new player profile.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="keys"></param>
        /// <param name="axises"></param>
        /// <param name="deviceNum"></param>
        public Profile(Dictionary<PlayerAction, Key> keys, Dictionary<Key, AxisInfo> axises, int deviceNum)
        {
            this.keys   = keys;
            this.axises = axises;
            keyShift    = deviceNum * ((int) KeyCode.Joystick2Button0 - (int) KeyCode.Joystick1Button0);
        }
    }
}
