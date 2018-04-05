using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LIL.Inputs
{
    /// <summary>
    /// Used to get values of buttons of each action available for the player.
    /// Created by a profile model.
    /// </summary>
    public class Profile
    {
        public static readonly Dictionary<int, ProfileModel> Models
            = new Dictionary<int, ProfileModel>();

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
            return KeyGroup.IsFromAxis(key)
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
            return KeyGroup.IsFromAxis(key)
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
            return KeyGroup.IsFromAxis(key)
                ? getAxis(key).isPressed
                : Input.GetKey((KeyCode)(key + keyShift));
        }

        public Profile(int playerNum, int deviceNum)
        {
            var model = Models[playerNum];
            keys = model.keys;
            axises = model.keys.Values
                .Where(KeyGroup.IsFromAxis)
                .ToDictionary(
                    key => key,
                    key => AxisInfo.From(key, deviceNum));

            foreach (var axis in axises.Values)
            {
                Recorder.RecordAxis(axis);
            }
            keyShift = deviceNum * ((int)KeyCode.Joystick2Button0 - (int)KeyCode.Joystick1Button0);
        }
    }
}
