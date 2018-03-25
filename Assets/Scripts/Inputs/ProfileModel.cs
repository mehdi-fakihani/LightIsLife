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
        /*[SerializeField] public ProfilsID id;
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

        [SerializeField] public Key interaction;


        [SerializeField] public Key changeTorch;
        [SerializeField] public Key submit;*/

        public ProfilsID id { get; set; }
        public Device device { get; set; }
        public Key up { get; set; }
        public Key down { get; set; }
        public Key left { get; set; }
        public Key right { get; set; }
        public Key attack { get; set; }
        public Key skill1 { get; set; }
        public Key skill2 { get; set; }
        public Key skill3 { get; set; }
        public Key skill4 { get; set; }
        public Key interaction { get; set; }
        public Key changeTorch { get; set; }
        public Key submit { get; set; }
        public Key pause { get; set; }

        public readonly Dictionary<PlayerAction, Key> keys
            = new Dictionary<PlayerAction, Key>();

        public ProfileModel(ProfilsID _id, Device _device, Key _up, Key _down, Key _left, Key _right, Key _attack, Key _skill1,
            Key _skill2, Key _skill3, Key _skill4, Key _interaction, Key _changeTorch, Key _submit, Key _pause)
        {
            id = _id;
            device = _device;
            up = _up;
            down = _down;
            left = _left;
            right = _right;
            attack = _attack;
            skill1 = _skill1;
            skill2 = _skill2;
            skill3 = _skill3;
            skill4 = _skill4;
            interaction = _interaction;
            changeTorch = _changeTorch;
            submit = _submit;
            pause = _pause;

            // Reference keys with their actions
            keys.Add(PlayerAction.Up, up);
            keys.Add(PlayerAction.Down, down);
            keys.Add(PlayerAction.Left, left);
            keys.Add(PlayerAction.Right, right);
            keys.Add(PlayerAction.Attack, attack);
            keys.Add(PlayerAction.Skill1, skill1);
            keys.Add(PlayerAction.Skill2, skill2);
            keys.Add(PlayerAction.Skill3, skill3);
            keys.Add(PlayerAction.Skill4, skill4);

            keys.Add(PlayerAction.Interaction, interaction);

            keys.Add(PlayerAction.ChangeTorch, changeTorch);
            keys.Add(PlayerAction.Submit, submit);
            keys.Add(PlayerAction.Pause, pause);
        }

        
        /*void Awake()
        {

            // Check that the keys correspond to the device used

            foreach (var key in keys.Values)
            {
                Assert.AreEqual(device, KeyGroup.DeviceOf(key),
                    "The keys of a profile must correspond to it's device");
            }

            Profile.Models.Add(id, this);
        }

        public void SetAModel(ProfileModel model)
        {

        }*/
    }
}
