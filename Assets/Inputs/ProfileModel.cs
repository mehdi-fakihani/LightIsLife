using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;

namespace LIL.Inputs
{
    /// <summary>
    /// Stores the keys used by a player.
    /// Creates player profiles with a device number, to allow simultaneous multiple gamepads.
    /// </summary>
    public class ProfileModel : MonoBehaviour
    {
        [SerializeField] public ProfilsID id;
        [SerializeField] public Device device;
        [SerializeField] public Key up;
        [SerializeField] public Key down;
        [SerializeField] public Key left;
        [SerializeField] public Key right;
        [SerializeField] public Key attack;
        [SerializeField] public Key skill1;
        [SerializeField] public Key skill2;
        [SerializeField] public Key skill3;
        [SerializeField] public Key skill4;

        private readonly Dictionary<PlayerAction, Key> keys
            = new Dictionary<PlayerAction, Key>();
        
        void Awake()
        {
            // TODO Check if all keys correspond to the device

            keys.Add(PlayerAction.Up,     up);
            keys.Add(PlayerAction.Down,   down);
            keys.Add(PlayerAction.Left,   left);
            keys.Add(PlayerAction.Right,  right);
            keys.Add(PlayerAction.Attack, attack);
            keys.Add(PlayerAction.Skill1, skill1);
            keys.Add(PlayerAction.Skill2, skill2);
            keys.Add(PlayerAction.Skill3, skill3);
            keys.Add(PlayerAction.Skill4, skill4);

            Profile.Models.Add(id, this);
        }
        
        /// <summary>
        /// Creates a player profile with he device number indicated.
        /// This number must be zero for the keyboard (only one keyboard available).
        /// </summary>
        /// <param name="deviceNum"></param>
        /// <returns></returns>
        public Profile create(int deviceNum)
        {
            // There is only one keyboard available
            if (device == Device.Keyboard) Assert.Zero(deviceNum);
            
            var axises = keys.Values
                .Where(AxisInfo.CanComesFrom)
                .ToDictionary(key => key, key => AxisInfo.From(key, deviceNum));
            
            foreach (var axis in axises.Values)
            {
                Recorder.RecordAxis(axis);
            }
            return new Profile(keys, axises, deviceNum);
        }
    }
}
